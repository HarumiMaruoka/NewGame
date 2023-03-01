// 日本語対応
using System;
using UnityEngine.InputSystem;

namespace Player
{
    [Serializable]
    public abstract class PlayerProperties
    {
        protected PlayerController _playerController { get; private set; }
        public void Init(PlayerController playerController)
        {
            _playerController = playerController;
            _playerController.AddProperty(this);
            Start();
        }
        public abstract void Start();
        public abstract void Update();
    }
}