using System;
using UnityEngine;
using UnityEngine.Events;

namespace JimmyHaglund.Rapid {
    public class IntEventListener : MonoBehaviour, IIntEventListener {
        [Serializable] public class UnityIntEvent : UnityEvent<int> { }
        [SerializeField] private IntEvent _event = null;
        [SerializeField] private UnityIntEvent _response = new UnityIntEvent();

        private void OnEnable() {
            if (_event == null) return;
            _event.RegisterListener(this);
        }

        private void OnDisable() {
            if (_event == null) return;
            _event.UnRegisterListener(this);
        }

        public void OnEventRaised(int value) {
            _response.Invoke(value);
        }
    }
}