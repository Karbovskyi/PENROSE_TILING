using AGame.Code.Gameplay.Features.Input.Systems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Gameplay.Features.Input
{
  public class InputFeature : Feature
  {
    public InputFeature(ISystemFactory systems)
    {
      Add(systems.Create<InitializeInputSystem>());
      Add(systems.Create<MouseDetectSystem>());
      Add(systems.Create<TouchDetectSystem>());
      Add(systems.Create<InputAxisDetectSystem>());
      Add(systems.Create<AddWorldPositionToTouchSystem>());
      
      Add(systems.Create<TouchStartedCleanupSystem>());
      Add(systems.Create<TouchEndedCleanupSystem>());
    }
  }
}