using System;
using System.Collections;
using UnityEngine;
using JimmyHaglund;

namespace MetalDetector {
    public class DigParticleSpawner : MonoBehaviour {
        [Serializable] public class EmitSettings {
            [SerializeField] public float SecondsSinceLastEmission = 0.05f;
            [SerializeField] public int NumberOfParticles = 10;
        }
        [SerializeField] [ChildReferenceButton] private ParticleSystem _particleSystem;
        [SerializeField] private EmitSettings[] _digEmitSettings;


        public void SpawnParticlesAtPosition(Vector3 position) {
            _particleSystem.transform.position = position;
            _particleSystem.Play();
        }

        private IEnumerator CO_EmitParticles() {
            foreach(EmitSettings setting in _digEmitSettings) {
                yield return new WaitForSeconds(setting.SecondsSinceLastEmission);
                _particleSystem.Emit(setting.NumberOfParticles);
            }
        }
    }
}