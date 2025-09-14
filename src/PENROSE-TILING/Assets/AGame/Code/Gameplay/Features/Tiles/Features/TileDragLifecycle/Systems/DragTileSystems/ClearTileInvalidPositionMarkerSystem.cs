using System.Collections.Generic;
using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class ClearTileInvalidPositionMarkerSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tiles;
        List<GameEntity> _buffer = new(12);

        public ClearTileInvalidPositionMarkerSystem(GameContext game)
        {
            _tiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.TilePositionInvalid));
        }
        
        public void Execute()
        {
            foreach (GameEntity tile in _tiles.GetEntities(_buffer)) 
                tile.isTilePositionInvalid = false;
        }
    }
}