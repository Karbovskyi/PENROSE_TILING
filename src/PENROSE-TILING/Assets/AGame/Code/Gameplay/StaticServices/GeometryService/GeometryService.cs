using System.Collections.Generic;
using AGame.Code.Gameplay.Features.Tiles.Data;
using UnityEngine;

namespace AGame.Code.Gameplay.StaticServices.GeometryService
{
    public static class GeometryService
    {
        private const float Epsilon = 1e-3f;
        
        public static Vector3 LocalToWorldPoint(Vector2 localPoint, Vector3 position, Quaternion rotation,
            Vector3 scale)
        {
            Matrix4x4 matrix = Matrix4x4.TRS(position, rotation, scale);
            return matrix.MultiplyPoint3x4(localPoint);
        }

        public static Rect GetRect(Vector2[] localPoints, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            float minX = float.PositiveInfinity, minY = float.PositiveInfinity;
            float maxX = float.NegativeInfinity, maxY = float.NegativeInfinity;

            foreach (Vector2 point in localPoints)
            {
                Vector3 wp = LocalToWorldPoint(point, position, rotation, scale);
                minX = Mathf.Min(minX, wp.x);
                minY = Mathf.Min(minY, wp.y);
                maxX = Mathf.Max(maxX, wp.x);
                maxY = Mathf.Max(maxY, wp.y);
            }

            return new Rect(minX, minY, maxX - minX, maxY - minY);
        }

        public static bool IsPointInTriangle(Vector2 point, Vector2 a, Vector2 b, Vector2 c)
        {
            Vector2 v0 = c - a;
            Vector2 v1 = b - a;
            Vector2 v2 = point - a;

            float d00 = Vector2.Dot(v0, v0);
            float d01 = Vector2.Dot(v0, v1);
            float d11 = Vector2.Dot(v1, v1);
            float d20 = Vector2.Dot(v2, v0);
            float d21 = Vector2.Dot(v2, v1);

            float denom = d00 * d11 - d01 * d01;
            if (Mathf.Abs(denom) < Epsilon) return false;

            float u = (d11 * d20 - d01 * d21) / denom;
            float v = (d00 * d21 - d01 * d20) / denom;

            return u >= -Epsilon && v >= -Epsilon && u + v <= 1 + Epsilon;
        }
        
        public static bool PolygonsIntersect(IEnumerable<Triangle> polyA, IEnumerable<Triangle> polyB)
        {
            foreach (var triA in polyA)
            foreach (var triB in polyB)
                if (TrianglesIntersect(triA, triB))
                    return true;
            return false;
        }
        
        private static bool TrianglesIntersect(Triangle a, Triangle b)
        {
            if (IsTrianglesIdentical(a, b))
                return true;
            
            if (IsSegmentsIntersect(a.A, a.B, b.A, b.B)) return true;
            if (IsSegmentsIntersect(a.A, a.B, b.B, b.C)) return true;
            if (IsSegmentsIntersect(a.A, a.B, b.C, b.A)) return true;

            if (IsSegmentsIntersect(a.B, a.C, b.A, b.B)) return true;
            if (IsSegmentsIntersect(a.B, a.C, b.B, b.C)) return true;
            if (IsSegmentsIntersect(a.B, a.C, b.C, b.A)) return true;

            if (IsSegmentsIntersect(a.C, a.A, b.A, b.B)) return true;
            if (IsSegmentsIntersect(a.C, a.A, b.B, b.C)) return true;
            if (IsSegmentsIntersect(a.C, a.A, b.C, b.A)) return true;

            return false;
        }

        private static bool IsTrianglesIdentical(Triangle triangleA, Triangle triangleB)
        {
            return IsTriangleHaveCommonPoint(triangleA.A, triangleB) &&
                   IsTriangleHaveCommonPoint(triangleA.B, triangleB) &&
                   IsTriangleHaveCommonPoint(triangleA.C, triangleB);
        }
        
        private static bool IsTriangleHaveCommonPoint(Vector2 point, Triangle triangle)
        {
            return IsPointsEqual(point, triangle.A) ||
                   IsPointsEqual(point, triangle.B) ||
                   IsPointsEqual(point, triangle.C);
        }
        
        private static bool IsPointsEqual(Vector2 a, Vector2 b)
        {
            return (a - b).sqrMagnitude <= Epsilon * Epsilon;
        }
        
        private static bool IsSegmentsIntersect(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            float o1 = PointOrientation(b1, b2, a1);
            float o2 = PointOrientation(b1, b2, a2);
            float o3 = PointOrientation(a1, a2, b1);
            float o4 = PointOrientation(a1, a2, b2);
            
            if (Mathf.Abs(o1) < Epsilon) o1 = 0f;
            if (Mathf.Abs(o2) < Epsilon) o2 = 0f;
            if (Mathf.Abs(o3) < Epsilon) o3 = 0f;
            if (Mathf.Abs(o4) < Epsilon) o4 = 0f;
            
            return (o1 * o2 < 0f) && (o3 * o4 < 0f);
        }

        private static float PointOrientation(Vector2 a, Vector2 b, Vector2 point)
        {
            Vector2 ab = b - a;
            Vector2 ap = point - a;
            return ab.x * ap.y - ab.y * ap.x;
        }

        private static bool IsSegmentsHaveCommonPoint(Vector2 a1, Vector2 a2, Vector2 b1, Vector2 b2)
        {
            return IsPointsEqual(a1, b1) || IsPointsEqual(a1, b2) || IsPointsEqual(a2, b1) || IsPointsEqual(a2, b2);
        }
    }
}