// 日本語対応
using UnityEngine;
using UniRx;

public class PlayerSearch : MonoBehaviour
{
    private BoolReactiveProperty _isPlayerFind = new BoolReactiveProperty(false);

    public IReadOnlyReactiveProperty<bool> IsPlayerFind => _isPlayerFind;
}