using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JimmyHaglund;

namespace MetalDetector {
    public class DeformableMesh : MonoBehaviour {
        [SerializeField] [ChildReferenceButton] private MeshFilter _targetMesh;

        private Mesh MyMesh => _targetMesh != null ? _targetMesh.mesh : null;

        public void RandomiseAltitudes(float maxDelta) {
            if (MyMesh == null) return;
            var vertices = MyMesh.vertices;
            for (int n = 0; n < MyMesh.vertexCount; n++) {
                var vertice = vertices;
                vertices[n].y += Random.Range(-maxDelta, maxDelta);
            }
            MyMesh.SetVertices(vertices);
        }



        public void PrintMesh() {
            if (MyMesh == null) {
                return;
            }
            Debug.Log("Vertices of mesh: " + _targetMesh + " (" + MyMesh.vertexCount + ")");
            for (int n = 0; n < MyMesh.vertexCount; n++) {
                var vertice = MyMesh.vertices[n];
                var color = (vertice.x * 100 + vertice.y * 10 + vertice.z).ToString();
                Debug.Log("<color= " + color + ">" + vertice + "</color>");
            }
        }
    }
}