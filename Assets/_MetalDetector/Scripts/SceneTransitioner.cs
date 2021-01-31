using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetalDetector {
    [CreateAssetMenu(menuName = "MetalDetector/SceneTransitioner")]
    public class SceneTransitioner : ScriptableObject {
        [SerializeField] private string _targetSceneName;
        private static List<string> _queuedScenes;
        private static IList<string> QueuedScenes {
            get {
                if (_queuedScenes == null) {
                    _queuedScenes = new List<string>();
                }
                return _queuedScenes;
            }
        }

        public void QueueTransition() {
            QueuedScenes.Add(_targetSceneName);
        }

        public void Transition() {
            SceneManager.LoadScene(_targetSceneName, LoadSceneMode.Additive);
        }

        public static void FinishTransition() {
            foreach(string sceneName in QueuedScenes) {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
            }
            QueuedScenes.Clear();
        }
    }
}
