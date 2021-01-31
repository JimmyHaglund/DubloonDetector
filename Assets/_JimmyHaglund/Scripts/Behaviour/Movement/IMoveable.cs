namespace JimmyHaglund.Behaviours.Movement {
    public interface IMoveable {
        void Displace(float dX, float dY, float dz);
        void SetVelocity(float velocityX, float velocityY, float velocityZ);
        void Accellerate(float accellerationX, float accellerationY, float accellerationZ);
        float MaxSpeedMetresPerSecond { get; set; }
    }
    [System.Serializable] public class IFMoveable : InterfaceField<IMoveable> { }
}
