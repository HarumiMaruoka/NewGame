// 日本語対応
using UnityEngine;
using UnityEngine.UI;

public class MealButton : ItemButton
{
    /// <summary> 所持数を表現するテキスト </summary>
    private Text _numberText = default;

    public void Setup(int id, ItemType itemType, Button owner, Item item)
    {
        base.Setup(id, itemType, owner, item, null);
        _numberText = transform.GetChild(1).gameObject.GetComponent<Text>();

        // 食事ボタンの名前を設定
        transform.GetChild(0).gameObject.GetComponent<Text>().text = ItemDataBase.Instance.Meals[id].Name;
    }

    public void UpdateNumberText(int currentValue)
    {
        _numberText.text = $"×{currentValue.ToString("000")}"; // 三桁で表示する
    }
}