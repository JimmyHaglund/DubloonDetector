using UnityEngine;

namespace MetalDetector {
    public class Quitter : ScriptableObject {
        public void Quit() {
#if (UNITY_EDITOR)
            Debug.Log("Application quit");
            return;
#endif
            Application.Quit();
        }
    }
}