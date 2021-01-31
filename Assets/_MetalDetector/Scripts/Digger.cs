using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using JimmyHaglund;

namespace MetalDetector {
    public class Digger : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] [ChildReferenceButton] private Camera _camera = null;
        [Header("Settings")]
        [SerializeField] private LayerMask _targetableLayers = default;
        [SerializeField] private float _digDepth = 0.15f;
        [SerializeField] [Min(0.0f)] private float _digRadius;
        [SerializeField] [Range(0.0f, 1.0f)] private float _digRadiusFalloff;
        [SerializeField] [Min(0.0f)] private float _digTimer = 0.5f;
        [Header("Events")]
        [SerializeField] private UnityEvent _onDigStart;
        [SerializeField] private Vector3Event _groundDugEvent;

        private RaycastHit _hitInfo;
        private bool _rayCastHit = false;
        private float _timeSinceLastDig = 0.0f;

        private MeshFilter _hitMeshFilter;
        private Mesh HitMesh => _hitMeshFilter.mesh;
        private MeshCollider _hitMeshCollider;
        private int _hitVertexIndex;

        private Vector3 HitVertexPosition => _hitMeshFilter.transform.position + _hitMeshFilter.transform.rotation * HitMesh.vertices[_hitVertexIndex];
        private bool CanDig => _rayCastHit && _timeSinceLastDig >= _digTimer;
        private bool _digging = false;

        private void Update() {
            _timeSinceLastDig += Time.deltaTime;
            if (_digging && CanDig) {
                _onDigStart.Invoke();
                _timeSinceLastDig = 0.0f;
            }
        }

        private void FixedUpdate() {
            var screenCentre = new Vector3(_camera.pixelWidth * 0.5f, _camera.pixelHeight* 0.5f);
            var ray = _camera.ScreenPointToRay(screenCentre);
            _rayCastHit = Physics.Raycast(ray, out _hitInfo, 5.0f, _targetableLayers.value);
            StoreHitVertex();
        }

        public void StartDig() {
            _digging = true;
        }

        public void StopDig() {
            _digging = false;
        }

        public void Dig() {
            // if (!CanDig) return;
            DigCircle();
            UpdateHitMeshCollider();
            if (_groundDugEvent != null) {
                _groundDugEvent.Raise(HitVertexPosition);   
            }
        }

        private void UpdateHitMeshCollider() {
            if (_hitMeshCollider == null) return;
            _hitMeshCollider.sharedMesh = null;
            _hitMeshCollider.sharedMesh = HitMesh;
        }

        private void DigSingle() {
            var vertices = HitMesh.vertices;
            var vertex = vertices[_hitVertexIndex];
            vertex.y -= _digDepth;
            vertices[_hitVertexIndex] = vertex;
            HitMesh.SetVertices(vertices);
        }

        private void DigCircle() {
            var vertices = HitMesh.vertices;
            var centre = HitMesh.vertices[_hitVertexIndex];
            var sqrRange = _digRadius * _digRadius;
            for(int n = 0; n < vertices.Length; n++) {
                var sqrDistance = Vector3.SqrMagnitude(vertices[n] - centre);
                if (sqrDistance > sqrRange) continue;
                var distance = Mathf.Sqrt(sqrDistance);
                var depth = _digDepth * GetDigStrength(distance / _digRadius);
                vertices[n] = DigVertex(vertices[n], depth);
            }
            HitMesh.SetVertices(vertices);
            HitMesh.RecalculateNormals();
        }

        float GetDigStrength(float fallofRatio) {
            return 0.5f + Mathf.Cos(_digRadiusFalloff* Mathf.PI / 2 * fallofRatio);
        }

        private Vector3 DigVertex(Vector3 vert, float depth) {
            var direction = Vector3.down;
            return vert + direction * depth;
        }

        public void StoreHitVertex() {
            if (!_rayCastHit) return;
            // Debug.Log("Ray cast hit: " + _hitInfo.collider.gameObject.name);
            _hitMeshFilter = _hitInfo.collider.GetComponent<MeshFilter>();
            _hitMeshCollider = _hitInfo.collider as MeshCollider;
            if (!HitMesh.isReadable) return;
            if (_hitMeshFilter == null || HitMesh == null) return;
            var tris = HitMesh.triangles;
            var hitVertexIndex = GetHitVertexIndex(_hitInfo, tris);
            if (hitVertexIndex < 0) return;
            _hitVertexIndex = hitVertexIndex;
        }

        private void OnDrawGizmos() {
            if (!_rayCastHit) return;
            Gizmos.color = Color.blue - new Color(0, 0, 0, 0.5f);
            Gizmos.DrawSphere(HitVertexPosition, 0.2f);
        }

        private int GetHitVertexIndex(RaycastHit hitInfo, int[] tris) {
            var barycentricCoordinate = hitInfo.barycentricCoordinate;
            var vertexIndex = hitInfo.triangleIndex * 3;
            if (vertexIndex < 0 || vertexIndex + 2 > tris.Length) {
                return -1;
            }
            var x = barycentricCoordinate.x;
            var y = barycentricCoordinate.y;
            var z = barycentricCoordinate.z;
            var largest = Mathf.Max(x, y, z);
            var closestX = tris[vertexIndex];
            var closestY = tris[vertexIndex + 1];
            var closestZ = tris[vertexIndex + 2];
            return largest == x ? closestX : largest == y ? closestY : closestZ;
        }
    }
}