using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemsController : MonoBehaviour
{
    [SerializeField] private int _maxAvailable = 3;

    private HashSet<HiddenItem> _uncollectedItems = new();
    private HashSet<HiddenItem> _collectedItems = new();

    private IReadOnlyCollection<HiddenItem> _availableItems;


    private void Start()
    {
        for(var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (!child.TryGetComponent<HiddenItem>(out var item)) continue;

            item.Interactable = false;
            _uncollectedItems.Add(item);

            item.OnCollected += OnItemCollected;
            item.OnDestroyed += OnItemDestroyed;
        }

        UpdateAvailableItems();
    }

    private void OnItemDestroyed(HiddenItem item)
    {
        item.OnDestroyed -= OnItemDestroyed;
        item.OnCollected -= OnItemCollected;
        _uncollectedItems.Remove(item);        
    }

    private void UpdateAvailableItems()
    {
        _uncollectedItems.ExceptWith(_collectedItems);
        _availableItems = _uncollectedItems.Take(_maxAvailable).ToArray();
        foreach (var item in _availableItems)
        {
            item.Interactable = true;
        }
    }

    public void OnItemCollected(HiddenItem item)
    {
        if (!_uncollectedItems.Contains(item))
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
