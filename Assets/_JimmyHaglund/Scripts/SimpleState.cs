using System.Collections.Generic;

namespace JimmyHaglund {
    /// <summary>
    /// A state that can contain transitions to other states. 
    /// </summary>
    public class SimpleState {
        private Dictionary<SimpleState, StateTransition> _transitions = new Dictionary<SimpleState, StateTransition>();
        private WriteAction _onStateEnter = new WriteAction();
        private WriteAction _onStateExit = new WriteAction();
        private SimpleState _activeTransition = null;

        public ReadAction OnStateEnter { get; private set; }
        public ReadAction OnStateExit { get; private set; }

        public SimpleState() {
            OnStateEnter = _onStateEnter;
            OnStateExit = _onStateExit;
        }

        public void AddTransition(SimpleState toState) {
            if (_transitions.ContainsKey(toState)) {
                StateTransition transition = _transitions[toState];
            }
            else {
                _transitions.Add(toState, new StateTransition(this, toState));
            }
        }
        public void RemoveTransition(SimpleState toState) {
            if (_activeTransition == toState) {
                EndTransition();
            }
            if (_transitions.ContainsKey(toState)) {
                _transitions.Remove(toState);
            }
        }
        public ReadAction OnTransitionStart(SimpleState toState) {
            StateTransition transition;
            if (_transitions.TryGetValue(toState, out transition)) {
                return transition.OnTransitionStart;
            }
            else {
                return null;
            }
        }
        public ReadAction OnTransitionEnd(SimpleState toState) {
            StateTransition transition;
            if (_transitions.TryGetValue(toState, out transition)) {
                return transition.OnTransitionEnd;
            }
            else {
                return null;
            }
        }

        protected void StartTransition(SimpleState to) {
            if (to == null) return;
            StateTransition transition;
            if (_transitions.TryGetValue(to, out transition)) {
                transition.StartTransition();
                _activeTransition = to;
            }
        }

        protected void EndTransition() {
            if (_activeTransition == null) return;
            StateTransition transition;
            if (_transitions.TryGetValue(_activeTransition, out transition)) {
                transition.EndTransition();
            }
            _activeTransition = null;
        }
        
        /// <summary>
        /// A state machine contains a number of states that it manages.
        /// </summary>
        /// The state machine is also a state, and can transition to other states or state machines.
        /// The state machine is the only way to create new states.
        public sealed class Machine : SimpleState {
            private SimpleState _activeState;
            public List<SimpleState> States { get; private set; } = new List<SimpleState>();

            public Machine(SimpleState initial) : base() {
                _activeState = initial;
                OnStateEnter.Add(StateEntered);
                OnStateExit.Add(StateExited);
            }

            public SimpleState AddState() {
                SimpleState newState = new SimpleState();
                States.Add(newState);
                return newState;
            }
            public SimpleState AddState(SimpleState addedState) {
                if (States.Contains(addedState)) return addedState;
                States.Add(addedState);
                return addedState;
            }
            public void RemoveState(SimpleState state) {
                States.Remove(state);
            }
            public void MoveState(SimpleState state, SimpleState.Machine toMachine) {
                States.Remove(state);
                toMachine.AddState(state);
            }

            public new void StartTransition(SimpleState toState) {
                _activeState.StartTransition(toState);
            }
            public new void EndTransition() {
                SimpleState newState = _activeState._activeTransition;
                if (newState == null) return;
                _activeState.EndTransition();
                _activeState = newState;
            }

            private void StateEntered() {
                _activeState._onStateEnter.Action?.Invoke();
            }

            private void StateExited() {
                _activeState._onStateExit.Action?.Invoke();
            }
        }

        private struct StateTransition {
            private SimpleState _fromState;
            private SimpleState _toState;
            private WriteAction _onTransitionStart;
            private WriteAction _onTransitionEnd;

            public ReadAction OnTransitionStart { get; private set; }
            public ReadAction OnTransitionEnd { get; private set; }

            public StateTransition(SimpleState from, SimpleState to) {
                _fromState = from;
                _toState = to;
                _onTransitionStart = new WriteAction();
                _onTransitionEnd = new WriteAction();
                OnTransitionStart = _onTransitionStart;
                OnTransitionEnd = _onTransitionEnd;
            }

            public void StartTransition() {
                _fromState._onStateExit.Action?.Invoke();
                _onTransitionStart.Action?.Invoke();
            }

            public void EndTransition() {
                _onTransitionEnd.Action?.Invoke();
                _toState._onStateEnter.Action?.Invoke();
            }

            private void EndTransition(object source, System.Timers.ElapsedEventArgs e) {
                _fromState.EndTransition();
            }
        }
    }
}