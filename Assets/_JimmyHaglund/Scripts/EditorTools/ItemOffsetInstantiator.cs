#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

namespace JimmyHaglund {
    public class ItemOffsetInstantiator : EditorWindow {
        private GameObject _baseObject = null;
        private int _cloneNumber = 0;
        private Vector3 _offset = default;
        private Vector3 _offsetRotation = default;
        private bool _parentToPrevious = false;

        [MenuItem("Tools/Item Cloner")]
        public static void ShowWindow() {
            EditorWindow.GetWindow(typeof(ItemOffsetInstantiator), false, "Item Cloner");
        }

        private void OnGUI() {
            _baseObject = EditorGUILayout.ObjectField("Base object", _baseObject, typeof(GameObject), true) as GameObject;
            _cloneNumber = EditorGUILayout.IntField("Number of clones", _cloneNumber);
            _parentToPrevious = EditorGUILayout.Toggle("Parent to previous", _parentToPrevious);
            _offset = EditorGUILayout.Vector3Field("Position offset", _offset);
            _offsetRotation = EditorGUILayout.Vector3Field("Rotation offset", _offsetRotation);

            if (GUILayout.Button("Create!") && _baseObject != null) {
                GameObject root = GameObject.Instantiate(_baseObject, null);
                GameObject lastClone = root;
                root.transform.position = _baseObject.transform.position + _offset;
                root.transform.Rotate(_offsetRotation);
                root.name = _baseObject.name + " (0)";

                for (int n = 1; n < _cloneNumber; n++) {
                    GameObject clone = GameObject.Instantiate(_baseObject, _parentToPrevious ? lastClone.transform : null);
                    clone.transform.position = lastClone.transform.position + _offset;
                    clone.transform.Rotate(_offsetRotation);
                    clone.name = string.Format(_baseObject.name + " ({0})", n.ToString());
                    lastClone = clone;
                }
                if (_parentToPrevious) {
                    root.transform.parent = _baseObject.transform;
                }
            }
        }
    }
}
#endif