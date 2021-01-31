using System.Collections;
using UnityEngine;
using TMPro;
using JimmyHaglund;

namespace MetalDetector.UserInterface {
    public class FloatTextDisplay : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] [ChildReferenceButton] private TMP_Text _textObject;
        [Header("Settings")]
        [SerializeField] private string _displayedText = "Float Value: {0}";
        [SerializeField] private float _displayedvalue = 0.0f;

        public void SetDisplayedValue(float value) {
            _displayedvalue = value;
            UpdateDisplayedText();
        }

        private void UpdateDisplayedText() {
            if (_textObject == null) return;
            _textObject.text = string.Format(_displayedText, _displayedvalue);
        }
#if UNITY_EDITOR
        private void OnValidate() {
            UpdateDisplayedText();
        }

#endif
    }
}