// 日本語対応
using System;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace Player
{
    [Serializable]
    public class MoveAnimationController
    {
        [AnimationParameter, SerializeField]
        private string _blendValueName = default;
        [NonSerialized]
        private PlayerMove _playerMove = null;

        private Animator _animator = null;

        public void Init(Animator animator, PlayerMove playerMove)
        {
            _animator = animator;
            _playerMove = playerMove;
            _playerMove.CurrentHorizontalSpeed.Subscribe(_ => SetParameter());
        }
        private void SetParameter()
        {
            _animator.SetFloat(_blendValueName,
                _playerMove.CurrentHorizontalSpeed.Value / _playerMove.MaxMovementSpeed);
        }
    }
}
