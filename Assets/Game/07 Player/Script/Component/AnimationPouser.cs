// 日本語対応
using UnityEngine;
using DG;
using DG.Tweening;

namespace Almighty
{
    /// <summary>
    /// アニメーションのポーズ, リジュームを管理するクラス。
    /// </summary>
    public class AnimationPouser : MonoBehaviour, IPausable
    {
        [AnimationParameter, SerializeField]
        private string[] _animSpeedValues = default;

        private float[] _originalValue = null;
        private Animator _animator = default;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _originalValue = new float[_animSpeedValues.Length];
            for (int i = 0; i < _originalValue.Length; i++)
            {
                _originalValue[i] = _animator.GetFloat(_animSpeedValues[i]);
            }
        }
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
