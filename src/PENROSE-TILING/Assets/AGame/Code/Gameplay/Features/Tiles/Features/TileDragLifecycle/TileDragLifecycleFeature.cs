using AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle
{
    public class TileDragLifecycleFeature : Feature
    {
        public TileDragLifecycleFeature(ISystemFactory systems)
        {
            Add(systems.Create<ConfigureDragProxyOnDragStartSystem>());
            Add(systems.Create<ConfigureTileOnDragStartSystem>());
            Add(systems.Create<ConfigureTileVerticesOnDragStartSystem>());
            
            Add(systems.Create<FindSnapLinksForProxySystem>());
            Add(systems.Create<TileSnapAlignmentSystem>());
            
            Add(systems.Create<ClearTileInvalidPositionMarkerSystem>());
            Add(systems.Create<CheckTileOverlapWithOtherTilesSystem>());
            Add(systems.Create<CheckTileOverlapWithButtonsZoneSystem>());
            
            Add(systems.Create<FindSnapLinksForTileSystem>());
            
            Add(systems.Create<UpdateTileVerticesOnDragSystem>());
            Add(systems.Create<UpdateTileRectOnDragSystem>());
            
            Add(systems.Create<ConnectTileOnDragEndSystem>());
            Add(systems.Create<DeleteTileOnDragEndSystem>());
            Add(systems.Create<ClearTileVerticesOnDragEndSystem>());
            
            //Add(systems.Create<UpdateTileZPositionOnDragSystem>());
            //Add(systems.Create<TilePlacementHighlightSystem>());
            
            Add(systems.Create<SnapLinksCleanupSystem>());
        }
    }
}