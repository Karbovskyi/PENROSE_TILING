using Entitas;

namespace AGame.Code.Infrastructure.Ecs.Features.TransformUpdators.Systems
{
    public class UpdateTransformRotationSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _movers;

        public UpdateTransformRotationSystem(GameContext game)
        {
            _movers = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Transform, GameMatcher.Rotation));
        }
    
        public void Execute()
        {
            foreach (GameEntity mover in _movers)
            {
                mover.Transform.rotation = mover.Rotation;
            }
        }
    }
}