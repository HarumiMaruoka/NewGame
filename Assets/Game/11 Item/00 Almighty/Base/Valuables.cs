// 日本語対応
using System;

[Serializable]
public class Valuables : Item
{
    public Valuables(int id) : base(id)
    {

    }

    public override ItemType Type => ItemType.Valuables;
}