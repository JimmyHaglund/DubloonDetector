using UnityEngine;
using UnityEngine.Events;

namespace JimmyHaglund.Rapid {
    public class GameEventListener : MonoBehaviour {
        [SerializeField] private GameEvent _gameEvent = null;
        [SerializeField] private UnityEvent _response = new UnityEvent();

        private void OnEnable() {
            if (_gameEvent == null) return;
            _gameEvent.RegisterListener(this);
        }

        private void OnDisable() {
            if (_gameEvent == null) return;
            _gameEvent.UnRegisterListener(this);
        }

        public void OnEventRaised() {
            _response.Invoke();
        }
    }
}