using UnityEngine;
using JimmyHaglund.Rapid;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MetalDetector {
    [CreateAssetMenu(menuName = "JimmyHaglund/Rapid/Float3 Event")]
    public class Vector3Event : ScriptableEvent<Vector3> {
#if (UNITY_EDITOR)
        [CustomEditor(typeof(Vector3Event))]
        public class FloatEventEditor : Editor {
            private Vector3Event _target;
            private GUILayoutOption _buttonHeight;
            private Vector3 _invokeValue = default;
            private void Awake() {
                _target = target as Vector3Event;
                _buttonHeight = GUILayout.Height(75);
            }

            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                _invokeValue = EditorGUILayout.Vector3Field("Invoke value", _invokeValue);
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