// 日本語対応
using UnityEngine;
using UnityEngine.AI;
using UniRx;

/// <summary>
/// プレイヤーに向かって移動するクラス
/// </summary>
public class MoveToPlayer : MonoBehaviour
{
    [TagName, SerializeField]
    private string _playerTag = default;

    private Transform _playerPos = null;
    /// <summary>
    /// 自身のナビメッシュエージェント <br/>
    /// リファレンス : https://docs.unity3d.com/ja/2021.3/ScriptReference/AI.NavMeshAgent.html
    /// </summary>
    private NavMeshAgent _navMeshAgent = null;
    private PlayerSearch _playerSearch = null;

    private void Start()
    {
        // プレイヤーのTransformを取得する。
        _playerPos = GameObject.FindGameObjectWithTag(_playerTag).transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _playerSearch = GetComponent<PlayerSearch>();

        _playerSearch.IsPlayerFind.Subscribe(value => _navMeshAgent.isStopped = value);
    }
}