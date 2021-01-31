using UnityEngine;
using UnityEditor;

namespace JimmyHaglund {
    public class ChildReferenceButton : PropertyAttribute {
#if (UNITY_EDITOR)
        [CustomPropertyDrawer(typeof(ChildReferenceButton))]
        public class ChildReferenceDrawer : AutoReferenceButtonDrawer {
            private const string TEXTUREID = "ChildReferenceButtonTexture";
            private const string TOOLTIP = "Find a child of this object and assign it to this field.";
            protected override PropertyExtraButton _button { get; set; } = new PropertyExtraButton(TEXTUREID, TOOLTIP);

            protected override void AssignObjectReference(SerializedProperty property) {
                AssignObjectFieldFromChildren(property);
            }

            private void AssignObjectFieldFromChildren(SerializedProperty property) {
                var targetType = GetSerializedPropertyTarget.From(property);
                var targetObject = property.serializedObject.targetObject as Component;
                property.objectReferenceValue = targetObject.GetComponentInChildren(targetType);
                property.serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}