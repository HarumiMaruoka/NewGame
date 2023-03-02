// 日本語対応
using UnityEngine;

/// <summary>
/// ボタンからポーズとリジュームの命令を呼び出す為のコンポーネント
/// </summary>
public class PauseAndResumeButton : MonoBehaviour
{
    /// <summary>
    /// ポーズの実行処理
    /// </summary>
    public void OnPause()
    {
        GameManager.Instance.PauseManager.ExecutePause();
    }
    /// <summary>
    /// リジュームの実行処理
    /// </summary>
    public void OnResume()
    {
        GameManager.Instance.PauseManager.ExecuteResume();
    }
}