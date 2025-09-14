using AGame.Code.Infrastructure.Ecs.Features.TransformUpdators.Systems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Infrastructure.Ecs.Features.TransformUpdators
{
    public class TransformFeature : Feature
    {
        public TransformFeature(ISystemFactory systems)
        {
            Add(systems.Create<UpdateTransformPositionSystem>());
            Add(systems.Create<UpdateTransformRotationSystem>());
            Add(systems.Create<UpdateTransformScaleSystem>());
        }
    }
}