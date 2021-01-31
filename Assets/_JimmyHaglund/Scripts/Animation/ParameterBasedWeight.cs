using UnityEngine;

namespace Kelvin.General {
    public class ParameterBasedWeight : StateMachineBehaviour {
        [SerializeField] private int _layerIndex = 0;
        [SerializeField] private float _targetWeight = 1.0f;
        [SerializeField] private string _intParameterName = "Parameter";
        [SerializeField] private int _onThreshold = 1;
        private float _weight = 0.0f;

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            var paramValue = animator.GetInteger(_intParameterName);
            if (paramValue == _onThreshold) {
                _weight = Mathf.Lerp(_weight, _targetWeight, Time.deltaTime * 7);
            }
            else {
                _weight = Mathf.Lerp(_weight, 0.0f, Time.deltaTime * 7);
            }
            animator.SetLayerWeight(_layerIndex, _weight);
        }
    }
}