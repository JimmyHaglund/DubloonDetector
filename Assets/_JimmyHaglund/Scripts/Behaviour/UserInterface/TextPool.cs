using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

namespace JimmyHaglund.Behaviours.UserInterface {
    public class TextPool : MonoBehaviour {
        [SerializeField] private TMP_Text _textObjectTemplate = null;
        [SerializeField] private int _maxActiveTexts = 10;
        [SerializeField] private List<TMP_Text> _texts = new List<TMP_Text>();
        [SerializeField] private float _textFadeTime = 5.0f;
        private int _activeTexts = 0;
        private float _fadeTimer;
        private void Awake() {
            var texts = new List<TMP_Text>();
            while (texts.Count < _maxActiveTexts) {
                texts.Add(InstantiateText(_textObjectTemplate));
            }
            for (int i = texts.Count - 1; i >= 0; i--) {
                _texts.Add(texts[i]);
            }
            enabled = _activeTexts > 0;
        }

        public void AddText(string text) {
            if (_activeTexts < _maxActiveTexts) {
                ++_activeTexts;
            }
            string lastText;
            for (int n = 0; n < _texts.Count && n < _activeTexts; n++) {
                lastText = _texts[n].text;
                _texts[n].text = text;
                text = lastText;
            }
            _texts[_activeTexts - 1].gameObject.SetActive(true);
            _fadeTimer = 0.0f;
            enabled = true;
        }

        private void Update() {
            _fadeTimer += Time.deltaTime;
            if (_fadeTimer > _textFadeTime) {
                _fadeTimer *= 0.8f;
                if (_activeTexts <= 0) {
                    _activeTexts = 0;
                    enabled = false;
                    return;
                }
                StartCoroutine(CO_FadeText(_texts[_activeTexts-- - 1]));
            }
        }

        private IEnumerator CO_FadeText(TMP_Text text) {
            text.gameObject.SetActive(false);
            
            yield return null;
        }
    
        private TMP_Text InstantiateText(TMP_Text template) {
            var newText = Instantiate(template, template.transform.parent);
            newText.gameObject.SetActive(false);
            return newText;
        }
    }
}