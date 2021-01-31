using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

namespace JimmyHaglund.Behaviours.UserInterface {
    public class PipHealthBar : MonoBehaviour {
        [SerializeField] private Image _healthPipTemplate = null;
        [SerializeField] private HorizontalLayoutGroup _layoutGroup = null;
        [SerializeField] private TMP_Text _numericText = null;
        [SerializeField] private GameObject _numericTextBackground = null;
        [SerializeField] private Color _minDamageColor = Color.red;
        [SerializeField] private Color _maxDamageColor = Color.gray;
        [SerializeField] private Image _healthBarBackground = null;
        private int _currentHealth;
        private int _maxHealth;
        private List<Image> _pipImages = new List<Image>();
        private Color _defaultPipColor;

        public void Initialise(int currentHealth, int maxHealth) {
            _currentHealth = currentHealth;
            _maxHealth = maxHealth;
            GeneratePips();
            GetPipColor();
            // AdjustLayout();
            // _background = GetComponent<Image>();
            Fade();
        }

        public void SetHealth(int value) {
            for (var n = value; n < _currentHealth; n++) {
                if (_pipImages[n] != null) {
                    _pipImages[n].enabled = false;
                }
            }
            if (_numericText != null) {
                _numericText.text = value.ToString();
            }
            _currentHealth = value;
        }

        public void Highlight() {
            SetImageAlpha(_healthBarBackground, 1.0f);
            foreach (Image pipImage in _pipImages) {
                SetImageAlpha(pipImage, 1.0f);
            }
            if (_numericTextBackground != null) {
                _numericTextBackground.SetActive(true);
            }
        }

        public void Fade() {
            SetImageAlpha(_healthBarBackground, 0.2f);
            foreach (Image pipImage in _pipImages) {
                pipImage.color = _defaultPipColor;
                SetImageAlpha(pipImage, 0.2f);
            }
            if (_numericTextBackground != null) {
                _numericTextBackground.SetActive(false);
            }
        }

        public void DisplayPotentialDamage(int minDamage, int maxDamage) {
            for (int n = _currentHealth - 1, i = 0; i < maxDamage && n >= 0; n--, i++) {
                _pipImages[n].color = i < minDamage ? _minDamageColor : _maxDamageColor;
            }
        }

        private void AddImageColor(Image image, float red = 0.0f, float green = 0.0f,
            float blue = 0.0f, float alpha = 0.0f) {
            if (image == null) return;
            var color = image.color;
            color.r += red;
            color.g += green;
            color.b += blue;
            color.a += alpha;
            image.color += color;
        }

        private void SetImageAlpha(Image image, float alpha = 1.0f) {
            if (image == null) return;
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }

        private void GeneratePips() {
            if (_healthPipTemplate == null) return;
            for (int n = 0; n < _maxHealth; n++) {
                var pip = Instantiate(_healthPipTemplate, _healthPipTemplate.transform.parent);
                pip.gameObject.SetActive(true);
                pip.enabled = (n < _currentHealth);
                var pipImage = pip.GetComponent<Image>();
                if (pipImage == null) continue;
                _pipImages.Add(pipImage);
            }
        }

        private void GetPipColor() {
            if (_healthPipTemplate == null) return;
            var pipImage = _healthPipTemplate.GetComponent<Image>();
            if (pipImage == null) return;
            _defaultPipColor = pipImage.color;
        }

        private void AdjustLayout() {
            if (_layoutGroup == null) {
                _layoutGroup = GetComponent<HorizontalLayoutGroup>();
            }
            if (_layoutGroup == null) return;
            _layoutGroup.SetLayoutHorizontal();
            LayoutRebuilder.ForceRebuildLayoutImmediate(_layoutGroup.transform as RectTransform);
            _layoutGroup.enabled = false;
        }
    }
}