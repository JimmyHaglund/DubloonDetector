using UnityEngine;

namespace JimmyHaglund.Rapid {
    public class TransformListSetItem : MonoBehaviour {
        [SerializeField] private TransformListSet _listSet = null;

        private void OnEnable() {
            if (_listSet == null) return;
            _listSet.Add(transform);
            AfterOnEnable();
        }

        private void OnDisable() {
            if (_listSet == null) return;
            _listSet.Remove(transform);
            AfterOnDisable();
        }

        protected virtual void AfterOnEnable() { }
        protected virtual void AfterOnDisable() { }
    }
}