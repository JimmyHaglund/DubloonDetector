using UnityEngine;
using UnityEditor;

namespace JimmyHaglund {
    public class SceneReferenceButton : PropertyAttribute {
#if (UNITY_EDITOR)
        [CustomPropertyDrawer(typeof(SceneReferenceButton))]
        public class SceneReferenceDrawer : AutoReferenceButtonDrawer {
            private const string TEXTUREID = "SceneReferenceButtonTexture";
            private const string TOOLTIP = "Find an object in the active scene and assign it to this field";
            protected override PropertyExtraButton _button { get; set; } = new PropertyExtraButton(TEXTUREID, TOOLTIP);

            protected override void AssignObjectReference(SerializedProperty property) {
                AssignObjectReferenceFromScene(property);
            }

            private void AssignObjectReferenceFromScene(SerializedProperty property) {
                var targetType = GetSerializedPropertyTarget.From(property);
                property.objectReferenceValue = GameObject.FindObjectOfType(targetType);
                property.serializedObject.ApplyModifiedProperties();
            }
        }
#endif
    }
}