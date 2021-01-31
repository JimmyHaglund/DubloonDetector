using UnityEngine;
using UnityEditor;

namespace JimmyHaglund.JCollections {
    public class DisableableObjectCollection : MonoBehaviour {
        [SerializeField] private GameObject[] _objects = null;

        public void SetEnabledCount(int count) {
            for (int n = 0; n < _objects.Length; n++) {
                _objects[n].SetActive(n < count);
            }
        }
    }
}