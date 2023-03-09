// 日本語対応
using System;
using UnityEngine.UI;

[Serializable]
public class Meal : Item
{
    public Meal(int id, string name, Image icon, string explanatoryText) :
        base(id, name, icon, explanatoryText)
    {

    }
    public override ItemType Type => ItemType.Meal;

    public override void OnButtonClick()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// この料理を使用したときの特殊効果
    /// </summary>
    [Serializable]
    public enum MealSpecialEffects
    {

    }
}