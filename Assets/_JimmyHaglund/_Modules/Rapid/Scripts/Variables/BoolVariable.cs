using UnityEngine;

namespace JimmyHaglund.Rapid {
    [CreateAssetMenu(menuName = "JimmyHaglund/Variables/Bool")]
    public class BoolVariable : ScriptableVariable<bool> {
        public void Toggle() {
            Value = !Value;
        }
    }
}