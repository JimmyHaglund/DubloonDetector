using UnityEngine;

namespace JimmyHaglund.Rapid {
    public class GameEventFirer : MonoBehaviour {
        [SerializeField] [RequiredReference] private GameEvent _event;

        public void Fire() {
            if (_event == null) return;
            _event.Raise();
        }
    }
}