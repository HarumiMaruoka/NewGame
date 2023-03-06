// 日本語対応
using System;

[Serializable]
public class Kitchenware : Item
{
	public Kitchenware(int id) : base(id)
    {

    }

    public override ItemType Type => ItemType.Kitchenware;
}