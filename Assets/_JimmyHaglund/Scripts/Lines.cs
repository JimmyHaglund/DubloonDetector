using System;
using System.Collections.Generic;
using UnityEngine;
using JimmyHaglund.Geometry;

namespace JimmyHaglund {
    public static class Lines {
        public static IList<Vector2Int> FindStraightestLine(int startX, int startY, int endX, int endY) {
            var linePoints = new List<Vector2Int>();
            int deltaX = endX - startX;
            int deltaY = endY - startY;
            int numberOfSteps = Math.Max(Math.Abs(deltaX), Math.Abs(deltaY));
            float stepLength = (numberOfSteps == 0) ? 0.0f : 1.0f / (float)numberOfSteps;
            float xStep = (float)deltaX * stepLength;
            float yStep = (float)deltaY * stepLength;
            float x = startX;
            float y = startY;
            for (int n = 0; n <= numberOfSteps; n++) {
                int xPosition = (int)Math.Round(startX + n * xStep);
                int yPosition = (int)Math.Round(startY + n * yStep);
                linePoints.Add(new Vector2Int(xPosition, yPosition));
            }
            return linePoints;
        }

        public static IList<Vector2Int> FindCardinallyConnectedLine(int startX, int startY, int endX, int endY) {
            var linePoints = new List<Vector2Int>();
            int deltaX = endX - startX;
            int deltaY = endY - startY;
            int numberOfSteps = Math.Max(Math.Abs(deltaX), Math.Abs(deltaY));
            float stepLength = (numberOfSteps == 0) ? 0.0f : 1.0f / (float)numberOfSteps;
            float xStep = (float)deltaX * stepLength;
            float yStep = (float)deltaY * stepLength;
            float x = startX;
            float y = startY;

            int currentX = Mathf.RoundToInt(startX);
            int currentY = Mathf.RoundToInt(startY);
            int lastX = currentX;
            int lastY = currentY;
            linePoints.Add(new Vector2Int(currentX, currentY));
            for (int n = 1; n <= numberOfSteps; n++) {
                var xPosition = startX + n * xStep;
                var yPosition = startY + n * yStep;
                currentX = Mathf.RoundToInt(xPosition);
                currentY = Mathf.RoundToInt(yPosition);
                if (Measure.ManhattanDistance(lastX, lastY, currentX, currentY) > 1) {
                    linePoints.Add(GetInbetweenpoint(xPosition, yPosition, lastX, lastY));
                }
                linePoints.Add(new Vector2Int(currentX, currentY));
                lastX = currentX;
                lastY = currentY;
            }
            return linePoints;
        }

        private static Vector2Int GetInbetweenpoint(float currentX, float currentY, int lastX, int lastY) {
            var offsetX = currentX - Mathf.Round(currentX);
            var offsetY = currentY - Mathf.Round(currentY);
            if (Math.Abs(offsetX) < Math.Abs(offsetY)) {
                return new Vector2Int(lastX, Mathf.RoundToInt(currentY));
            } else {
                var extraY = lastY + Math.Sign(offsetY);
                return new Vector2Int(Mathf.RoundToInt(currentX), lastY);
            }
        }
    }
}