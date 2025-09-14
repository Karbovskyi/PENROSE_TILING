using AGame.Code.Gameplay.Features.CameraMovement;
using AGame.Code.Gameplay.Features.DragAndDrop;
using AGame.Code.Gameplay.Features.Input;
using AGame.Code.Gameplay.Features.Tiles.Features.TileDebug;
using AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle;
using AGame.Code.Gameplay.Features.Tiles.Features.TileSpawn;
using AGame.Code.Infrastructure.Ecs.Features.BindView;
using AGame.Code.Infrastructure.Ecs.Features.Destruct;
using AGame.Code.Infrastructure.Ecs.Features.TransformUpdators;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Gameplay.Features
{
    public class TillingSceneFeature : Feature
    {
        public TillingSceneFeature(ISystemFactory systems)
        {
            Add(systems.Create<InputFeature>());
            
            Add(systems.Create<TileSpawnFeature>());

            Add(systems.Create<DragAndDropFeature>());
            Add(systems.Create<CameraMovementFeature>());
            Add(systems.Create<TileDragLifecycleFeature>());
            Add(systems.Create<TileDebugFeature>());
            
            Add(systems.Create<BindViewFeature>());
            Add(systems.Create<TransformFeature>());
            
            Add(systems.Create<ProcessDestructedFeature>());
        }
    }
}