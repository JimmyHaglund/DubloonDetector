using System;
using UnityEngine;
using UnityEngine.Events;

namespace MetalDetector {
    public class BoolInverter : MonoBehaviour {
        [Serializable] public class BoolUnityEvent : UnityEvent<bool> { }

        [SerializeField] private BoolUnityEvent _response;

        public void Invert(bool value) {
            _response.Invoke(!value);
        }
    }
}