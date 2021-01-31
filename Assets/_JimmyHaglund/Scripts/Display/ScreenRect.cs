using UnityEngine;

namespace JimmyHaglund.Display {
    /// <summary>
    /// Utility class for convenient screen-related methods,
    /// such as converting from world to screen space or
    /// rendering textures to ui.
    /// </summary>
    public static class ScreenRect {
        private static Texture2D _whiteTexture;
        public static Texture2D WhiteTexture {
            get {
                if (_whiteTexture == null) {
                    _whiteTexture = new Texture2D(1, 1);
                    _whiteTexture.SetPixel(0, 0, Color.white);
                    _whiteTexture.Apply();
                }
                return _whiteTexture;
            }
        }

        /// <summary>
        /// Creates a rectangle between two screen positions.
        /// </summary>
        /// <param name="screenPositionA"></param>
        /// <param name="screenPositionB"></param>
        /// <returns></returns>
        public static Rect GetScreenRect(Vector3 screenPositionA, Vector3 screenPositionB) {
            screenPositionA = new Vector3(screenPositionA.x, Screen.height - screenPositionA.y, screenPositionA.z);
            screenPositionB = new Vector3(screenPositionB.x, Screen.height - screenPositionB.y, screenPositionB.z);

            Vector3 topLeftCorner = Vector3.Min(screenPositionA, screenPositionB);
            Vector3 bottomRightCorner = Vector3.Max(screenPositionA, screenPositionB);

            return Rect.MinMaxRect(topLeftCorner.x, topLeftCorner.y, bottomRightCorner.x, bottomRightCorner.y);
        }

        /// <summary>
        /// Draw rectangle on screen with given color.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        public static void DrawScreenRect(Rect rect, Color color) {
            GUI.color = color;
            GUI.DrawTexture(rect, WhiteTexture);
            GUI.color = Color.white;
        }

        /// <summary>
        /// Draws box borders on screen.
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="color"></param>
        /// <param name="thickness"></param>
        public static void DrawScreenRectBorders(Rect rect, Color color, float thickness) {
            // Top
            ScreenRect.DrawScreenRect(new Rect(rect.xMin, rect.yMin, rect.width, thickness), color);
            // Left
            ScreenRect.DrawScreenRect(new Rect(rect.xMin, rect.yMin, thickness, rect.height), color);
            // Right
            ScreenRect.DrawScreenRect(new Rect(rect.xMax - thickness, rect.yMin, thickness, rect.height), color);
            // Bottom
            ScreenRect.DrawScreenRect(new Rect(rect.xMin, rect.yMax - thickness, rect.width, thickness), color);
        }

        /// <summary>
        /// Retrieves bounding box in camera view port space based on two screen points.
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="ScreenPositionA"></param>
        /// <param name="ScreenPositionB"></param>
        /// <returns></returns>
        public static Bounds GetViewportBounds(Camera camera, Vector3 ScreenPositionA, Vector3 ScreenPositionB) {
            Vector3 viewPortPointA = camera.ScreenToViewportPoint(ScreenPositionA);
            Vector3 viewPortPointB = camera.ScreenToViewportPoint(ScreenPositionB);

            Vector3 boundsMin = Vector3.Min(viewPortPointA, viewPortPointB);
            Vector3 boundsMax = Vector3.Max(viewPortPointA, viewPortPointB);
            boundsMin.z = camera.nearClipPlane;
            boundsMax.z = camera.farClipPlane;
            Bounds bounds = new Bounds();
            bounds.SetMinMax(boundsMin, boundsMax);
            return bounds;
        }
    }
}