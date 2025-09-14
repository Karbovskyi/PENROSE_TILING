using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class ClearTileVerticesOnDragEndSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _draggingTiles;
        private int _layerMask;

        public ClearTileVerticesOnDragEndSystem(GameContext game)
        {
            _game = game;
            _draggingTiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.DragEnded, GameMatcher.Tile));
        }

        public void Execute()
        {
            foreach (GameEntity tile in _draggingTiles)
            {
                tile.RemoveSnapLinks();
                foreach (int tileId in tile.TileVerticesIds)
                {
                    GameEntity vertex = _game.GetEntityWithId(tileId);
                    vertex.isDragging = false;
                }
            }
        }
    }
}