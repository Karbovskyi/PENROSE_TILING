using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class DeleteTileOnDragEndSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _droppedTiles;

        public DeleteTileOnDragEndSystem(GameContext game)
        {
            _game = game;
            _droppedTiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Tile, GameMatcher.DragEnded, GameMatcher.TilePositionInvalid));
        }
        
        public void Execute()
        {
            foreach (GameEntity tile in _droppedTiles)
            {
                tile.isDestructed = true;

                for (int i = 0; i < tile.TileVerticesIds.Length; i++)
                {
                    _game.GetEntityWithId(tile.TileVerticesIds[i]).isDestructed = true;
                }
            }
        }
    }
}