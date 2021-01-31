using UnityEngine;
using System.Collections.Generic;

namespace JimmyHaglund.Rapid {
    public class ScriptableEvent<T> : ScriptableObject {
        private List<IEventListener<T>> _listeners = new List<IEventListener<T>>();

        public void Raise(T data) {
            for (int i = _listeners.Count - 1; i >= 0; i--) {
                _listeners[i].OnEventRaised(data);
            }
        }

        public void RegisterListener(IEventListener<T> listener) {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void UnRegisterListener(IEventListener<T> listener) {
            _listeners.Remove(listener);
        }
    }
}