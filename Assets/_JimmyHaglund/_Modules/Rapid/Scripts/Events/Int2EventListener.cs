using System;
using UnityEngine;
using UnityEngine.Events;

namespace JimmyHaglund.Rapid {
    public class Int2EventListener : MonoBehaviour, IEventListener<int, int> {
        [Serializable] public class UnityInt2Event : UnityEvent<int, int> { }
        [SerializeField] private Int2Event _event = null;
        [SerializeField] private UnityInt2Event _response = new UnityInt2Event();

        private void OnEnable() {
            if (_event == null) return;
            _event.RegisterListener(this);
        }

        private void OnDisable() {
            if (_event == null) return;
            _event.UnRegisterListener(this);
        }

        public void OnEventRaised(int valueA, int valueB) {
            _response.Invoke(valueA, valueB);
        }
    }
}