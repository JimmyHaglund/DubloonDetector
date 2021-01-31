using UnityEngine;

namespace JimmyHaglund {
    public class ConeGizmo : MonoSingleton<ConeGizmo> {
        private Mesh _coneMesh = null;

        protected override void OnCreated() {
            gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        public static void DrawCone(Vector3 position, Vector3 pointForward) {
            Instance.LoadAndDrawCone(position, pointForward);
        }

        private void LoadAndDrawCone(Vector3 position, Vector3 pointForward) {
            if (_coneMesh == null) {
                if (!LoadConeMesh()) return;
            }
            var rotation = Quaternion.FromToRotation(Vector3.forward, pointForward);
            Gizmos.DrawMesh(_coneMesh, position, rotation);
        }
        private bool LoadConeMesh() {
            _coneMesh = Resources.Load("GizmoCone", typeof(Mesh)) as Mesh;
            return _coneMesh != null;
        }
    }
}
