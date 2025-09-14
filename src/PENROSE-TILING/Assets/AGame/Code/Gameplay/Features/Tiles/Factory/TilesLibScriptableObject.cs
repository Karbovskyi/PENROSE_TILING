using AGame.Code.Gameplay.Features.Tiles.ScriptableObjects;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Factory
{
    [CreateAssetMenu(menuName = "CreateScriptableObjects/TilesLib", fileName = "TilesLib", order = 0)]
    public class TilesLibScriptableObject : ScriptableObject
    {
        public TileScriptableObject[] Tiles;
    }
}