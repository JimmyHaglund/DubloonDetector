using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

namespace JimmyHaglund.Geometry {
    /// <summary>
    /// Contains methods for generating positions in patterns.
    /// </summary>
    public static class PositionPatterns {
        public static Vector3[] Circle(int numberOfPoints, float radius) {
            var points = new Vector3[numberOfPoints];
            for (int n = 0; n < numberOfPoints; n++) {
                var angle = 2.0f * n *  Mathf.PI / numberOfPoints;
                var pointX = Mathf.Cos(angle) * radius;
                var pointY = 0.0f;
                var pointZ = Mathf.Sin(angle) * radius;
                var point = new Vector3(pointX, pointY, pointZ);
                points[n] = point;
            }
            return points;
        }

        public static void PlaceInCircle(IEnumerable<Component> items, Vector3 centre, float radius) {
            var nodeCount = Count(items);
            var circle = PositionPatterns.Circle(nodeCount, radius);
            int n = 0;
            foreach (Component item in items) {
                if (item == null) continue;
                item.transform.position = centre + circle[n++];
#if (UNITY_EDITOR)
                if (!Application.isPlaying) {
                    EditorUtility.SetDirty(item.transform);
                }
#endif
            }
        }

        private static int Count(IEnumerable enumerable) {
            int n = 0;
            foreach(object item in enumerable) {
                n++;
            }
            return n;
        }
    }
}
