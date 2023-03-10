// 日本語対応
using System;
using UnityEngine.UI;

[Serializable]
public class CookingIngredients : Item
{
    public CookingIngredients(int id, string name, string explanatoryText) :
        base(id, name, explanatoryText)
    {

    }

    public override ItemType Type => ItemType.CookingIngredients;

    public override void OnButtonClick()
    {
        throw new NotImplementedException();
    }
}