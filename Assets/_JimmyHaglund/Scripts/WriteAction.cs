using System;
namespace JimmyHaglund {
    /// <summary>
    /// Action that can be executed 
    /// </summary>
    public sealed class WriteAction : ReadAction {
        public Action Action {
            get => _action;
            set => _action = value;
        }
        public WriteAction(Action action = null) : base(action) {
            Action = action;
        }
    }
    public sealed class WriteAction<T> : ReadAction<T> {
        public Action<T> Action { get; set; }
        public WriteAction(Action<T> action = null) {
            Action = action;
            Create(this);
        }
    }
    public sealed class WriteAction<T1, T2> : ReadAction<T1, T2> {
        public Action<T1, T2> Action { get; set; }
        public WriteAction(Action<T1, T2> action = null) {
            Action = action;
            Create(this);
        }
    }
    public sealed class WriteAction<T1, T2, T3> : ReadAction<T1, T2, T3> {
        public Action<T1, T2, T3> Action { get; set; }
        public WriteAction(Action<T1, T2, T3> action = null) {
            Action = action;
            Create(this);
        }
    }

    /// <summary>
    /// Action that cannot be executed.
    /// </summary>
    public class ReadAction {
        protected Action _action;

        protected ReadAction(Action action) {
            _action = action;
        }

        public void Add(Action action) {
            _action += action;
        }

        public void Remove(Action action) {
            _action -= action;
        }

        public static ReadAction operator +(ReadAction capAction, Action addAction) {
            capAction.Add(addAction);
            return capAction;
        }
        public static ReadAction operator -(ReadAction capAction, Action subtractAction) {
            capAction.Remove(subtractAction);
            return capAction;
        }
    }

    public class ReadAction<T> {
        private WriteAction<T> _action;
        protected ReadAction() { }
        protected void Create(WriteAction<T> action) {
            _action = action;
        }
        public void Add(Action<T> action) {
            _action.Action += action;
        }
        public void Remove(Action<T> action) {
            _action.Action -= action;
        }

        public static ReadAction<T> operator +(ReadAction<T> capAction, Action<T> addAction) {
            capAction.Add(addAction);
            return capAction;
        }
        public static ReadAction<T> operator -(ReadAction<T> capAction, Action<T> subtractAction) {
            capAction.Remove(subtractAction);
            return capAction;
        }
    }

    public class ReadAction<T1, T2> {
        private WriteAction<T1, T2> _action;
        protected ReadAction() { }
        protected void Create(WriteAction<T1, T2> action) {
            _action = action;
        }
        public void Add(Action<T1, T2> action) {
            _action.Action += action;
        }
        public void Remove(Action<T1, T2> action) {
            _action.Action -= action;
        }

        public static ReadAction<T1, T2> operator +(ReadAction<T1, T2> capAction, Action<T1, T2> addAction) {
            capAction.Add(addAction);
            return capAction;
        }
        public static ReadAction<T1, T2> operator -(ReadAction<T1, T2> capAction, Action<T1, T2> subtractAction) {
            capAction.Remove(subtractAction);
            return capAction;
        }
    }

    public class ReadAction<T1, T2, T3> {
        private WriteAction<T1, T2, T3> _action;
        protected ReadAction() { }
        protected void Create(WriteAction<T1, T2, T3> action) {
            _action = action;
        }
        public void Add(Action<T1, T2, T3> action) {
            _action.Action += action;
        }
        public void Remove(Action<T1, T2, T3> action) {
            _action.Action -= action;
        }

        public static ReadAction<T1, T2, T3> operator +(ReadAction<T1, T2, T3> capAction, Action<T1, T2, T3> addAction) {
            capAction.Add(addAction);
            return capAction;
        }
        public static ReadAction<T1, T2, T3> operator -(ReadAction<T1, T2, T3> capAction, Action<T1, T2, T3> subtractAction) {
            capAction.Remove(subtractAction);
            return capAction;
        }
    }
}