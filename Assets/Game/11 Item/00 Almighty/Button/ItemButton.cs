// 日本語対応
using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private int _id = -1;
    private ItemType _itemType = ItemType.NotSet;

    public int ID => _id;
    public ItemType ItemType => _itemType;

    public virtual void Setup(int id, ItemType itemType, Button owner, Item item, Action onClicked)
    {
        _id = id;
        _itemType = itemType;
        owner.onClick.AddListener(item.OnButtonClick);
        owner.onClick.AddListener(() => onClicked?.Invoke());
    }
}
