using System;
using UnityEditor;

namespace JimmyHaglund.Rapid {
    [Serializable]
    public class BoolReference : VariableReference<bool, BoolVariable> { }

#if (UNITY_EDITOR)
    [CustomPropertyDrawer(typeof(BoolReference))]
    public class BoolReferenceDrawer : ReferenceDrawer { }
#endif
}
