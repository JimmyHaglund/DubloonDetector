using System;
using UnityEngine;
using UnityEngine.Events;
using JimmyHaglund.Rapid;

namespace MetalDetector {
    public class Float2EventListener : MonoBehaviour, IEventListener<float, float> {
        [Serializable] public class UnityFloat2Event : UnityEvent<float, float> { }

        [SerializeField] private Float2Event _event = null;
        [SerializeField] private UnityFloat2Event _response = new UnityFloat2Event();

        private void OnEnable() {
            if (_event == null) return;
            _event.RegisterListener(this);
        }

        private void OnDisable() {
            if (_event == null) return;
            _event.UnRegisterListener(this);
        }

        public void OnEventRaised(float valueA, float valueB) {
            _response.Invoke(valueA, valueB);
        }
    }
}