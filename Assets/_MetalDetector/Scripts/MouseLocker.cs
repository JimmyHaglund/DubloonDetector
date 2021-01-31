using System.Collections;
using UnityEngine;

namespace MetalDetector {
    public class MouseLocker : MonoBehaviour {

        private void OnEnable() {
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable() {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        private void Update() {
            if (!enabled) return;
            Cursor.visible = false;
        }
    }
}