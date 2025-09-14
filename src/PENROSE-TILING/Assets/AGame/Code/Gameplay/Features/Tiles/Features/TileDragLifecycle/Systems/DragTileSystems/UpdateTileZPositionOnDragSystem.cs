using AGame.Code.Extensions;
using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class UpdateTileZPositionOnDragSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _draggingTiles;
        private int _layerMask;

        public UpdateTileZPositionOnDragSystem(GameContext game)
        {
            _draggingTiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Dragging, GameMatcher.Tile)
                .NoneOf(GameMatcher.DragEnded));
        }

        public void Execute()
        {
            foreach (GameEntity tile in _draggingTiles)
            {
                tile.ReplaceWorldPosition(tile.WorldPosition.SetZ(-2));
            }
        }
    }
}