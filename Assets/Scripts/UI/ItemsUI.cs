using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;

public class ItemsUI : MonoBehaviour
{
    [Inject]
    private ItemsController _itemsController;
    [Inject]
    private LevelSettings _levelSettings;

    [SerializeField] private RectTransform _layoutTransform;
    [SerializeField] private ItemView _itemPrefab;

    private List<ItemView> _items = new();

    private void Awake()
    {
        _itemsController.OnAvailableItemsUpdated += Refresh;
    }

    private void Refresh()
    {
        var itemInfos = _itemsController.AvalilableItems.Select(item => item.ItemInfo);
        var targetSize = _itemsController.AvalilableItems.Count;

        // fix size
        while (_items.Count > targetSize)
        {
            var toRemove = _items[^1];
            Destroy(toRemove.gameObject);
            _items.RemoveAt(_items.Count - 1);
        }
        while (_items.Count < targetSize)
        {
            var newItem = Instantiate(_itemPrefab, _layoutTransform);
            _items.Add(newItem);
        }

        // refresh
        var i = 0;
        foreach (var itemInfo in itemInfos) 
        {
            _items[i++].Refresh(itemInfo, _levelSettings.ShowIcons, _levelSettings.ShowNames);
        }
    }

    private void OnDestroy()
    {
        _itemsController.OnAvailableItemsUpdated -= Refresh;
    }
}
