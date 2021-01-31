#if (UNITY_EDITOR)
using System;
using System.Reflection;
using UnityEditor;

namespace JimmyHaglund {
    public static class GetSerializedPropertyTarget {
        public static Type From(SerializedProperty property)  {
            var targetObject = property.serializedObject.targetObject;
            var targetObjectClassType = targetObject.GetType();
            var field = GetField(targetObjectClassType, property.propertyPath);
            return field?.FieldType;
        }

        private static FieldInfo GetField(Type fromType, string fieldPath) {
            var field = fromType.GetField(fieldPath, BindingFlags.Public | BindingFlags.Instance);
            if (field == null) {
                field = fromType.GetField(fieldPath, BindingFlags.NonPublic | BindingFlags.Instance);
                if (field == null) {
                    var parentType = fromType.BaseType;
                    return parentType == null ? null : GetField(parentType, fieldPath);
                }
                return field;
            }
            return null;
        }
    }
}
#endif