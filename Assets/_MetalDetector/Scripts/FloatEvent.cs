using UnityEngine;
using JimmyHaglund.Rapid;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MetalDetector {
    [CreateAssetMenu(menuName ="JimmyHaglund/Rapid/Float Event")]
    public class FloatEvent : ScriptableEvent<float> {
#if (UNITY_EDITOR)
        [CustomEditor(typeof(FloatEvent))]
        public class FloatEventEditor : Editor {
            private FloatEvent _target;
            private GUILayoutOption _buttonHeight;
            private float _invokeValue = 0.0f;
            private void Awake() {
                _target = target as FloatEvent;
                _buttonHeight = GUILayout.Height(75);
            }

            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                _invokeValue = EditorGUILayout.FloatField("Invoke value A", _invokeValue);
                var buttonStyle = new GUIStyle(GUI.skin.button);
                buttonStyle.fontSize = 32;
                if (GUILayout.Button("Raise", buttonStyle, _buttonHeight)) {
                    _target.Raise(_invokeValue);
                }
            }
        }
#endif
    }
}