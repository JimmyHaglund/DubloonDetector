using UnityEngine;
using UnityEditor;

namespace JimmyHaglund.Rapid {
    [CreateAssetMenu(menuName = "JimmyHaglund/Rapid/Transform List Set")]
    public class TransformListSet : ListSet<Transform> {

#if (UNITY_EDITOR)
        [CustomEditor(typeof(TransformListSet))]
        public class ListSetEditor : Editor {
            private bool _dropDownOpen = false;
            private TransformListSet _target;
            private GUIStyle _titleStyle;
            private bool _initialised = false;

            public override void OnInspectorGUI() {
                if (!_initialised) Initialise();
                base.OnInspectorGUI();
                if (_target._items.Count == 0) return;
                var label = "ITEMS: " + _target._items.Count;
                EditorGUILayout.LabelField(label, _titleStyle);
                foreach (Transform item in _target._items) {
                    EditorGUILayout.LabelField(item.name);
                }
            }

            private void Initialise() {
                _target = target as TransformListSet;
                _titleStyle = new GUIStyle(GUI.skin.label);
                _titleStyle.fontStyle = FontStyle.Bold;
            }
        }
#endif
    }
}