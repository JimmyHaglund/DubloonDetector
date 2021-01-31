using System;
using UnityEngine;
using UnityEngine.Events;
using JimmyHaglund.Rapid;

namespace MetalDetector {
    public class Vector3EventListener : ScriptableEventListener<Vector3> {
        [Serializable] public class Vector3UnityEvent : UnityEvent<Vector3> { }

        [SerializeField] private Vector3Event _event;
        [SerializeField] private Vector3UnityEvent _response;

        protected override ScriptableEvent<Vector3> TargetEvent => _event;

        public override void OnEventRaised(Vector3 data) {
            _response.Invoke(data);
        }
    }
}