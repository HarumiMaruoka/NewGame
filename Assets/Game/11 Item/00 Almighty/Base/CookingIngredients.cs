// 日本語対応
using System;

[Serializable]
public class CookingIngredients : Item
{
    public CookingIngredients(int id) : base(id)
    {

    }

    public override ItemType Type => ItemType.CookingIngredients;
}