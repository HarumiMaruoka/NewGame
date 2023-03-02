// 日本語対応
using System;
using UnityEngine;
using HitSupport;
using UniRx;

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
        private FloatReactiveProperty _currentHorizontalSpeed = new FloatReactiveProperty(0f);
        [Tooltip("垂直方向移動の現在速度"), SerializeField]
        private float _currentVerticalSpeed = 0f;

        [Header("移動アニメーションを制御する値")]
        [Tooltip("地上移動アニメーションを制御する値"), SerializeField]
        private MoveAnimationController _moveAnimationController = default;
        [Tooltip("ジャンプ、空中、着地アニメーションを制御する値"), SerializeField]
        private JumpAnimationController jumpAnimationController = default;

        /// <summary> 現在プレイヤーが向いている方向（垂直方向） </summary>
        private Vector3 _currentDirection = Vector3.zero;
        private Quaternion _targetRotation = default;

        public bool CanMove { get; set; } = true;
        public bool CanJump { get; set; } = true;
        public IReadOnlyReactiveProperty<float> CurrentHorizontalSpeed => _currentHorizontalSpeed;
        public float MaxMovementSpeed => _maxMovementSpeed;
        public OverLapSphere GroundedChecker => _groundedChecker;
        public event Action OnJump = null;

        public override void Start()
        {
            _groundedChecker.Init(_playerController.transform);
            _moveAnimationController.Init(_playerController.Animator, this);
            jumpAnimationController.Init(_playerController.Animator, this);
        }
        public override void Update()
        {
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
            if (_playerController.InputManager.IsExist[InputType.Move] && CanMove)
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
                _currentHorizontalSpeed.Value += Time.deltaTime * _movementAcceleration;
                if (_maxMovementSpeed < _currentHorizontalSpeed.Value)
                {
                    _currentHorizontalSpeed.Value = _maxMovementSpeed;
                } // 最大値を超えないように調整する
            }
            // 入力がない場合の処理
            else
            {
                // 速度を減算する
                _currentHorizontalSpeed.Value -= Time.deltaTime * _movementDeceleration;
                if (0f > _currentHorizontalSpeed.Value)
                {
                    _currentHorizontalSpeed.Value = 0f;
                } // 最小値を下回らないように調整する
            }
            return _currentDirection * _currentHorizontalSpeed.Value;
        }
        private float VerticalCalculation()
        {
            // 接地していて、ジャンプ入力があるときの処理
            if (_groundedChecker.IsHit() && _playerController.InputManager.IsPressed[InputType.Jump] && CanJump)
            {
                OnJump?.Invoke();
                _currentVerticalSpeed = _jumpPower;
            }
            // 接地していて、垂直移動速度がマイナス（落下している）ときの処理
            else if (_groundedChecker.IsHit() && _currentVerticalSpeed < 0f)
            {
                _currentVerticalSpeed = Time.deltaTime * _gravitationalAcceleration;
            }
            // 接地していないときの処理
            else
            {
                _currentVerticalSpeed += Time.deltaTime * _gravitationalAcceleration;
            }
            return _currentVerticalSpeed;
        }
    }
}