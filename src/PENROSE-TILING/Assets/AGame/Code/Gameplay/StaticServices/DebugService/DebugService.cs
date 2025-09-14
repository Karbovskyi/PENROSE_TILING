using UnityEngine;

namespace AGame.Code.Gameplay.StaticServices.DebugService
{
    public static class DebugService
    {
        public static void DrawDebugCircle(Vector3 center, float radius, int segments = 32, Color? color = null,
            float duration = 0f)
        {
            Color drawColor = color ?? Color.white;

            float angleStep = 360f / segments;
            for (int i = 0; i < segments; i++)
            {
                float angleCurrent = Mathf.Deg2Rad * angleStep * i;
                float angleNext = Mathf.Deg2Rad * angleStep * (i + 1);

                Vector3 pointCurrent = center + new Vector3(Mathf.Cos(angleCurrent), Mathf.Sin(angleCurrent), 0) * radius;
                Vector3 pointNext = center + new Vector3(Mathf.Cos(angleNext), Mathf.Sin(angleNext), 0) * radius;

                Debug.DrawLine(pointCurrent, pointNext, drawColor, duration);
            }
        }

        public static void DrawDebugRect(Rect rect, float z = 0f, Color? color = null, float duration = 0f)
        {
            var c = color ?? Color.yellow;

            Vector3 bottomLeft  = new Vector3(rect.xMin, rect.yMin, z);
            Vector3 bottomRight = new Vector3(rect.xMax, rect.yMin, z);
            Vector3 topRight    = new Vector3(rect.xMax, rect.yMax, z);
            Vector3 topLeft     = new Vector3(rect.xMin, rect.yMax, z);

            Debug.DrawLine(bottomLeft, bottomRight, c, duration);
            Debug.DrawLine(bottomRight, topRight, c, duration);
            Debug.DrawLine(topRight, topLeft, c, duration);
            Debug.DrawLine(topLeft, bottomLeft, c, duration);
        }
    }
}