using Entitas;

namespace AGame.Code.Infrastructure.Ecs.Features.TransformUpdators.Systems
{
    public class UpdateTransformScaleSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _transforms;

        public UpdateTransformScaleSystem(GameContext game)
        {
            _transforms = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Transform, GameMatcher.Scale));
        }
    
        public void Execute()
        {
            foreach (GameEntity entity in _transforms)
            {
                entity.Transform.localScale = entity.Scale;
            }
        }
    }
}