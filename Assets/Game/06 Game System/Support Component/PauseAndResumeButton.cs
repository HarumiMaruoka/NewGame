// 日本語対応
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// ボタンからポーズとリジュームの命令を呼び出す為のコンポーネント
/// </summary>
public class PauseAndResumeButton : MonoBehaviour
{
    [Tooltip("ポーズ処理（インスペクタで割り当てる用）"), SerializeField]
    private UnityEvent _onPause = default;
    [Tooltip("リジューム処理（インスペクタで割り当てる用）"), SerializeField]
    private UnityEvent _onResume = default;

    /// <summary>
    /// ポーズの実行処理
    /// </summary>
    public void ExecutePause()
    {
        GameManager.Instance.PauseManager.ExecutePause(_onPause.Invoke);
    }
    /// <summary>
    /// リジュームの実行処理
    /// </summary>
    public void ExecuteResume()
    {
        GameManager.Instance.PauseManager.ExecuteResume(_onResume.Invoke);
    }
}