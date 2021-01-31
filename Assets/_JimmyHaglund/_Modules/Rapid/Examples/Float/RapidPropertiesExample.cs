#if (UNITY_EDITOR)
using UnityEngine;
using UnityEngine.UI;

namespace JimmyHaglund.Rapid.Development {
    [ExecuteInEditMode]
    public class RapidPropertiesExample : MonoBehaviour {
        [SerializeField] private FloatReference _floatValue;
        [SerializeField] [ChildReferenceButton] Text _valueDisplayText = null;

        private float FloatValue => _floatValue.Value;

        private void Update() {
            if (_valueDisplayText == null) return;
            _valueDisplayText.text = "Float value: " + FloatValue;
        } 
    }
}
#endif