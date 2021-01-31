using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using JimmyHaglund;
using UnityEngine.Events;

namespace MetalDetector {
    public class TransitionHandler : MonoBehaviour {
        [SerializeField] [SceneReferenceButton] private Image _fadeImage;
        [SerializeField] private UnityEvent _onTransition;
        [SerializeField] private float _fadeTime = 0.5f;
        [SerializeField] private float _blackTime = 0.2f;
        private float _age = 0.0f;

        private void Start() {
            StartCoroutine(CO_UnloadAfterTime(_fadeTime));
            StartCoroutine(CO_TransitionAfterTime(_fadeTime + _blackTime * 0.5f));
            StartCoroutine(CO_UnloadAfterTime(_fadeTime * 2 + _blackTime));
        }

        private void Update() {
            _age += Time.deltaTime;
            var targetAlpha = GetAlpha(_age);
            var c = _fadeImage.color;
            _fadeImage.color = new Color(c.r, c.g, c.b, targetAlpha);
        }

        private float GetAlpha(float age) {
            if (age < _fadeTime) return _age / _fadeTime;
            if (age < _fadeTime + _blackTime) return 1.0f;
            return 1.0f - (age - _fadeTime + _blackTime) / _fadeTime;
        }

        private IEnumerator CO_UnloadAfterTime(float time) {
            yield return new WaitForSeconds(time);
            SceneUnloader.Unload();
        }

        private IEnumerator CO_TransitionAfterTime(float time) {
            yield return new WaitForSeconds(time);
            SceneTransitioner.FinishTransition();
            _onTransition.Invoke();
        }
    }
}