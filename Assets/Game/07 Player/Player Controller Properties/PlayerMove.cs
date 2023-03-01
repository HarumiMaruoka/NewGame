// 日本語対応
using System;
using UnityEngine;
using HitSupport;

namespace Player
{
    [Serializable]
    public class PlayerMove : PlayerProperties
    {
        [Header("水平方向の移動に使用する値")]
        [Tooltip("移動加速度"), SerializeField]
        private float _movementAcceleration = 1f;
        [Tooltip("移動最大速度"), SerializeField]
        private float _maxMovementSpeed = 4f;
        [Tooltip("移動減速度"), SerializeField]
        private float _movementDeceleration = 1f;

        [Header("垂直方向の移動に使用する値")]
        [Tooltip("接地状態検知用の値"), SerializeField]
        private OverLapSphere _groundedChecker = default;
        [Tooltip("重力加速度"), SerializeField]
        private float _gravitationalAcceleration = -1f;
        [Tooltip("ジャンプ力"), SerializeField]
        private float _jumpPower = 8f;

        [Header("回転に使用する値")]
        [Tooltip("回転速度"), SerializeField]
        private float _rotationSpeed = 600f;

        [Header("インスペクタで値の変化を確認する用")]
        [Tooltip("水平方向移動の現在速度"), SerializeField]
        private float _currentHorizontalSpeed = 0f;
        [Tooltip("垂直方向移動の現在速度"), SerializeField]
        private float _currentVerticalSpeed = 0f;

        /// <summary> 現在プレイヤーが向いている方向（垂直方向） </summary>
        private Vector3 _currentDirection = Vector3.zero;
        private Quaternion _targetRotation = default;

        public override void Start()
        {
            _groundedChecker.Init(_playerController.transform);
        }
        public override void Update()
        {
            _groundedChecker.Update();

            var velocity = HorizontalCalculation();
            velocity.y = VerticalCalculation();
            _playerController.CharacterController.
                Move(velocity * Time.deltaTime);
        }
        public void OnDrawGizmo(Transform origin)
        {
            _groundedChecker.OnDrawGizmos(origin);
        }
        private Vector3 HorizontalCalculation()
        {


            // 入力がある場合の処理
            if (_playerController.InputManager.IsExist[InputType.Move])
            {
                // 入力の方向を保存しておく
                Vector3 value = _playerController.InputManager.GetValue<Vector2>(InputType.Move);
                _currentDirection = new Vector3(value.x, 0f, value.y).normalized;

                // カメラに合わせて 移動方向とプレイヤーの向きを調整する
                _currentDirection = Camera.main.transform.TransformDirection(_currentDirection);
                _targetRotation = Quaternion.LookRotation(_currentDirection, Vector3.up);
                _currentDirection.y = 0f;
                _currentDirection = _currentDirection.normalized;
                // 回転の補正
                _targetRotation.x = 0f;
                _targetRotation.z = 0f;
                _playerController.transform.rotation = Quaternion.RotateTowards(_playerController.transform.rotation, _targetRotation, _rotationSpeed * Time.deltaTime);

                // 速度を加算する
                _currentHorizontalSpeed += Time.deltaTime * _movementAcceleration;
                if (_maxMovementSpeed < _currentHorizontalSpeed)
                {
                    _currentHorizontalSpeed = _maxMovementSpeed;
                } // 最大値を超えないように調整する
            }
            // 入力がない場合の処理
            else
            {
                // 速度を減算する
                _currentHorizontalSpeed -= Time.deltaTime * _movementDeceleration;
                if (0f > _currentHorizontalSpeed)
                {
                    _currentHorizontalSpeed = 0f;
                } // 最大値を超えないように調整する
            }
            // Debug.Log(_currentHorizontalSpeed);
            return _currentDirection * _currentHorizontalSpeed;
        }
        private float VerticalCalculation()
        {
            if (_groundedChecker.IsHit() && _playerController.InputManager.IsPressed[InputType.Jump])
            {
                _currentVerticalSpeed = _jumpPower;
            }
            else if (_groundedChecker.IsHit() && _currentVerticalSpeed < 0f)
            {
                _currentVerticalSpeed = Time.deltaTime * _gravitationalAcceleration;
            }
            else
            {
                _currentVerticalSpeed += Time.deltaTime * _gravitationalAcceleration;
            }
            return _currentVerticalSpeed;
        }
    }
}