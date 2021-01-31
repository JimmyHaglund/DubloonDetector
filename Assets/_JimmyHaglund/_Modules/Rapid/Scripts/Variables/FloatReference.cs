using System;
using UnityEditor;

namespace JimmyHaglund.Rapid {
    [Serializable]
    public class FloatReference : VariableReference<float, FloatVariable> { }
    
#if (UNITY_EDITOR)
    [CustomPropertyDrawer(typeof(FloatReference))]
    public class FloatReferenceDrawer : ReferenceDrawer { }
#endif
}
