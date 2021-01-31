using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UObject = UnityEngine.Object;

namespace JimmyHaglund {
    [Serializable]
    public class ObjectField {
        [SerializeField] protected UObject _valueObject = null;
        protected virtual Type ValueType => typeof(object);

#if (UNITY_EDITOR)
        [CustomPropertyDrawer(typeof(ObjectField), true)]
        public class InterfaceContainerDrawer : PropertyDrawer {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                var target = GetObjectFieldFromProperty(property);
                EditorGUI.BeginChangeCheck();
                target._valueObject = GenericField(position, property.displayName, target._valueObject, target.ValueType);
                if (EditorGUI.EndChangeCheck()) {
                    AssignMultiple(property.serializedObject.targetObject.GetType(), target._valueObject, property.propertyPath);
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }

            protected UObject GenericField(Rect position, string label, UObject displayValue, Type type) {
                label += string.Format(" ({0})", GetTypeName(type));
                UObject setObject = EditorGUI.ObjectField(position, label, displayValue, typeof(UObject), true) as UObject;
                if (setObject == displayValue) return displayValue;
                if (setObject == null) {
                    return null;
                }
                if (setObject.GetType() == typeof(GameObject)) {
                    var go = setObject as GameObject;
                    var obj = go.GetComponent(type);
                    if (obj != null) return obj;
                }
                if (type.IsInstanceOfType(setObject)) {
                    return setObject;
                }
                return displayValue;
            }

            private void AssignMultiple(Type fieldType, UObject value, string targetPropertyPath) {
                foreach(UObject targetObject in Selection.objects) {
                    var targetComponent = targetObject;
                    if (targetObject is GameObject) {
                        var targetGO = targetObject as GameObject;
                        targetComponent = targetGO.GetComponent(fieldType);
                    }
                    if (targetComponent.GetType() == fieldType) {
                        var targetObjectField = GetObjectFieldFromTarget(targetComponent, targetPropertyPath);
                        targetObjectField._valueObject = value;
                        EditorUtility.SetDirty(targetComponent);
                    }
                }
            }

            private string GetTypeName(Type type) {
                var name = type.Name;
                if (type.IsGenericType) {
                    var indexOfBacktick = name.IndexOf('`');
                    name = name.Remove(indexOfBacktick) + "<";
                    Type[] typeParameters = type.GetGenericArguments();
                    for(int n = 0; n < typeParameters.Length; n++) {
                        string typeParameterName = GetTypeName(typeParameters[n]);
                        name += n == 0 ? typeParameterName : ", " + typeParameterName;
                    }
                    name += ">";
                }
                return name;
            }

            private ObjectField GetObjectFieldFromProperty(SerializedProperty property) {
                var targetObject = property.serializedObject.targetObject;
                return GetObjectFieldFromTarget(targetObject, property.propertyPath);
            }

            private ObjectField GetObjectFieldFromTarget(UObject target, string propertyPath) {
                var targetObjectClassType = target.GetType();
                var field = targetObjectClassType.GetField(propertyPath, BindingFlags.Public | BindingFlags.Instance);
                if (field == null) {
                    field = targetObjectClassType.GetField(propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field == null || !Attribute.IsDefined(field, typeof(SerializeField))) {
                        return null;
                    }
                }
                return field.GetValue(target) as ObjectField;
            }
        }
#endif
    }

    [Serializable]
    public class InterfaceField<T> : ObjectField where T: class {
        private T _value;
        protected override Type ValueType => typeof(T);

        public override string ToString() {
            return _value != null ? _value.ToString() : "null";
        }

        public bool Equals(T other) {
            return _value == other;
        }

        public T Value {
            set {
                _value = value;
                var gen = _value as object;
                _valueObject = gen as UObject;
            }
            get {
                if (!Application.isPlaying) {
                    if (_value != _valueObject) {
                        _value = _valueObject as T;
                    }
                }
                if (_value == null && _valueObject != null) {
                    _value = _valueObject as T;
                }
                return _value;
            }
        }
    }
}