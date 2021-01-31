#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace JimmyHaglund {
    public class SceneContainerOpener : ScriptableObject {
        [MenuItem("Tools/Open Work Scenes")]
        static void OpenScene() {
            var container = Resources.Load("WorkScenes") as MultiSceneContainer;
            if (container == null) {
                Debug.LogWarning("No scene container found, cannot open!");
                return;
            }
            // container.PrintScenes();
            container.OpenScenes();
        }
    }
}
#endif