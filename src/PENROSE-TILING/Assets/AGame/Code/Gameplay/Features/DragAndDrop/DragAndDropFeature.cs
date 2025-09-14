using AGame.Code.Gameplay.Features.DragAndDrop.Systems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Gameplay.Features.DragAndDrop
{
    public class DragAndDropFeature : Feature
    {
        public DragAndDropFeature(ISystemFactory systems)
        {
            Add(systems.Create<RotateDragProxyByAxisSystem>());
            Add(systems.Create<StartRotateDragProxyByTouchesSystem>());
            Add(systems.Create<StopRotateDragProxyByTouchesSystem>());
            Add(systems.Create<RotateDragProxyByTouchesSystem>());
            
            Add(systems.Create<DragStartSystem>());
            Add(systems.Create<CreateDraggableProxySystem>());
            Add(systems.Create<DragEndSystem>());

            Add(systems.Create<MoveDragProxySystem>());
            Add(systems.Create<UpdateDraggableByDragProxySystem>());

            Add(systems.Create<DragStartedCleanupSystem>());
            Add(systems.Create<DragEndedCleanupSystem>());
            Add(systems.Create<PlayerMoveDraggableMarkerCleanupSystem>());
        }
    }
}