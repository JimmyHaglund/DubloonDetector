using UnityEngine;
using TMPro;
using JimmyHaglund;

namespace MetalDetector.Development {
    public class Float2Display : MonoBehaviour {
        [SerializeField] [ChildReferenceButton] private TMP_Text _valueText = null;
        [SerializeField] public string _text = "Value A: {0} | Value B: {1}";
        public void DisplayText(float valueA, float valueB) {
            _valueText.text = string.Format(_text, valueA, valueB);
        }
    }
}