using UnityEngine;
[ExecuteAlways]
public class SegmentRotator2D : MonoBehaviour
{
    [Header("Points (local 2D)")]
    public Vector2 pointA = Vector2.zero;
    public Vector2 pointB = Vector2.right;

    [Header("Extra Rotation")]
    [Tooltip("Angle in degrees to additionally rotate point B around point A")]
    public float angle;

    [Header("Debug (world positions)")]
    [SerializeField] private Vector2 currentA;
    [SerializeField] private Vector2 currentB;

    private void Update()
    {
        // Точка A у локальних координатах
        Vector2 localA = pointA;

        // Вектор від A до B у локальних координатах
        Vector2 dir = pointB - pointA;

        // Додаткове обертання на кут
        float rad = angle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(rad);
        float sin = Mathf.Sin(rad);

        Vector2 rotated = new Vector2(
            dir.x * cos - dir.y * sin,
            dir.x * sin + dir.y * cos
        );

        Vector2 localB = pointA + rotated;

        // Перетворюємо у світові координати з урахуванням трансформа геймобджекта
        currentA = transform.TransformPoint(localA);
        currentB = transform.TransformPoint(localB);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
            //Gizmos.DrawSphere(currentA, 0.05f);

        Gizmos.color = Color.red;
        //Gizmos.DrawSphere(currentB, 0.05f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(currentA, currentB);
    }
}