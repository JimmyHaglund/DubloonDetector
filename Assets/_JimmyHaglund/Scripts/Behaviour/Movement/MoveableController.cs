using UnityEngine;


namespace JimmyHaglund.Behaviours.Movement {

    /// <summary>
    /// Base Controller for a Movable creature component.
    /// </summary>
    public abstract class MoveableController : MonoBehaviour {
        [SerializeField] [SceneReferenceButton] protected Camera _camera = null;
        [SerializeField] protected IFMoveable _targetMoveable = new IFMoveable();
        private Vector3 _currentMovement = Vector3.zero;

        private IMoveable Moveable => _targetMoveable.Value;

        protected virtual void Move(Vector2 input) {
            if (Moveable == null || !enabled) return;

            Vector3 cameraForwardFlat = Vector3.ProjectOnPlane(_camera.transform.forward, Vector3.up).normalized;
            Vector3 cameraRightFlat = Quaternion.AngleAxis(90.0f, Vector3.up) * cameraForwardFlat;
            Vector3 forwardInput = cameraForwardFlat * input.y + cameraRightFlat * input.x;
            Moveable.Displace(forwardInput.x, forwardInput.y, forwardInput.z);
        }
    }
}
