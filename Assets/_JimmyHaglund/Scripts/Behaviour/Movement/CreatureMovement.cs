using UnityEngine;

namespace JimmyHaglund.Behaviours.Movement {
    public class CreatureMovement : MonoBehaviour {
        [SerializeField] private float _fallSpeed = 9.8f;
        [SerializeField] private MoveState _uprightSettings;
        [SerializeField] private MoveState _crouchSettings;
        [SerializeField] private MoveState _crawlSettings;
        [SerializeField] protected float _minimumVelocity = 0.025f;
        
        protected float _walkSpeed = 2.0f;
        protected float _jogSpeed = 4.0f;
        protected float _sprintSpeed = 6.0f;
        protected float FallSpeed => _fallSpeed;

        protected Vector3 Velocity { get; set; }
        protected Vector3 DesiredVelocity { get; set; }

        /// <summary>
        /// Move states such as upright, crouching, crawling all fall under this one.
        /// </summary>
        [System.Serializable]
        private struct MoveState {
            public string OnEnterAnimationTrigger;
            public float WalkMultiplier;
            public float JogMultiplier;
            public float SprintMultiplier;
            public MoveState(float walkMultiplier, float jogMultiplier, float sprintMultiplier, string animationTrigger = "") {
                WalkMultiplier = walkMultiplier;
                JogMultiplier = jogMultiplier;
                SprintMultiplier = sprintMultiplier;
                OnEnterAnimationTrigger = animationTrigger;
            }
        }

        protected virtual void Update() {
            DesiredVelocity = Vector3.zero;
        }

        /// <summary>
        /// Creature movement uses a Character Controller and is subject to gravity et cetera.
        /// </summary>
        /// <param name="input"></param>
        public void Displace(Vector3 input) {
            if (Vector3.SqrMagnitude(input) > 1.0f) {
                input.Normalize();
            }
            
            DesiredVelocity = input * _jogSpeed;
        }

        public void Turn(Vector3 targetForward) {
            // Get look angle
            Vector3 flatForward = Vector3.ProjectOnPlane(targetForward, Vector3.up);
            if (flatForward != Vector3.zero) {
                transform.forward = flatForward;
            }
        }
    }
}