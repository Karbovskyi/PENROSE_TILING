using System.Collections.Generic;
using AGame.Code.Gameplay.Features.Tiles.Data;
using AGame.Code.Gameplay.Features.Tiles.Factory;
using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class ConfigureTileOnDragStartSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ITilesFactory _tilesFactory;
        private readonly IGroup<GameEntity> _dragStartedTiles;

        public ConfigureTileOnDragStartSystem(GameContext game, ITilesFactory tilesFactory)
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
                tile.AddSnapLinks(new List<SnapLinkData>(4));
            }
        }
    }
}