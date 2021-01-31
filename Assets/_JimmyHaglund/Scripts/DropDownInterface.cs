using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UObject = UnityEngine.Object;

namespace JimmyHaglund {
    [Serializable]
    public abstract class DropDownInterface {
        [SerializeField] protected string _valueAsString = "";

        protected virtual string[] GetClassList() => new string[] { "None" };
        protected virtual void AssignValueIndex(int index) { }
        protected virtual void AssignValue(Type type) { }
        protected virtual void TryLoadFromString() { }

#if (UNITY_EDITOR)
        [CustomPropertyDrawer(typeof(DropDownInterface), true)]
        public class DropDownInterfacePropertyDrawer : PropertyDrawer {
            int _selectedIndex = 0;
            bool _initialised = false;
            string[] _popupOptions = null;
            private DropDownInterface _target = null;

            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
                if (!_initialised) Initialise(property);
                
                var dropDownInterface = GetInstanceFromProperty(property);
                if (dropDownInterface == null) return;
                EditorGUI.BeginChangeCheck();
                _selectedIndex = EditorGUI.Popup(position, _selectedIndex, _popupOptions);
                if (EditorGUI.EndChangeCheck()) {
                    _target.AssignValueIndex(_selectedIndex);
                    EditorUtility.SetDirty(property.serializedObject.targetObject);
                }
            }

            private DropDownInterface GetInstanceFromProperty(SerializedProperty property) {
                var targetObject = property.serializedObject.targetObject;
                var targetObjectClassType = targetObject.GetType();
                var field = targetObjectClassType.GetField(property.propertyPath, BindingFlags.Public | BindingFlags.Instance);
                if (field == null) {
                    field = targetObjectClassType.GetField(property.propertyPath, BindingFlags.NonPublic | BindingFlags.Instance);
                    if (field == null || !Attribute.IsDefined(field, typeof(SerializeField))) {
                        return null;
                    }
                }
                return field.GetValue(targetObject) as DropDownInterface;
            }

            private void Initialise(SerializedProperty property) {
                _target = GetInstanceFromProperty(property);
                _popupOptions = _target.GetClassList();
                _selectedIndex = GetIndexFromTarget();
                _initialised = true;
            }

            private int GetIndexFromTarget() {
                var valueAsString = _target._valueAsString;
                for(int n = 0; n < _popupOptions.Length; n++) {
                    if (_popupOptions[n] == valueAsString) return n;
                }
                return 0;
            }
        }
#endif

    }

    [Serializable]
    public class DropDownInterface<InterfaceType> : DropDownInterface {
        private InterfaceType _value;
        private Type[] _implementingTypes = null;
        
        public InterfaceType Value {
            get {
                if (_value == null) {
                    if (_implementingTypes == null) GetImplementingTypes();
                    TryLoadFromString();
                }
                return _value;
            }
        }

        protected override string[] GetClassList() {
            if (_implementingTypes == null) {
                _implementingTypes = FindImplementingTypes();
            }
            var classList = new string[_implementingTypes.Length];
            for (int n = 0; n < classList.Length; n++) {
                classList[n] = RemoveBefore(_implementingTypes[n].ToString(), '.');
            }
            return classList;
        }

        protected override void AssignValueIndex(int index) {
            if (index > _implementingTypes.Length || index < 0) return;
            var targetType = _implementingTypes[index];
            AssignValue(targetType);
        }

        protected override void AssignValue(Type type) {
            if (!type.IsClass || !TypeIsCompatibleWithInterface(type)) return;

            _value = (InterfaceType)Activator.CreateInstance(type);
            _valueAsString = RemoveBefore(type.ToString(), '.');
        }

        protected override void TryLoadFromString() {
            if (_valueAsString == "") return;
            var types = GetImplementingTypes();
            foreach (Type type in types) {
                if (type.ToString() == _valueAsString) {
                    if (!type.IsClass) {
                        _valueAsString = "";
                        return;
                    }
                    var newInstance = Activator.CreateInstance(type);
                    _value = (InterfaceType)newInstance;
                    return;
                }
            }
        }

        private string RemoveBefore(string target, char divider) {
            var dividerIndex = target.Length - 1;
            for (var i = target.Length - 1; i > 0; i--) {
                if (target[i] == divider) {
                    return target.Substring(i + 1);
                }
            }
            return target;
        }

        private Type[] GetImplementingTypes() {
            if (_implementingTypes == null) {
                _implementingTypes = FindImplementingTypes();
            }
            return _implementingTypes;
        }

        private Type[] FindImplementingTypes() {
            var targetType = typeof(InterfaceType);
            var implementingTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterfaces().Contains(targetType) && !(typeof(UObject).IsAssignableFrom(type)));
            return implementingTypes.ToArray();
        }

        private bool TypeIsCompatibleWithInterface(Type type) {
            var targetType = typeof(InterfaceType);
            return type.GetInterfaces().Contains(targetType);
        }
    }
}