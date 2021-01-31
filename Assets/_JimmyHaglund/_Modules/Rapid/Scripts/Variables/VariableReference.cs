using System;
using UnityEngine;

namespace JimmyHaglund {
    [Serializable]
    public class VariableReference<VariableType, ReferenceType> where ReferenceType : ScriptableVariable<VariableType> {
        [SerializeField] private bool _useConstant = false;
        [SerializeField] private ReferenceType _variable = default;
        [SerializeField] private VariableType _constantValue = default;

        public VariableType Value => !_useConstant && _variable != null ? _variable.Value : _constantValue;
    }
}