// 日本語対応
using System;

[Serializable]
public abstract class Item
{
    private int _id = -1;

    public abstract ItemType Type { get; }

    public int ID => _id;

    public Item(int id)
    {
        _id = id;
    }
}