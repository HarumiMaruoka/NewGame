// 日本語対応
using System;

[Serializable]
public class Armor : Item
{
    public Armor(int id) : base(id)
    {

    }
    public override ItemType Type => ItemType.Armor;
}