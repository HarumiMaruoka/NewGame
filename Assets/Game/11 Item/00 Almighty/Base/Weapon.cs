// 日本語対応
using System;

[Serializable]
public class Weapon : Item
{
    public Weapon(int id) : base(id)
    {

    }
    public override ItemType Type => ItemType.Weapon;
}