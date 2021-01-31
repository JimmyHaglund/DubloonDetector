using System.Collections;
using UnityEngine;
using TMPro;
using JimmyHaglund;

namespace MetalDetector {
    public class IntVariableTextDisplay : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] [ChildReferenceButton] private TMP_Text _targetText = null;
        [Header("Settings")]
        [SerializeField] private string _text = "Value: {0}";

        public void SetDisplayValue(int value) {
            if (_targetText == null) return;
            _targetText.text = string.Format(_text, value);
        }
    }
}