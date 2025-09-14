using AGame.Code.Gameplay.StaticServices.DebugService;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDebug.Systems
{
    public class DebugDrawTileSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _tiles;
        private int _layerMask;

        public DebugDrawTileSystem(GameContext game)
        {
            _game = game;
            _tiles = game.GetGroup(GameMatcher.Tile);
        }
        
        public void Execute()
        {
            foreach (GameEntity tile in _tiles)
            {
                DebugTileEdges(tile);
            }
        }
        
        private void DebugTileEdges(GameEntity tile)
        {
            int vertexCount = tile.TileVerticesIds.Length;

            for (int i = 0; i < vertexCount; i++)
            {
                GameEntity vertex = _game.GetEntityWithId(tile.TileVerticesIds[i]);
                GameEntity nextVertex = _game.GetEntityWithId(
                    tile.TileVerticesIds[(i + 1) % vertexCount]);

                DrawVertex(vertex);
                DrawEdge(vertex, nextVertex);
            }
        }
        
        private static void DrawVertex(GameEntity vertex)
        {
            DebugService.DrawDebugCircle(vertex.WorldPosition, 0.1f, 4, Color.cyan);
        }

        private static void DrawEdge(GameEntity from, GameEntity to)
        {
            Debug.DrawLine(from.WorldPosition, to.WorldPosition, Color.red);
        }
    }
}