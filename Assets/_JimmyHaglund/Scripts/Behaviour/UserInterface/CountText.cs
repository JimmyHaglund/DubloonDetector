using UnityEngine;
using TMPro;

namespace JimmyHaglund.Behaviours.UserInterface {
    public class CountText : MonoBehaviour {
        [SerializeField] private string _prefix = "";
        [SerializeField] private string _suffix = "";
        [SerializeField] private int _number = 0;
        [SerializeField] [ChildReferenceButton] TMP_Text _text = null;

        public int Number {
            get => _number;
            set {
                _number = value;
                SetTextNumber(_number);
            }
        }

        public void Add(int value) {
            Number += value;
        }

        public void Subtract(int value) {
            Number -= value;
        }

        private void SetTextNumber(int number) {
            if (_text == null) return;
            _text.text = _prefix + _number + _suffix;
        }

#if (UNITY_EDITOR)
        private void OnValidate() {
            SetTextNumber(_number);
        }
#endif
    }
}
