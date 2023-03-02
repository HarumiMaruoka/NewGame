// 日本語対応
using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UniRx;
using UnityEngine.Serialization;

namespace Player
{
    [Serializable]
    public class JumpAnimationController
    {
        [AnimationParameter, SerializeField]
        private string _jumpAnimName = default;
        [AnimationParameter, SerializeField]
        private string _midairAnimName = default;
        [NonSerialized]
        private PlayerMove _playerMove = null;

        private Animator _animator = null;

        public void Init(Animator animator, PlayerMove playerMove)
        {
            _animator = animator;
            _playerMove = playerMove;

            // Midairアニメーションの設定
            // IsHitReactivePropertyがtrueであれば着地、falseであれば空中にいる事を表現する。
            _playerMove.GroundedChecker.IsHitReactiveProperty.
                Subscribe(value => _animator.SetBool(_midairAnimName, !value));
            // ジャンプアニメーションの設定
            _playerMove.OnJump += () => AnimPlay(_jumpAnimName);
        }
        /// <summary> 1フレームだけ特定のアニメーションパラメータをtrueにする </summary>
        /// <param name="animName"></param>
        private async void AnimPlay(string animName)
        {
            _animator.SetBool(animName, true);
            await UniTask.DelayFrame(1);
            _animator.SetBool(animName, false);
        }
    }
}