using System;

namespace JimmyHaglund.Geometry {
    public static class Measure {
        public static float Distance(int x1, int y1, int x2, int y2) {
            var dX = x2 - x1;
            var dY = y2 - y1;
            return (float)Math.Sqrt(dX * dX + dY * dY);
        }
        public static float SquaredDistance(int x1, int y1, int x2, int y2) {
            var dX = x2 - x1;
            var dY = y2 - y1;
            return dX * dX + dY * dY;
        }

        public static int MaxCardinalDistance(int x1, int y1, int x2, int y2) {
            return Math.Max(Math.Abs(x1 - x2), Math.Abs(y1 - y2));
        }

        public static int ManhattanDistance(int x1, int y1, int x2, int y2) {
            return Math.Abs(x1 - x2) + Math.Abs(y1 - y2);
        }
    }
}
