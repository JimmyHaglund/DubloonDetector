using System.Collections;
using UnityEngine;

namespace JimmyHaglund {
    public class ScriptableVariable<VariableType> : ScriptableObject {
        [SerializeField] public VariableType Value;
    }
}