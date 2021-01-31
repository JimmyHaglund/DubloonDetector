using System.Collections;
using UnityEngine;
using JimmyHaglund.Rapid;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MetalDetector {
    [CreateAssetMenu(menuName = "JimmyHaglund/Rapid/Bool Event")]
    public class BoolEvent : ScriptableEvent<bool> {

#if (UNITY_EDITOR)
        [CustomEditor(typeof(BoolEvent))]
        public class BoolEventEditor : Editor {
            private BoolEvent _target;
            private GUILayoutOption _buttonHeight;
            private bool _invokeValue = false;
            private void Awake() {
                _target = target as BoolEvent;
                _buttonHeight = GUILayout.Height(75);
            }

            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                _invokeValue = EditorGUILayout.Toggle("Invoke value", _invokeValue);
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