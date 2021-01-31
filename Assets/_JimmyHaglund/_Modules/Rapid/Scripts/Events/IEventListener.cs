namespace JimmyHaglund.Rapid {
    public interface IEventListener {
        void OnEventRaised();
    }
    public class IFEventListener : InterfaceField<IEventListener> { }

    public interface IEventListener<T> {
        void OnEventRaised(T arg);
    }
    public class IFEventListener<T> : InterfaceField<IEventListener<T>> { }

    public interface IEventListener<T1, T2> {
        void OnEventRaised(T1 arg1, T2 arg2);
    }
    public class IFEventListener<T1, T2> : InterfaceField<IEventListener<T1, T2>> { }
}
