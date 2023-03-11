// 日本語対応
using UnityEngine;

/// <summary>
/// ゲームの管理者
/// </summary>
public class GameManager
{
    #region Singleton
    private static GameManager _instance = new GameManager();
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError($"Error! Please correct!");
            }
            return _instance;
        }
    }
    private GameManager() { }
    #endregion

    private PauseManager _pauseManager = new PauseManager();
    private SaveManager _saveManager = new SaveManager();
    private ActorDataBase _actorDataBase = new ActorDataBase();
    /// <summary>
    /// ポーズ状態を管理するクラス
    /// </summary>
    public PauseManager PauseManager => _pauseManager;
    /// <summary>
    /// セーブロードを管理するクラス
    /// </summary>
    public SaveManager SaveManager => _saveManager;
    /// <summary>
    /// アクターの情報を保持、管理するクラス
    /// </summary>
    public ActorDataBase ActorDataBase => _actorDataBase;
}