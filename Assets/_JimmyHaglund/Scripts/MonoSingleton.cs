using UnityEngine;

namespace JimmyHaglund {
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour {
        private static T _instance;
        protected static T Instance {
            get {
                if (_instance == null) {
                    _instance = GameObject.FindObjectOfType<T>();
                    if (_instance == null) {
                        GameObject newObject = new GameObject();
                        newObject.name = typeof(T).ToString();
                        _instance = newObject.AddComponent<T>();
                    }
                }
                (_instance as MonoSingleton<T>).OnCreated();
                return _instance;
            }
        }
        /// <summary>
        /// Singleton pattern, assigns self as instance and ensures only one instance exists.
        /// </summary>
        private void OnEnable() {
            if (_instance != null && _instance != this) {
                Debug.Log("Doublet created for " + this.GetType().ToString());
                Destroy(this);
                return;
            }
            Debug.Log("Singleton created for " + this.GetType().ToString());
            _instance = this as T;
            OnOnEnable();
        }
        
        private void OnDisable() {
            if (_instance == this) {
                _instance = null;
                OnOnDisable();
            }
        }
        protected virtual void OnOnEnable() { }
        protected virtual void OnOnDisable() { }
        protected virtual void OnCreated() { }
    }
}
