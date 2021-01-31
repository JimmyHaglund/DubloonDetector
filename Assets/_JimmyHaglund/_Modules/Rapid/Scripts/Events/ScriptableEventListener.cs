using UnityEngine;

namespace JimmyHaglund.Rapid {
    public abstract class ScriptableEventListener<T> : MonoBehaviour, IEventListener<T> {
        protected abstract ScriptableEvent<T> TargetEvent { get; }
        
        private void OnEnable() {
            if (TargetEvent == null) return;
            TargetEvent.RegisterListener(this);
            AfterOnEnable();
        }
        protected virtual void AfterOnEnable() { }

        private void OnDisable() {
            if (TargetEvent == null) return;
            TargetEvent.UnRegisterListener(this);
            AfterOnDisable();
        }
        protected virtual void AfterOnDisable() { }

        public abstract void OnEventRaised(T data);
    }
}