using System;
using UnityEditor;
using JimmyHaglund;

namespace MetalDetector {
    [Serializable] public class IntReference : VariableReference<int, IntVariable> { }

#if (UNITY_EDITOR)
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : ReferenceDrawer { }
#endif
}
