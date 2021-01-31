using System;
using UnityEngine;
using UnityEngine.Events;

namespace JimmyHaglund.Rapid {
    public class StringEventListener : ScriptableEventListener<string>, IEventListener<string> {
        [SerializeField] private StringEvent _event;
        [SerializeField] private StringUnityEvent _response;

        protected override ScriptableEvent<string> TargetEvent => _event;

        public override void OnEventRaised(string arg) {
            _response.Invoke(arg);
        }

        [Serializable] public class StringUnityEvent : UnityEvent<string> { }
    }
}