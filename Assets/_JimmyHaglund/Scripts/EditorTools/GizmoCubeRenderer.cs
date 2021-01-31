using UnityEngine;

namespace JimmyHaglund {
    public class GizmoCubeRenderer : MonoBehaviour {
        [SerializeField] private Vector3 _cubeCenterOffset = Vector3.zero;
        [SerializeField] private Vector3 _cubeSize = Vector3.one;
        [SerializeField] private Color _color = Color.blue;
        private void OnDrawGizmos() {
            Gizmos.color = _color;
            Gizmos.DrawCube(transform.position + _cubeCenterOffset, _cubeSize);
        }
    }
}