// 日本語対応
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary> 食事UIを制御するクラス </summary>
public class MealUIController : MonoBehaviour
{
    [Tooltip("食事ボタンのプレハブ"), SerializeField]
    private GameObject _itemButtonPrefab = default;
    [Tooltip("誰に使用しますか？画面時に出すアクターボタンのプレハブ"), SerializeField]
    private GameObject _mealActorButtonPrefab = default;

    [Tooltip("食事ボタンのプレハブの親オブジェクト"), SerializeField]
    private Transform _mealButtonsParent = default;
    [Tooltip("アクターボタンの親オブジェクト"), SerializeField]
    private Transform _mealActorButtonsParent = default;

    /// <summary> 各ボタンへの参照を保持するフィールド。添字に食事のIDを指定する。 </summary>
    private MealButton[] _mealButtons = new MealButton[ItemDataBase.MaxID_Meal];
    /// <summary> 各アクターボタンへの参照を保持するフィールド。添字にアクターIDを指定する。 </summary>
    private MealActorButton[] _mealActorButtons = new MealActorButton[ActorDataBase.MaxActorID];

    /// <summary> 各ボタンへの参照。添字に食事IDを指定することで取得できる。 </summary>
    public MealButton[] MealButtons => _mealButtons;
    /// <summary> 各アクターボタンへの参照。添字にアクターIDを指定することで取得できる。 </summary>
    public MealActorButton[] MealActorButtons => _mealActorButtons;

    /// <summary> UIボタンを生成済みかどうか表現する値。 </summary>
    private bool _isButtonGenerated = false;

    public async void GenerateButton()
    {
        if (!_isButtonGenerated) // ボタンが未生成である時のみ実行する
        {
            _isButtonGenerated = true;

            // 各食事ボタンを生成し、食事ボタン用コンポーネントを割り当て、
            // 名前用テキストに、この食事の名前を割り当てる。
            for (int i = 0; i < ItemDataBase.MaxID_Meal; i++)
            {
                var buttonObj = Instantiate(_itemButtonPrefab, _mealButtonsParent);
                var mealButton = buttonObj.AddComponent<MealButton>();
                var button = buttonObj.GetComponent<Button>();
                mealButton.Setup(i, ItemType.Meal, button, ItemDataBase.Instance.Meals[i]);
                button.onClick.AddListener(() => { });
            }
            // 各アクターボタンを生成する
            for (int i = 0; i < ActorDataBase.MaxActorID; i++)
            {
                var mealActorButton = Instantiate(_mealActorButtonPrefab, _mealActorButtonsParent);
                await UniTask.WaitUntil(() => GameManager.Instance.ActorDataBase.IsGenerated);
                mealActorButton.transform.GetChild(0).GetComponent<Text>().text =
                    GameManager.Instance.ActorDataBase.Actors[i].Name;
            }
        }
    }
}
