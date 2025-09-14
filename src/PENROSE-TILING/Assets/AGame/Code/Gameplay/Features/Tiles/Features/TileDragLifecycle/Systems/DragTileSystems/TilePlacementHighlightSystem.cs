using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class TilePlacementHighlightSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _tiles;

        public TilePlacementHighlightSystem(GameContext game)
        {
            _tiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.Dragging, GameMatcher.TileView));
        }
        
        public void Execute()
        {
            foreach (GameEntity tile in _tiles)
            {
                //tile.TileView.SetValidHighlight(!tile.isTilePositionValid);
            }
        }
    }
}