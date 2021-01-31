using System.Collections;
using UnityEngine;
using UnityEditor;

namespace JimmyHaglund {
    public class RequiredReferenceAttribute : PropertyAttribute {
#if UNITY_EDITOR
        [CustomPropertyDrawer(typeof(RequiredReferenceAttribute))]
        public class RequiredFieldAttributeDrawer : PropertyDrawer {
            public string WarningText { get; set; } = "<color=yellow><b>Warning!</b> {0} must be assigned for this script to work properly!</color>";

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                if (property.objectReferenceValue == null) {
                    var warningPosition = new Rect(position);
                    var halfHeight = position.height * 0.5f;
                    warningPosition.height -= halfHeight;
                    position.y += halfHeight;
                    position.height -= halfHeight;
                    DisplayWarningLabel();

                    void DisplayWarningLabel() {
                        var name = property.displayName;
                        var style = new GUIStyle();
                        style.richText = true;
                        EditorGUI.LabelField(warningPosition, string.Format(WarningText, name), style);
                    }
                }
                EditorGUI.PropertyField(position, property);
            }

            public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
                var baseHeight = base.GetPropertyHeight(property, label);
                if (property.objectReferenceValue == null) {
                    return baseHeight * 2;
                }
                return baseHeight;
            }
        }
#endif
    }
}