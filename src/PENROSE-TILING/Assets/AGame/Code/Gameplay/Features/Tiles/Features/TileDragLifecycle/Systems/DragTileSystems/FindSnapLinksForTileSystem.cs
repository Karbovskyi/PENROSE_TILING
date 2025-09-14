using AGame.Code.Gameplay.Features.Tiles.Data;
using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class FindSnapLinksForTileSystem : IExecuteSystem
    {
        private const float SqrSnapDistance = 0.00001f;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _draggingTiles;
        private readonly IGroup<GameEntity> _verteces;

        public FindSnapLinksForTileSystem(GameContext game)
        {
            _game = game;
            _draggingTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.Dragging, GameMatcher.SnapLinks)
                .NoneOf(GameMatcher.TilePositionInvalid));
            
            _verteces = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Vertex, GameMatcher.WorldPosition)
                .NoneOf(GameMatcher.Dragging));
        }
        
        public void Execute()
        {
            foreach (GameEntity dragTile in _draggingTiles)
            {
                for (var i = 0; i < dragTile.TileVerticesIds.Length; i++)
                {
                    GameEntity dragVertex = _game.GetEntityWithId(dragTile.TileVerticesIds[i]);
                    foreach (GameEntity vertex in _verteces)
                    {
                        float sqrDistance = (vertex.WorldPosition - dragVertex.WorldPosition).sqrMagnitude;

                        if (sqrDistance <= SqrSnapDistance)
                        {
                            dragTile.SnapLinks.Add(new SnapLinkData(dragVertex.Id,i, vertex.Id));
                        }
                    }
                }
            }
        }
    }
}