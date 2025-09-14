using AGame.Code.Infrastructure.Ecs.Features.Entity;
using Entitas;

namespace AGame.Code.Gameplay.Features.Input.Systems
{
  public class InitializeInputSystem : IInitializeSystem
  {
    public void Initialize()
    {
      CreateEntity.InputEmpty()
        .isInput = true;
    }
  }
}