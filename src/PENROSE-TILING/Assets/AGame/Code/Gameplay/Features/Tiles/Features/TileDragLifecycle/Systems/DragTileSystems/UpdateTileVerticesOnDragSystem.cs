using AGame.Code.Gameplay.StaticServices.GeometryService;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class UpdateTileVerticesOnDragSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _draggingTiles;

        public UpdateTileVerticesOnDragSystem(GameContext game)
        {
            _game = game;
            _draggingTiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Dragging, GameMatcher.Tile));
        }

        public void Execute()
        {
            foreach (GameEntity tile in _draggingTiles)
            {
                Vector2[] verticesLocal = tile.TileStaticData.Vertices;

                for (var i = 0; i < verticesLocal.Length; i++)
                {
                    Vector3 vertexPosition = GeometryService.LocalToWorldPoint(verticesLocal[i], tile.WorldPosition, tile.Rotation, Vector3.one);
                    GameEntity vertex = _game.GetEntityWithId(tile.TileVerticesIds[i]);
                    vertex.ReplaceWorldPosition(vertexPosition);
                }
            }
        }
    }
}