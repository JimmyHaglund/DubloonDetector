using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JimmyHaglund;
using JimmyHaglund.Rapid;

namespace MetalDetector {
    public class Treasure : TransformListSetItem, IEventListener<Vector3> {
        [Header("Dependencies")]
        [SerializeField] private Vector3Event _onDugEvent = null;
        [SerializeField] private IntEvent _setScoreEvent = null;
        [SerializeField] private IntVariable _dubloonCount = null;
        [SerializeField] [ChildReferenceButton] private MeshRenderer _renderer;
        [Header("Settings")]
        [SerializeField] private bool _autoPickWhenDugUp = true;
        [SerializeField] private LayerMask _digLayer = default;
        [SerializeField] private int _dubloonWorth = 1;

        private MaterialPropertyBlock _propertyBlock;
        private bool _collected = false;

        private int DubloonCount {
            get {
                return _dubloonCount.Value;
            }
            set {
                _dubloonCount.Value = value;
            }
        }

        private void Awake() {
            _propertyBlock = new MaterialPropertyBlock();
        }

        protected override void AfterOnEnable() {
            if (_autoPickWhenDugUp && _onDugEvent != null) {
                _onDugEvent.RegisterListener(this);
            }
        }

        protected override void AfterOnDisable() {
            if (_autoPickWhenDugUp && _onDugEvent != null) {
                _onDugEvent.UnRegisterListener(this);
            }
        }

        public void OnEventRaised(Vector3 digPosition) {
            if (Vector3.SqrMagnitude(digPosition - transform.position) > 1.0f) return;
            PickupIfAboveDigLayer();
        }

        private void PickupIfAboveDigLayer() {
            if (_collected) return;
            if (Physics.Raycast(transform.position + Vector3.up * 2, Vector3.down, 2.0f, _digLayer.value)) {
                return;
            }
            _collected = true;
            DubloonCount += _dubloonWorth;
            if (_setScoreEvent == null) {
                Debug.Log("Gained score: " + DubloonCount);
            } else {
                _setScoreEvent.Raise(DubloonCount);
            }
            StartCoroutine(CO_AnimatePickup());
        }

        private IEnumerator CO_AnimatePickup() {
            for (int n = 0; n < 100; n++) {
                yield return null;
                transform.position += Vector3.up * 0.01f;
                // var alpha = (float)n / 100.0f;
                // SetRendererMaterialAlpha(alpha);
            }
            Destroy(gameObject);
        }

        private void SetRendererMaterialAlpha(float alpha) {
            if (_renderer == null) return;
            _renderer.GetPropertyBlock(_propertyBlock);
            var color = _propertyBlock.GetColor("_Color");
            color.a = alpha;
            _propertyBlock.SetColor("_Color", color);
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}