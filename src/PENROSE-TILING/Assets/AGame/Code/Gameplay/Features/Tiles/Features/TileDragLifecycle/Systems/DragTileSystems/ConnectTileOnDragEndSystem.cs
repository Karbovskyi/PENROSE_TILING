using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class ConnectTileOnDragEndSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _droppedTiles;

        public ConnectTileOnDragEndSystem(GameContext game)
        {
            _game = game;
            _droppedTiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Tile, GameMatcher.DragEnded, GameMatcher.SnapLinks)
                .NoneOf(GameMatcher.TilePositionInvalid));
        }
        
        public void Execute()
        {
            foreach (GameEntity tile in _droppedTiles)
            {
                MergeTileVertices(tile);
            }
        }

        private void MergeTileVertices(GameEntity tile)
        {
            for (int i = 0; i < tile.SnapLinks.Count; i++)
            {
                GameEntity targetVertex = _game.GetEntityWithId(tile.SnapLinks[i].TargetVertexId);
                targetVertex.ReplaceVertexConnectionCount(targetVertex.VertexConnectionCount + 1);
                    
                int dragVertexId = tile.SnapLinks[i].SourceVertexId;
                GameEntity dragVertex = _game.GetEntityWithId(dragVertexId);
                dragVertex.isDestructed = true;
                    
                int dragVertexIndex = tile.SnapLinks[i].SourceVertexIndex;
                tile.TileVerticesIds[dragVertexIndex] = targetVertex.Id;
            }
        }
    }
}