using UnityEngine;

namespace AGame.Code.Extensions
{
    public static class RectExtensions
    {
        public static void Encapsulate(this ref Rect a, Rect b)
        {
            float minX = Mathf.Min(a.xMin, b.xMin);
            float minY = Mathf.Min(a.yMin, b.yMin);
            float maxX = Mathf.Max(a.xMax, b.xMax);
            float maxY = Mathf.Max(a.yMax, b.yMax);

            a = Rect.MinMaxRect(minX, minY, maxX, maxY);
        }
        
        public static void Encapsulate(this ref Rect a, Vector2 point)
        {
            float minX = Mathf.Min(a.xMin, point.x);
            float minY = Mathf.Min(a.yMin, point.y);
            float maxX = Mathf.Max(a.xMax, point.x);
            float maxY = Mathf.Max(a.yMax, point.y);

            a = Rect.MinMaxRect(minX, minY, maxX, maxY);
        }
        
        public static Rect Scaled(this Rect r, float scale) =>
            new Rect(
                r.x + r.width * (1 - scale) * 0.5f,
                r.y + r.height * (1 - scale) * 0.5f,
                r.width * scale,
                r.height * scale
            );
    }
}