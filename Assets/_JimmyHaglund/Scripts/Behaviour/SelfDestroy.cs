using UnityEngine;
using System.Collections;

namespace JimmyHaglund {
    public class SelfDestroy : MonoBehaviour {
        [SerializeField] private float _lifeTimeSeconds = 1.0f;

        private void Start() {
            StartCoroutine(CO_DestroyAfterSeconds(_lifeTimeSeconds));
        }

        private IEnumerator CO_DestroyAfterSeconds(float seconds) {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}