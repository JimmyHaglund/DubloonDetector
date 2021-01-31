using UnityEngine;
using System.Collections;

namespace JimmyHaglund.Behaviours.Movement {
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMovement : MonoBehaviour {
        private CharacterController _characterController = null;

        private void Awake() {
            _characterController = GetComponent<CharacterController>();
            if (_characterController == null) {
                _characterController = gameObject.AddComponent<CharacterController>();
            }
        }
    }
}