using AGame.Code.Infrastructure.Ecs.Features.Destruct.Systems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Infrastructure.Ecs.Features.Destruct
{
  public class ProcessDestructedFeature : Feature
  {
    public ProcessDestructedFeature(ISystemFactory systems)
    {
      Add(systems.Create<SelfDestructTimerSystem>());
      
      Add(systems.Create<CleanupGameDestructedViewSystem>());
      Add(systems.Create<CleanupGameDestructedSystem>());
    }
  }
}