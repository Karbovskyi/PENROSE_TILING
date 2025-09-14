using AGame.Code.Gameplay.Features.Tiles.ScriptableObjects;
using AGame.Code.Gameplay.StaticServices.GeometryService;
using Shapes;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Data
{
    public class TileStaticData
    {
        public int Id { get; private set; }
        public Vector2[] Vertices => _scriptable.Vertices;
        public TileTriangleIndices[] Triangles { get; private set; }
        public PolygonPath PolygonPath { get; private set; }

        public Color ValidBorderColor => _scriptable.ValidBorderColor;
        public Color InvalidBorderColor => _scriptable.InvalidBorderColor;
        public GradientFill GradientFill =>_scriptable.GradientFill;
        
        public Color LineColor => _scriptable.LineColor;
        public LineData[] Lines => _scriptable.Lines;

        private TileScriptableObject _scriptable;

        public TileStaticData(int id, TileScriptableObject scriptable)
        {
            Id = id;
            _scriptable = scriptable;
            
            Triangles = PolygonTriangulator.Triangulate(Vertices);
            PolygonPath = new PolygonPath();
            PolygonPath.AddPoints(Vertices);
        }

        public Rect GetRect(Vector3 worldPosition, Quaternion rotation, Vector3 scale)
        {
            return GeometryService.GetRect(Vertices, worldPosition, rotation, scale);
        }

        public Vector3 LocalVertexToWorld(int vertexIndex, Vector3 worldPosition, Quaternion rotation, Vector3 scale)
        {
            return GeometryService.LocalToWorldPoint(Vertices[vertexIndex], worldPosition, rotation, scale);
        }
    }
}