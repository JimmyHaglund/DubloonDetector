using UnityEngine;
using UnityEngine.UI;

namespace JimmyHaglund.Behaviours.UserInterface {
    [AddComponentMenu("UI/*Color Toggle", 31)]
    public class ColorToggle : Toggle {
        private Color _defaultColor = Color.white;

        protected override void Awake() {
            base.Awake();
            if (!Application.isPlaying) return;
            GetNormalColor();
            onValueChanged.AddListener(SetNormalColor);
        }

        protected override void Start() {
            base.Start();
            if (!Application.isPlaying) return;
            DetermineColor(isOn);
        }

        private void DetermineColor(bool value) {
            SetNormalColor(value);
        }

        private void GetNormalColor() {
            _defaultColor = colors.normalColor;
        }

        private void SetNormalColor(bool on) {
            var toggleColors = colors;
            toggleColors.normalColor = isOn ? colors.selectedColor : _defaultColor;
            colors = toggleColors;
        }
    }
}