using UnityEngine;

namespace JimmyHaglund {
    public abstract class MonoCollection<T> : MonoBehaviour {
        protected T[] Items { get; private set; }
        private void Awake() {
            Items = GetComponentsInChildren<T>();
            OnAwake();
        }

        protected void OnAwake() { }
    }
}
