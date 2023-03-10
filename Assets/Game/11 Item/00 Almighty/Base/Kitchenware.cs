// 日本語対応
using System;
using UnityEngine.UI;

[Serializable]
public class Kitchenware : Item
{
    public Kitchenware(int id, string name, string explanatoryText) :
        base(id, name, explanatoryText)
    {

    }

    public override ItemType Type => ItemType.Kitchenware;

    public override void OnButtonClick()
    {
        throw new NotImplementedException();
    }
}