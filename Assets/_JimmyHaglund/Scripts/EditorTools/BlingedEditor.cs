#if (UNITY_EDITOR)
using UnityEditor;
using UnityEngine;

namespace JimmyHaglund {
    public class BlingedEditor<ClassType> : Editor where ClassType : Object {
        protected ClassType _target;
        private void Awake() {
            _target = target as ClassType;
            AfterAwake();
        }
        protected virtual void AfterAwake() { }
    }
}
#endif