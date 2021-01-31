using UnityEngine;

namespace JimmyHaglund {
    public static class Vector3Extensions {
        public static Vector3 WithZ(this Vector3 vector, float zValue) {
            return new Vector3(vector.x, vector.y, zValue);
        }

        public static Vector2 XY(this Vector3 vector) {
            return new Vector2(vector.x, vector.y);
        }
    }
}
