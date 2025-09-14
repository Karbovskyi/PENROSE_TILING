using AGame.Code.Gameplay.Features.Tiles.Features.TileDebug.Systems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDebug
{
    public class TileDebugFeature : Feature
    {
        public TileDebugFeature(ISystemFactory systems)
        {
            Add(systems.Create<DebugDrawTileSystem>());
            Add(systems.Create<DebugDrawRectSystem>());
            Add(systems.Create<DebugDrawProxySnapLinksSystem>());
        }
    }
}