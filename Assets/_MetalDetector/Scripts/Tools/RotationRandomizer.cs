using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (UNITY_EDITOR)
using UnityEditor;
#endif
namespace MetalDetector.Tools {
    public class RotationRandomizer : MonoBehaviour {
        public void Randomize() {
            transform.rotation = Random.rotation;
        }

#if (UNITY_EDITOR)
        [CustomEditor(typeof(RotationRandomizer))]
        public class RotationRandomizerEditor : Editor {
            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                if (GUILayout.Button("Randomize")) {
                    var randomizer = target as RotationRandomizer;
                    Undo.RecordObject(randomizer.transform, "Randomized Rotation");
                    randomizer.Randomize();
                }
            }
        }
            
#endif
    }
}