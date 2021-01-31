using UnityEngine;
using System.Collections.Generic;
using JimmyHaglund.Rapid;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MetalDetector {
    [CreateAssetMenu(menuName = "JimmyHaglund/Rapid/Float 2 Event")]
    public class Float2Event : ScriptableObject {
        private List<IEventListener<float, float>> _listeners = new List<IEventListener<float, float>>();

        public void Raise(float valueA, float valueB) {
            for (int i = _listeners.Count - 1; i >= 0; i--) {
                _listeners[i].OnEventRaised(valueA, valueB);
            }
        }

        public void RegisterListener(IEventListener<float, float> listener) {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void UnRegisterListener(IEventListener<float, float> listener) {
            _listeners.Remove(listener);
        }

#if (UNITY_EDITOR)
        [CustomEditor(typeof(Float2Event))]
        public class Float2EventEditor : Editor {
            private Float2Event _target;
            private GUILayoutOption _buttonHeight;
            private float _invokeValueA = 0.0f;
            private float _invokeValueB = 0.0f;
            private void Awake() {
                _target = target as Float2Event;
                _buttonHeight = GUILayout.Height(75);
            }

            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                _invokeValueA = EditorGUILayout.FloatField("Invoke value A", _invokeValueA);
                _invokeValueB = EditorGUILayout.FloatField("Invoke value B", _invokeValueB);
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