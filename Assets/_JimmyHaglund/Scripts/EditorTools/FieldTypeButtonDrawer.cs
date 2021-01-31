#if (UNITY_EDITOR)
using UnityEngine;
using UnityEditor;

namespace JimmyHaglund {
    public class FieldTypeButton : PropertyAttribute {
        [CustomPropertyDrawer(typeof(FieldTypeButton))]
        public class FieldTypeButtonDrawer : PropertyDrawer {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                var buttonPosition = ButtonRect(position);
                var fieldPosition = FieldRect(position);
                MakeButton(buttonPosition, property);
                EditorGUI.PropertyField(fieldPosition, property);
            }

            private void MakeButton(Rect position, SerializedProperty property) {
                if (property.propertyType != SerializedPropertyType.ObjectReference) return;
                if (GUI.Button(position, "Log type")) {
                    Debug.Log("Property type: " + property.type + " Enum value: " + property.propertyType.ToString());
                }
            }

            private Rect ButtonRect(Rect position) {
                var w = position.width;
                var h = position.height;
                var x0 = position.x;
                var y0 = position.y;
                return new Rect(x0, y0, w * 0.2f - 10, h);
            }

            private Rect FieldRect(Rect position) {
                var w = position.width;
                var h = position.height;
                var x0 = position.x;
                var y0 = position.y;
                return new Rect(x0 + w * 0.2f + 10, y0, w * 0.8f, h);
            }
        }
    }
}
#endif