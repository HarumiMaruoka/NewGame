// 日本語対応
using System;
using UnityEngine.UI;

[Serializable]
public class Valuables : Item
{
    public Valuables(int id, string name, string explanatoryText) :
        base(id, name, explanatoryText)
    {

    }

    public override ItemType Type => ItemType.Valuables;

    public override void OnButtonClick()
    {
        throw new NotImplementedException();
    }
}