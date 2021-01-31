#if (UNITY_EDITOR)
using UnityEditor;
using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;
using UObject = UnityEngine.Object;

namespace JimmyHaglund {
    /// <summary>
    /// Editor with ability to create inspector fields automatically through reflection.
    /// Not compatible with all types, though most common C# simple types are accepted.
    /// </summary>
    public class UniversalEditor : Editor {
        /// <summary>
        /// Creates inspector field for most basic c# type and returns TRUE if inspector value was changed.
        /// </summary>
        /// <param name="label"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        /// Accepted types:
        /// byte
        /// sbyte
        /// short
        /// ushort
        /// int
        /// uint
        /// long
        /// float
        /// double
        /// bool
        /// string
        protected bool MakeInspectorField(string label, ref object value,
            System.Type containingObjectType = null, bool privateMember = false) {
            // Returns true if target property or field has the [SerializeField] attribute.
            bool HasSerializeField() {
                BindingFlags accessFlag = privateMember ? BindingFlags.NonPublic : BindingFlags.Public;
                if (containingObjectType != null) {
                    MemberInfo[] allMembers = containingObjectType.GetMembers();
                    MemberInfo[] memberInfos = containingObjectType.GetMember(label, BindingFlags.Instance | accessFlag);
                    foreach (MemberInfo memberInfo in memberInfos) {
                        SerializeField serializeFieldAttribute =
                            (memberInfo.GetCustomAttribute<SerializeField>(false));
                        if (serializeFieldAttribute != null) {
                            // Debug.Log("Serialize field attribute found.");
                            return true;
                        }
                    }
                    return false;
                }
                return true;
            }
            System.Type type = value?.GetType();
            if (!HasSerializeField()) return false;

            if (type == typeof(byte)) {
                byte oldValue = (byte)value;
                byte newValue = ByteField(label, (byte)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(sbyte)) {
                sbyte oldValue = (sbyte)value;
                sbyte newValue = SByteField(label, (sbyte)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(short)) {
                short oldValue = (short)value;
                short newValue = ShortField(label, (short)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(ushort)) {
                ushort oldValue = (ushort)value;
                ushort newValue = UShortField(label, (ushort)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(int)) {
                int oldValue = (int)value;
                int newValue = EditorGUILayout.IntField(label, (int)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(uint)) {
                uint oldValue = (ushort)value;
                uint newValue = UIntField(label, (uint)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(long)) {
                long oldValue = (long)value;
                long newValue = EditorGUILayout.LongField(label, (long)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(float)) {
                float oldValue = (float)value;
                float newValue = EditorGUILayout.FloatField(label, (float)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(double)) {
                double oldValue = (double)value;
                double newValue = EditorGUILayout.DoubleField(label, (double)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(bool)) {
                bool oldValue = (bool)value;
                bool newValue = EditorGUILayout.Toggle(label, (bool)value);
                value = newValue;
                return (oldValue != newValue);
            }
            if (type == typeof(string)) {
                string oldValue = (string)value;
                string newValue = EditorGUILayout.TextField(label, (string)value);
                value = newValue;
                return (oldValue != newValue);
            }
            return false;
        }

        /// <summary>
        /// Allows displaying any object in an inspector field, even non-mono based ones such as interfaces.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="label"></param>
        /// <param name="displayValue"></param>
        /// <returns></returns>
        /// Note that generics are usually not actually stored by Unity so it's all bull anyway.
        protected T GenericField<T>(string label, T displayValue = null) where T : class {
            var uObject = displayValue as UObject;
            UObject setObject = EditorGUILayout.ObjectField(label, uObject, typeof(UObject), true) as UObject;
            var gameObject = setObject as GameObject;
#pragma warning disable 253
            if (setObject == null || setObject == displayValue) return displayValue;
#pragma warning restore 253
            var typeObject = gameObject != null ? gameObject.GetComponent<T>() : setObject as T;
            if (typeObject != null) {
                EditorUtility.SetDirty(target);
                return typeObject;
            }
            return displayValue;
        }
        /*
        * This is an experiment to see if it would be possible to display pretty much anything in an 
        * inspector. Display works, assignment does not.
        protected object AnyField(string label, object displayValue = null) {
            var uObject = displayValue as UObject;
            UObject setObject = EditorGUILayout.ObjectField(label, uObject, typeof(UObject), true) as UObject;
            if (setObject == null) return displayValue;
            return uObject;
        }
        */

        protected byte ByteField(string label, byte value) {
            return (byte)Mathf.Clamp(EditorGUILayout.IntField(label, (int)value),
                byte.MinValue, byte.MaxValue);
        }

        protected sbyte SByteField(string label, sbyte value) {
            return (sbyte)Mathf.Clamp(EditorGUILayout.IntField(label, (int)value),
                sbyte.MinValue, sbyte.MaxValue);
        }

        protected short ShortField(string label, short value) {
            return (short)Mathf.Clamp(EditorGUILayout.IntField(label, (int)value),
                short.MinValue, short.MaxValue);
        }

        protected ushort UShortField(string label, ushort value) {
            return (ushort)Mathf.Clamp(EditorGUILayout.IntField(label, (int)value),
                ushort.MinValue, ushort.MaxValue);
        }

        protected uint UIntField(string label, uint value) {
            return (uint)Mathf.Clamp(EditorGUILayout.IntField(label, (int)value),
                uint.MinValue, uint.MaxValue);
        }

        protected bool UnfoldProperties(object target) {
            List<PropertyInfo> properties =
                new List<PropertyInfo>(target.GetType().GetProperties());
            bool targetChanged = false;
            // Display  & edit properties
            foreach (PropertyInfo info in properties) {
                object value = info.GetValue(target);
                bool valueChanged = MakeInspectorField(info.Name, ref value, target.GetType());
                if (valueChanged) {
                    info.SetValue(target, value);
                    targetChanged = true;
                }
            }
            return targetChanged;
        }

        protected bool UnfoldPublicFields(object target) {
            List<FieldInfo> fields = new List<FieldInfo>(target.GetType().
                GetFields(BindingFlags.Public | BindingFlags.Instance));
            bool targetChanged = false;
            // Display  & edit properties
            foreach (FieldInfo info in fields) {
                object value = info.GetValue(target);
                bool valueChanged = MakeInspectorField(info.Name, ref value, target.GetType(), false);
                if (valueChanged) {
                    info.SetValue(target, value);
                    targetChanged = true;
                }
            }
            return targetChanged;
        }

        protected bool UnfoldPrivateFields(object target) {
            List<FieldInfo> fields = new List<FieldInfo>(target.GetType().
                GetFields(BindingFlags.NonPublic | BindingFlags.Instance));
            bool targetChanged = false;

            foreach (FieldInfo info in fields) {
                // Don't show auto property fields
                if (info.Name.Contains("k__BackingField")) continue;

                object value = info.GetValue(target);
                bool valueChanged = MakeInspectorField(info.Name, ref value, target.GetType(), true);
                if (valueChanged) {
                    info.SetValue(target, value);
                    targetChanged = true;
                }
            }
            return targetChanged;
        }
    }

    [Serializable]
    public class Container<T> : UObject, IEquatable<T> {
        public T Value { get; set; }

        public bool Equals(T other) {
            return Value.Equals(other);
        }
    }
}
#endif