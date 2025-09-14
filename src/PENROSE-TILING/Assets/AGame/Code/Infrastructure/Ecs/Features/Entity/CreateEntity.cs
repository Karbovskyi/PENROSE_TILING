namespace AGame.Code.Infrastructure.Ecs.Features.Entity
{
  public static class CreateEntity
  {
    public static GameEntity GameEmpty() =>
      Contexts.sharedInstance.game.CreateEntity();
    
    public static InputEntity InputEmpty() =>
      Contexts.sharedInstance.input.CreateEntity();
  }
}