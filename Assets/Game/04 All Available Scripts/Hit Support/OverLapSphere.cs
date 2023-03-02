using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

namespace HitSupport
{
    [System.Serializable]
    public class OverLapSphere
    {
        [SerializeField]
        private Vector3 _offset;
        [SerializeField]
        private float _radius;
        [SerializeField]
        private LayerMask _targetLayer;

        private BoolReactiveProperty _isHit = new BoolReactiveProperty(false);
        private Transform _origin;

        public Vector3 Offset => _offset;
        public float Radius => _radius;
        public LayerMask TargetLayer => _targetLayer;
        public Transform Origin => _origin;
        public IReadOnlyReactiveProperty<bool> IsHitReactiveProperty => _isHit;

        private Collider[] _colliders = null;

        public void Init(Transform origin)
        {
            _origin = origin;
        }
        public Collider[] GetCollider()
        {
            if (_colliders != null) return _colliders;

            else
            {
                var dir = _origin.rotation * _offset;
                ResetCollidersNextFrame();
                return _colliders = Physics.OverlapSphere(_origin.position + dir, _radius, _targetLayer);
            }
        }
        private async void ResetCollidersNextFrame()
        {
            await UniTask.DelayFrame(1);
            _colliders = null;
        }

        public bool IsHit()
        {
            return _isHit.Value = GetCollider().Length > 0;
        }

        [SerializeField]
        private bool _isDrawGizmo = true;
        [SerializeField]
        private Color _gizmoHitColor = Color.red;
        [SerializeField]
        private Color _gizmoNotHitColor = Color.blue;

        public void OnDrawGizmos(Transform origin)
        {
            if (_isDrawGizmo)
            {
                var dir = origin.rotation * _offset;

                if (Physics.OverlapSphere(origin.position + dir, _radius, _targetLayer).Length > 0)
                {
                    Gizmos.color = _gizmoHitColor;
                }
                else
                {
                    Gizmos.color = _gizmoNotHitColor;
                }
                Gizmos.DrawSphere(origin.position + dir, _radius);
            }
        }
    }
}