#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;
using System;
using UObject = UnityEngine.Object;

namespace JimmyHaglund {
    // Adopted & refactored. From https://gist.github.com/bcatcho/8638881
    public class ScriptableObjectInstantiator {
        [MenuItem("Assets/Create/Instantiate selected Scriptable Object", true)]
        private static bool IsScriptableObject() {
            var target = Selection.activeObject;
            if ((target == null) || !(target is TextAsset)) {
                return false;
            }
            var assetPath = AssetDatabase.GetAssetPath(target);
            if (assetPath == null) return false;
            var monoScript = (MonoScript)AssetDatabase.LoadAssetAtPath(assetPath, typeof(MonoScript));
            if (monoScript == null) {
                return false;
            }
            var targetType = monoScript.GetClass();
            if (targetType == null || !targetType.IsSubclassOf(typeof(ScriptableObject))) {
                return false;
            }
            return true;
        }

        [MenuItem("Assets/Create/Instantiate selected script", priority = 0)]
        private static void InstantiateScriptableObjectFromMenu() {
            var target = Selection.activeObject;
            var assetPath = AssetDatabase.GetAssetPath(target);
            var monoScript = (MonoScript)AssetDatabase.LoadAssetAtPath(assetPath, typeof(MonoScript));
            var scriptType = monoScript.GetClass();
            var path = EditorUtility.SaveFilePanelInProject("Save asset as .asset", scriptType.Name + "_asset.asset", "asset", "Please enter a file name");

            try {
                var inst = ScriptableObject.CreateInstance(scriptType);
                AssetDatabase.CreateAsset(inst, path);
            } catch (Exception e) {
                Debug.LogException(e);
            }
        }
        
    }
}

#endif