using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace JimmyHaglund {
#if (UNITY_EDITOR)
    [CustomEditor(typeof(ToggleGroup))]
    public class ToggleGroupAssignChildrenButton : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            if (GUILayout.Button("Assign child toggles to this group")) {
                var targetGroup = target as ToggleGroup;
                var childToggles = GetChildToggles();
                foreach (Toggle toggle in childToggles) {
                    toggle.group = targetGroup;
                    EditorUtility.SetDirty(toggle);
                }
            }
        }

        private Toggle[] GetChildToggles() {
            var targetGroup = target as ToggleGroup;
            return targetGroup.GetComponentsInChildren<Toggle>();
        }
    }
#endif
}
