using System.Collections.Generic;
using AGame.Code.Gameplay.Features.Tiles.Data;
using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class ConfigureDragProxyOnDragStartSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _proxy;

        public ConfigureDragProxyOnDragStartSystem(GameContext game)
        {
            _game = game;
            _proxy = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.DragProxy, GameMatcher.DragStarted));
        }

        public void Execute()
        {
            foreach (GameEntity proxy in _proxy)
            {
                GameEntity draggable = _game.GetEntityWithId(proxy.DragProxyTargetId);

                if (draggable.isTile)
                {
                    proxy.AddSnapLinks(new List<SnapLinkData>(4));
                    proxy.AddTileStaticData(draggable.TileStaticData);
                }
            }
        }
    }
}