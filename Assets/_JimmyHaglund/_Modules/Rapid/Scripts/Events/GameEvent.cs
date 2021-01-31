using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

namespace JimmyHaglund.Rapid {
    [CreateAssetMenu(menuName = "JimmyHaglund/Rapid/Game Event")]
    public class GameEvent : ScriptableObject {
        private List<GameEventListener> _listeners = new List<GameEventListener>();

        public void Raise() {
            for (int i = _listeners.Count - 1; i >= 0; i--) {
                _listeners[i].OnEventRaised();
            }
        }

        public void RegisterListener(GameEventListener listener) {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void UnRegisterListener(GameEventListener listener) {
            _listeners.Remove(listener);
        }

#if (UNITY_EDITOR)
        [CustomEditor(typeof(GameEvent))]
        public class GameEventEditor : Editor {
            private GameEvent _target;
            private GUILayoutOption _buttonHeight;
            private void Awake() {
                _target = target as GameEvent;
                _buttonHeight = GUILayout.Height(75);
            }

            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                var style = new GUIStyle(GUI.skin.button);
                style.fontSize = 32;
                if (GUILayout.Button("Raise", style, _buttonHeight)) {
                    _target.Raise();
                }
            }
        }
#endif
    }
}