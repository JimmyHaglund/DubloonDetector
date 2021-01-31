using System;
using UnityEngine;
using UnityEngine.Events;
using JimmyHaglund.Rapid;

namespace MetalDetector {
    public class FloatEventListener : ScriptableEventListener<float>{
        [Serializable] public class FloatUnityEvent : UnityEvent<float> { }

        [SerializeField] private FloatEvent _event;
        [SerializeField] private FloatUnityEvent _response;

        protected override ScriptableEvent<float> TargetEvent => _event;

        public override void OnEventRaised(float data) {
            _response.Invoke(data);
        }
    }
}