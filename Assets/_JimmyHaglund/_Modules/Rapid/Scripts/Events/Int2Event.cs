using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace JimmyHaglund.Rapid {
    [CreateAssetMenu(menuName = "JimmyHaglund/Rapid/Int2 Event")]
    public class Int2Event : ScriptableObject {
        private List<IEventListener<int, int>> _listeners = new List<IEventListener<int, int>>();

        public void Raise(int valueA, int valueB) {
            for (int i = _listeners.Count - 1; i >= 0; i--) {
                _listeners[i].OnEventRaised(valueA, valueB);
            }
        }

        public void RegisterListener(IEventListener<int, int> listener) {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void UnRegisterListener(IEventListener<int, int> listener) {
            _listeners.Remove(listener);
        }


#if (UNITY_EDITOR)
        [CustomEditor(typeof(Int2Event))]
        public class Int2EventEditor : Editor {
            private Int2Event _target;
            private GUILayoutOption _buttonHeight;
            private int _invokeValueA = 0;
            private int _invokeValueB = 0;
            private void Awake() {
                _target = target as Int2Event;
                _buttonHeight = GUILayout.Height(75);
            }

            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                _invokeValueA = EditorGUILayout.IntField("Invoke value A", _invokeValueA);
                _invokeValueB = EditorGUILayout.IntField("Invoke value B", _invokeValueB);
                var buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.fontSize = 32;
                if (GUILayout.Button("Raise", buttonStyle, _buttonHeight)) {
                    _target.Raise(_invokeValueA, _invokeValueB);
                }
            }
        }
#endif
    }
}