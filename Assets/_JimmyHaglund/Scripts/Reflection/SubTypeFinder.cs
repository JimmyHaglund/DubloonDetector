using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace JimmyHaglund.Reflection {
    /// <summary>
    /// Retreives all subclasses of a given type and returns them as a list.
    /// </summary>
    /// /// Based on code on Stack Overflow, authored by Repo Man (19-10-19). Modified for use in project.
    /// https://stackoverflow.com/questions/5411694/get-all-inherited-classes-of-an-abstract-class
    public static class SubtypeFinder {
        public static List<Type> GetSubClassTypes<BaseType>() {
            Type[] typeArray = Assembly.GetAssembly(typeof(BaseType)).GetTypes();
            IEnumerable<Type> typeCollection =
                from type in typeArray
                where (type.IsSubclassOf(typeof(BaseType)) || ContainsInterface<BaseType>(type)) &&
                ((type.IsClass && !type.IsAbstract) || type.IsValueType)
                select type;

            return typeCollection.ToList();
        }

        private static bool ContainsInterface<T>(System.Type target) {
            return target.GetInterfaces().Contains(typeof(T));
        }
    }
}
