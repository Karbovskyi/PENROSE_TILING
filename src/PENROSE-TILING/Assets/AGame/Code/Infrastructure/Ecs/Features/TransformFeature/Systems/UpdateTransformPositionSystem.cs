using Entitas;

namespace AGame.Code.Infrastructure.Ecs.Features.TransformUpdators.Systems
{
  public class UpdateTransformPositionSystem : IExecuteSystem
  {
    private readonly IGroup<GameEntity> _movers;

    public UpdateTransformPositionSystem(GameContext game)
    {
      _movers = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.Transform, GameMatcher.WorldPosition));
    }
    
    public void Execute()
    {
      foreach (GameEntity mover in _movers)
      {
        mover.Transform.position = mover.WorldPosition;
      }
    }
  }
}