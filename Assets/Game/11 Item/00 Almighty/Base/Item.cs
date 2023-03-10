// 日本語対応
using System;
using UnityEngine.UI;

[Serializable]
public abstract class Item
{
    private readonly int _id = -1;
    private readonly string _name = "未設定";
    private readonly string _explanatoryText = "未設定";
    private Image _icon = null;

    public int ID => _id;
    public string Name => _name;
    public Image Icon => _icon;
    public string ExplanatoryText => _explanatoryText;

    public abstract ItemType Type { get; }
    public abstract void OnButtonClick();

    public Item(int id, string name, string explanatoryText)
    {
        _id = id; _name = name; _explanatoryText = explanatoryText;
    }
    public void SetIcon(Image image)
    {
        _icon = image;
    }
}