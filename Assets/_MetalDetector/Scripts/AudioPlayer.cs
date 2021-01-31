using System.Collections;
using UnityEngine;
using JimmyHaglund;

namespace MetalDetector {
    public class AudioPlayer : MonoBehaviour {
        [Header("Dependencies")]
        [SerializeField] [ChildReferenceButton] private AudioSource _player = null;
        [Header("Settings")]
        [SerializeField] private AudioClip[] _clips = null;
        
        public void PlayRandomClip() {
            if (_player == null || _clips == null || _clips.Length == 0) return;
            _player.Stop();
            var clip = _clips[Random.Range(0, _clips.Length - 1)];
            _player.clip = clip;
            _player.Play();
        }
    }
}