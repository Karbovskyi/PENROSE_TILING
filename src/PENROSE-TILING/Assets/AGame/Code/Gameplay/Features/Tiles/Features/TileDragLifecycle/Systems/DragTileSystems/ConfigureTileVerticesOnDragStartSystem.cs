using AGame.Code.Gameplay.Features.Tiles.Factory;
using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class ConfigureTileVerticesOnDragStartSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ITilesFactory _tilesFactory;
        private readonly IGroup<GameEntity> _dragStartedTiles;
        private int _layerMask;

        public ConfigureTileVerticesOnDragStartSystem(GameContext game, ITilesFactory tilesFactory)
        {
            _game = game;
            _tilesFactory = tilesFactory;
            _dragStartedTiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.DragStarted, GameMatcher.Tile));
        }

        public void Execute()
        {
            foreach (GameEntity tile in _dragStartedTiles)
            {
                for (int i = 0; i < tile.TileVerticesIds.Length; i++)
                {
                    GameEntity vertexToCopy = _game.GetEntityWithId(tile.TileVerticesIds[i]);

                    if (vertexToCopy.VertexConnectionCount == 0)
                    {
                        vertexToCopy.isDragging = true;
                    }
                    else
                    {
                        vertexToCopy.ReplaceVertexConnectionCount(vertexToCopy.VertexConnectionCount - 1);
                        GameEntity newVertex = _tilesFactory.CopyVertex(vertexToCopy);
                        newVertex.isDragging = true;
                        tile.TileVerticesIds[i] = newVertex.Id;
                    }
                }
            }
        }
    }
}