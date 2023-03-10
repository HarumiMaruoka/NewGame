// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class ItemButton : MonoBehaviour
{
    private int _id = -1;
    private ItemType _itemType = ItemType.NotSet;

    public int ID => _id;
    public ItemType ItemType => _itemType;

    public void Setup(int id, ItemType itemType)
    {
        _id = id;
        _itemType = itemType;
    }
}
