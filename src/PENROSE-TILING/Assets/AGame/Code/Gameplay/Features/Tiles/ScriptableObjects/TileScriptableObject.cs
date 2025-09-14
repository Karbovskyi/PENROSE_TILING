using AGame.Code.Gameplay.Features.Tiles.Data;
using Shapes;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.ScriptableObjects
{
    [CreateAssetMenu(menuName = "CreateScriptableObjects/Tile", fileName = "Tile", order = 0)]
    public class TileScriptableObject : ScriptableObject
    {
        public string Name;
    
        public Vector2[] Vertices;
        public Color ValidBorderColor;
        public Color InvalidBorderColor;
        public GradientFill GradientFill;

        public Color LineColor;
        public LineData[] Lines;
    }
}
