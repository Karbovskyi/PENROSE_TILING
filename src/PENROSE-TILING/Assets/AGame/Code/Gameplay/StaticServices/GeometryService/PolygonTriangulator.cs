using System.Collections.Generic;
using AGame.Code.Gameplay.Features.Tiles.Data;
using UnityEngine;

namespace AGame.Code.Gameplay.StaticServices.GeometryService
{
    // Згенеровано AI
    public static class PolygonTriangulator
    {
        /// <summary>
        /// Розбиває полігон на трикутники.
        /// </summary>
        /// <param name="vertices">Масив вершин полігону (повинні йти по порядку, за або проти годинникової стрілки).</param>
        /// <returns>Масив індексів трикутників у форматі TileTriangleIndices.</returns>
        public static TileTriangleIndices[] Triangulate(Vector2[] vertices)
        {
            List<TileTriangleIndices> triangles = new List<TileTriangleIndices>();
            List<int> remainingIndices = new List<int>();

            if (vertices == null || vertices.Length < 3)
            {
                return triangles.ToArray(); // Неможливо тріангулювати менше трьох вершин
            }

            // Створюємо список індексів вершин
            for (int i = 0; i < vertices.Length; i++)
            {
                remainingIndices.Add(i);
            }

            // Якщо полігон за годинниковою стрілкою, розвертаємо індекси,
            // оскільки алгоритм очікує порядок проти годинникової стрілки.
            if (IsClockwise(vertices, remainingIndices))
            {
                remainingIndices.Reverse();
            }

            // Процес "відрізання вух"
            while (remainingIndices.Count > 3)
            {
                bool earFound = false;
                for (int i = 0; i < remainingIndices.Count; i++)
                {
                    int prevIndex = remainingIndices[(i == 0) ? remainingIndices.Count - 1 : i - 1];
                    int currentIndex = remainingIndices[i];
                    int nextIndex = remainingIndices[(i + 1) % remainingIndices.Count];

                    Vector2 pPrev = vertices[prevIndex];
                    Vector2 pCurr = vertices[currentIndex];
                    Vector2 pNext = vertices[nextIndex];

                    // Перевіряємо, чи є поточна вершина "вухом"
                    if (IsEar(pPrev, pCurr, pNext, vertices, remainingIndices))
                    {
                        // Додаємо трикутник до списку
                        triangles.Add(new TileTriangleIndices(prevIndex, currentIndex, nextIndex));

                        // Видаляємо вершину "вуха" зі списку
                        remainingIndices.RemoveAt(i);
                        earFound = true;
                        break;
                    }
                }

                // Захист від нескінченного циклу для складних або неправильних полігонів
                if (!earFound)
                {
                    Debug.LogError(
                        "Polygon triangulation failed. The polygon might be complex, self-intersecting, or have collinear points.");
                    return new TileTriangleIndices[0];
                }
            }

            // Додаємо останній трикутник, що залишився
            if (remainingIndices.Count == 3)
            {
                triangles.Add(new TileTriangleIndices(remainingIndices[0], remainingIndices[1], remainingIndices[2]));
            }

            return triangles.ToArray();
        }

        /// <summary>
        /// Перевіряє, чи є вершина "вухом".
        /// </summary>
        private static bool IsEar(Vector2 p1, Vector2 p2, Vector2 p3, Vector2[] allVertices, List<int> remainingIndices)
        {
            // Перевірка 1: Трикутник повинен бути повернутий в тому ж напрямку, що і полігон (випуклий кут)
            // Для полігонів проти годинникової стрілки площа має бути додатною
            if (CrossProduct(p1, p2, p3) < 0)
            {
                return false;
            }

            // Перевірка 2: Жодна інша вершина полігону не повинна знаходитись всередині цього трикутника
            foreach (int index in remainingIndices)
            {
                Vector2 p = allVertices[index];
                if (p == p1 || p == p2 || p == p3)
                {
                    continue;
                }

                if (IsPointInTriangle(p, p1, p2, p3))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Перевіряє, чи знаходиться точка всередині трикутника.
        /// </summary>
        private static bool IsPointInTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c)
        {
            float d1 = CrossProduct(p, a, b);
            float d2 = CrossProduct(p, b, c);
            float d3 = CrossProduct(p, c, a);

            bool hasNeg = (d1 < 0) || (d2 < 0) || (d3 < 0);
            bool hasPos = (d1 > 0) || (d2 > 0) || (d3 > 0);

            return !(hasNeg && hasPos);
        }

        /// <summary>
        /// Перевіряє напрямок обходу вершин (за чи проти годинникової стрілки).
        /// </summary>
        private static bool IsClockwise(Vector2[] vertices, List<int> indices)
        {
            float sum = 0;
            for (int i = 0; i < indices.Count; i++)
            {
                Vector2 p1 = vertices[indices[i]];
                Vector2 p2 = vertices[indices[(i + 1) % indices.Count]];
                sum += (p2.x - p1.x) * (p2.y + p1.y);
            }

            return sum > 0;
        }

        /// <summary>
        /// Розраховує "псевдо" векторний добуток для 2D векторів.
        /// Використовується для визначення орієнтації трьох точок.
        /// </summary>
        private static float CrossProduct(Vector2 a, Vector2 b, Vector2 c)
        {
            // (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x)
            return (b.x - a.x) * (c.y - a.y) - (b.y - a.y) * (c.x - a.x);
        }
    }
}