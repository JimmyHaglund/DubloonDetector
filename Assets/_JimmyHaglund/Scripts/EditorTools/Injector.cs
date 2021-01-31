using System;
using System.Reflection;

namespace JimmyHaglund {
    public class Inject : Attribute { }
    public static class Injector {
        public static void Inject<T> (object target, T value) {
            InjectFields(target, value);
            InjectProperties(target, value);
        }

        public static void InjectFields<T> (object target, T value) {
            var fields = target.GetType().GetFields();
            foreach (FieldInfo field in fields) {
                var injectAttribute = field.GetCustomAttribute<Inject>();
                if (injectAttribute == null) continue;
                Inject(target, field, value);
            }
        }

        public static void InjectProperties<T> (object target, T value) {
            var properties = target.GetType().GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo property in properties) {
                var injectAttribute = property.GetCustomAttribute<Inject>();
                if (injectAttribute == null) continue;
                Inject(target, property, value);

            }
        }

        public static void Inject<T> (object target, FieldInfo field, T value) {
            if (field.FieldType == typeof(T)) {
                field.SetValue(target, value);
            }
        }

        public static void Inject<T> (object target, PropertyInfo property, T value) {
            if (property.PropertyType == typeof(T)) {
                property.SetValue(target, value);
            }
        }
    }
}
