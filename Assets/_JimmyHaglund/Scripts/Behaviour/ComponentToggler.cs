using UnityEngine;
using Behaviour = UnityEngine.Behaviour;

namespace JimmyHaglund {
    public class ComponentToggler : MonoBehaviour {
        [SerializeField] private Behaviour[] _behaviours = new Behaviour[0];
        [SerializeField] private Collider[] _colliders = new Collider[0];
        [SerializeField] private Renderer[] _renderers = new Renderer[0];

        public void Enable() => SetComponentsEnabled(true);
        public void Disable() => SetComponentsEnabled(false);

        private void SetComponentsEnabled(bool enabled) {
            foreach (Behaviour behaviour in _behaviours) {
                if (behaviour == null) continue;
                behaviour.enabled = enabled;
            }
            foreach (Collider collider in _colliders) {
                if (collider == null) continue;
                collider.enabled = enabled;
            }
            foreach (Renderer renderer in _renderers) {
                if (renderer == null) continue;
                renderer.enabled = enabled;
            }
        }
    }
}