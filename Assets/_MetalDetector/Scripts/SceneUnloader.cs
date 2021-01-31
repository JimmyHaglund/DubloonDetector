using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MetalDetector {
    [CreateAssetMenu(menuName = "MetalDetector/Scene Unloader")]
    public class SceneUnloader : ScriptableObject {
        [SerializeField] private string _sceneName;

        private static IList<string> _markedScenes;

        private static IList<string> MarkedScenes {
            get {
                if (_markedScenes == null) {
                    _markedScenes = new List<string>();
                }
                return _markedScenes;
            }
        }

        public void MarkForUnload() {
            if (MarkedScenes.Contains(_sceneName)) return;
            MarkedScenes.Add(_sceneName);
        }

        public static void Unload() { 
            for(int n = _markedScenes.Count - 1; n >= 0; n--){
                SceneManager.UnloadSceneAsync(_markedScenes[n]);
            }
                _markedScenes.Clear();
        }
    }
}
