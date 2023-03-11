// 日本語対応
using UnityEngine;
using UnityEngine.Events;

public class SaveAndLoadButton : MonoBehaviour
{
    [Tooltip("セーブ処理（インスペクタから割り当てる用）"), SerializeField]
    private UnityEvent _onSave = default;
    [Tooltip("ロード処理（インスペクタから割り当てる用）"), SerializeField]
    private UnityEvent _onLoad = default;

    /// <summary>
    /// セーブの実行処理
    /// </summary>
    public void ExecutePause()
    {
        GameManager.Instance.PauseManager.ExecutePause(_onSave.Invoke);
    }
    /// <summary>
    /// ロードの実行処理
    /// </summary>
    public void ExecuteResume()
    {
        GameManager.Instance.PauseManager.ExecuteResume(_onLoad.Invoke);
    }
}
