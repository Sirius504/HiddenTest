using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    [SerializeField] private int _maxAvailable = 3;

    private HashSet<HiddenItem> _itemsToCollect = new();
    private HashSet<HiddenItem> _collectedItems = new();

    private HiddenItem[] _availableItems;

    public IReadOnlyCollection<HiddenItem> AvalilableItems => _availableItems;
    public IReadOnlyCollection<HiddenItem> ItemsToCollect => _itemsToCollect;


    private void Start()
    {
        for(var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (!child.TryGetComponent<HiddenItem>(out var item)) continue;

            item.Interactable = false;
            _itemsToCollect.Add(item);

            item.OnCollected += OnItemCollected;
            item.OnDestroyed += OnItemDestroyed;
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
        _availableItems = _itemsToCollect.Take(_maxAvailable).ToArray();
        foreach (var item in _availableItems)
        {
            item.Interactable = true;
            Debug.Log(item.Name);
        }
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
}
