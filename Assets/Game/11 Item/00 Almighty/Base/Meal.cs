// 日本語対応
using System;

[Serializable]
public class Meal : Item
{
    public Meal(int id) : base(id)
    {

    }
    public override ItemType Type => ItemType.Meal;
}