// 日本語対応
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ItemButtonController : MonoBehaviour
{
    [Tooltip("アイテムデータを読み込んだ直後に発行されるイベント"), SerializeField]
    private UnityEvent _onDataLoaded = default;
    [Tooltip("イベントシステム"), SerializeField]
    private EventSystem _eventSystem = default;
    /// <summary> 前フレームで選択されていたオブジェクト </summary>
    private GameObject _previousSelectedObject = null;

    /// <summary> アイテムデータを読み込んだ直後に発行されるイベント </summary>
    public event Action OnDataLoaded = null;
    /// <summary> 選択中のオブジェクトが変更された時に発行されるイベント </summary>
    public event Action<GameObject, GameObject> OnChangedSelectedButton = null;

    private async void Awake()
    {
        if (!ItemDataBase.Instance.IsLoaded)
        {
            await ItemDataBase.Instance.LoadItemData();
            _onDataLoaded.Invoke();
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
}
