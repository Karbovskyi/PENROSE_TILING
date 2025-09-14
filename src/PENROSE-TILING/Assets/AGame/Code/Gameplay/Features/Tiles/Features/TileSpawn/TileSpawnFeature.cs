using AGame.Code.Gameplay.Features.Tiles.Features.TileSpawn.Systems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileSpawn
{
    public class TileSpawnFeature : Feature
    {
        public TileSpawnFeature(ISystemFactory systems)
        {
            Add(systems.Create<CreateButtonsSystem>());
            //Add(systems.Create<ViewportRectToWorldRectSystem>());
            Add(systems.Create<SpawnTileSystem>());
            //Add(systems.Create<InitializeTileViewSystem>());
        }
    }
}