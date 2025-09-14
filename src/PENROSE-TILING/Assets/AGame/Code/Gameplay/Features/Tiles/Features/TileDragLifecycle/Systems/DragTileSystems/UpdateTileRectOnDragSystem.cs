using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class UpdateTileRectOnDragSystem : IExecuteSystem
    {
        private readonly List<GameEntity> _buffer = new(32);
        private readonly IGroup<GameEntity> _tiles;
        
        public UpdateTileRectOnDragSystem(GameContext game)
        {
            _tiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Tile, GameMatcher.Dragging));
        }

        public void Execute()
        {
            foreach (GameEntity tile in _tiles.GetEntities(_buffer))
            {
                Rect rect = tile.TileStaticData.GetRect(tile.WorldPosition, tile.Rotation, Vector3.one);
                tile.ReplaceRect(rect);
            }
        }
    }
}