// 日本語対応
using UnityEngine;

namespace Almighty
{
    /// <summary>
    /// アニメーションのポーズ, リジュームを管理するクラス。
    /// </summary>
    public class AnimationPouser : MonoBehaviour, IPausable
    {
        [Tooltip("アニメーション制御用の値を割り当ててください"), AnimationParameter, SerializeField]
        private string[] _animSpeedValues = default;

        /// <summary> 元々のアニメーションスピードを保存しておく配列 </summary>
        private float[] _originalValue = null;
        private Animator _animator = default;

        private void Start()
        {
            // アニメーターを取得
            _animator = GetComponent<Animator>();
            // 配列の領域を確保
            _originalValue = new float[_animSpeedValues.Length];
            // アニメーションスピードの初期値を取得し 保存しておく。
            for (int i = 0; i < _originalValue.Length; i++)
            {
                _originalValue[i] = _animator.GetFloat(_animSpeedValues[i]);
            }
        }
        /// <summary>
        /// アニメーションのポーズ処理 <br/>
        /// アニメーションスピード制御用の値を0にする。
        /// </summary>
        public void Pause()
        {
            if (_animSpeedValues != null)
            {
                for (int i = 0; i < _animSpeedValues.Length; i++)
                {
                    _animator.SetFloat(_animSpeedValues[i], 0f);
                }
            }
        }
        /// <summary>
        /// アニメーションのリジューム処理 <br/>
        /// アニメーションスピード制御用の値を元の値に戻す。
        /// </summary>
        public void Resume()
        {
            if (_animSpeedValues != null)
            {
                for (int i = 0; i < _animSpeedValues.Length; i++)
                {
                    _animator.SetFloat(_animSpeedValues[i], _originalValue[i]);
                }
            }
        }

        // ポーズマネージャーに自身を登録する
        private void OnEnable()
        {
            GameManager.Instance.PauseManager.Register(this);
        }
        private void OnDisable()
        {
            GameManager.Instance.PauseManager.Lift(this);
        }
    }
}
