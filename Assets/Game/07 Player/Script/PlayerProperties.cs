// 日本語対応
using System;

namespace Player
{
    [Serializable]
    public abstract class PlayerProperties
    {
        [NonSerialized]
        protected PlayerController _playerController = null;

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