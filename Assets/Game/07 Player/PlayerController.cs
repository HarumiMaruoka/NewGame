// 日本語対応
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        // ========= インスペクタ用フィールド ========= //
        [SerializeField]
        private PlayerMove _playerMove = default;

        // ================ フィールド ================ //
        private CharacterController _characterController = null;
        private InputManager _inputManager = new InputManager();
        private List<PlayerProperties> _playerProperties = new List<PlayerProperties>();

        // =============== プロパティ群 =============== //
        public PlayerMove PlayerMove => _playerMove;
        public CharacterController CharacterController => _characterController;
        public InputManager InputManager => _inputManager;
        public IReadOnlyList<PlayerProperties> PlayerProperties => _playerProperties;

        // ================ メソッド群 ================ //
        private void Start()
        {
            _characterController = GetComponent<CharacterController>();
            _inputManager.Init();
            _playerMove.Init(this);
        }

        private void Update()
        {
            foreach (var e in _playerProperties) e.Update();
        }
        private void OnDrawGizmos()
        {
            _playerMove.OnDrawGizmo(transform);
        }
        public void AddProperty(PlayerProperties playerProperties)
        {
            if (!_playerProperties.Contains(playerProperties))
            {
                _playerProperties.Add(playerProperties);
            }
        }
    }
}
