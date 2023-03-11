// 日本語対応
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 食料ウィンドウを制御するスクリプト
/// </summary>
public class MealWindowController : MonoBehaviour
{
    [SerializeField]
    private ItemButtonController _itemButtonController = default;
    [SerializeField]
    private Text _descriptionTextArea = default;

    private void OnEnable()
    {
        _itemButtonController.OnChangedSelectedButton += DrawingUpdate;
    }

    /// <summary> 説明文を更新する </summary>
    /// <param name="oldGO"> 変更前の選択オブジェクト </param>
    /// <param name="newGO"> 変更後の選択オブジェクト </param>
    void DrawingUpdate(GameObject oldGO, GameObject newGO)
    {
        if (newGO != null && newGO.TryGetComponent(out MealButton mealButton))
        {
            _descriptionTextArea.text = ItemDataBase.Instance.Meals[mealButton.ID].ExplanatoryText;
        }
    }
}