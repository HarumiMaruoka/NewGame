// 日本語対応
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemButtonController : MonoBehaviour
{
    public event Action OnDataLoaded = null;
    public event Action<GameObject, GameObject> OnChangedSelectedButton = null;

    [SerializeField]
    private GameObject _itemButton = default;
    [SerializeField]
    private Transform _mealButtonParent = default;
    [SerializeField]
    private EventSystem _eventSystem = default;

    private GameObject _previousSelectedObject = null;

    private async void Awake()
    {
        if (!ItemDataBase.Instance.IsLoaded)
        {
            await ItemDataBase.Instance.LoadItemData();
            GenerateButton();
            OnDataLoaded?.Invoke();
        }
    }
    private void Update()
    {
        if (_previousSelectedObject != _eventSystem.currentSelectedGameObject)
        {
            OnChangedSelectedButton?.Invoke(_previousSelectedObject, _eventSystem.currentSelectedGameObject);
        }
        _previousSelectedObject = _eventSystem.currentSelectedGameObject;
    }
    private void GenerateButton()
    {
        SetupMealButton(_mealButtonParent);
    }
    private void SetupMealButton(Transform parent)
    {
        for (int i = 0; i < ItemDataBase.MaxID_Meal; i++)
        {
            var button = Instantiate(_itemButton, parent);
            button.AddComponent<ItemButton>().Setup(i, ItemType.Meal);
            var mealButton = button.AddComponent<MealButton>();
            mealButton.Setup();
            mealButton.NameText.text = ItemDataBase.Instance.Meals[i].Name;
        }
    }
}
