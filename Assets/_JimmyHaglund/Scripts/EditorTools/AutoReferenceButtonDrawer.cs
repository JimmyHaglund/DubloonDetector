using UnityEngine;
using UnityEditor;

namespace JimmyHaglund {
#if (UNITY_EDITOR)
        public abstract class AutoReferenceButtonDrawer : PropertyDrawer {
            protected abstract PropertyExtraButton _button { get; set; }

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                EditorGUI.PropertyField(position, property);
                if (property.propertyType != SerializedPropertyType.ObjectReference) return;
                DrawAssignButton(_button.SetPosition(position).LoadTexture(), property);
            }

            private void DrawAssignButton(PropertyExtraButton button, SerializedProperty property) {
                if (GUI.Button(button.Position, button.Content, button.Style)) {
                    AssignObjectReference(property);
                }
            }

            protected abstract void AssignObjectReference(SerializedProperty property);
        }
#endif
}