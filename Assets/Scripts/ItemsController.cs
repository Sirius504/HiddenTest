using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class ItemsController : MonoBehaviour
{
    [Inject] private LevelSettings _levelSettings;

    private HashSet<HiddenItem> _itemsToCollect = new();
    private HashSet<HiddenItem> _collectedItems = new();

    private HiddenItem[] _availableItems;

    public IReadOnlyCollection<HiddenItem> AvalilableItems => _availableItems;
    public IReadOnlyCollection<HiddenItem> ItemsToCollect => _itemsToCollect;
    public event Action OnAvailableItemsUpdated;


    private void Awake()
    {
        for(var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (!child.TryGetComponent<HiddenItem>(out var item)) continue;
            if (!item.ItemInfo.Enabled) continue;

            _itemsToCollect.Add(item);

            item.OnCollected += OnItemCollected;
            item.OnDestroyed += OnItemDestroyed;
        }
        int GetIndex(HiddenItem item) => _levelSettings.ItemInfos.IndexOf(item.ItemInfo);
        _itemsToCollect = _itemsToCollect.OrderBy(GetIndex).ToHashSet();
    }

    private void Start()
    {
        foreach(var item in _itemsToCollect)
        {
            item.Interactable = false;
        }
        UpdateAvailableItems();
    }

    private void OnItemDestroyed(HiddenItem item)
    {
        item.OnDestroyed -= OnItemDestroyed;
        item.OnCollected -= OnItemCollected;
        _itemsToCollect.Remove(item);        
    }

    private void UpdateAvailableItems()
    {
        _itemsToCollect.ExceptWith(_collectedItems);
        _availableItems = _itemsToCollect.Take(_levelSettings.MaxItemsAvailable).ToArray();
        foreach (var item in _availableItems)
        {
            item.Interactable = true;
        }
        OnAvailableItemsUpdated?.Invoke();
    }

    public void OnItemCollected(HiddenItem item)
    {
        if (!_itemsToCollect.Contains(item))
        {
            Debug.LogError($"Collected unregistered item {item.Name}");
            return;
        }

        if (!_collectedItems.Add(item))
        {
            Debug.LogWarning($"Collected item {item.Name} which has been collected already.");
        }

        UpdateAvailableItems();
    }

    private void OnDestroy()
    {
        foreach (var item in _itemsToCollect)
        {
            item.OnDestroyed -= OnItemDestroyed;
            item.OnCollected -= OnItemCollected;
        }
    }
}
