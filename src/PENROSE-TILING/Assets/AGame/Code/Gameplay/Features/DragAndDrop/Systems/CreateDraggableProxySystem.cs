using AGame.Code.Extensions;
using AGame.Code.Infrastructure.Ecs.Features.Entity;
using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class CreateDraggableProxySystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _draggables;

        public CreateDraggableProxySystem( GameContext game)
        {
            _draggables = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Draggable, GameMatcher.Dragging, GameMatcher.DragStarted, GameMatcher.DragOffset, GameMatcher.DragTouchId, GameMatcher.WorldPosition));
        }

        public void Execute()
        {
            foreach (GameEntity draggable in _draggables)
            {
                CreateEntity.GameEmpty()
                    .With(x => x.isDragProxy = true)
                    .With(x => x.isDragStarted = true)
                    .AddWorldPosition(draggable.WorldPosition )
                    .AddRotation(draggable.Rotation)
                    .AddDragProxyTargetId(draggable.Id)
                    .AddDragOffset(draggable.DragOffset)
                    .AddDragTouchId(draggable.DragTouchId);
            }
        }
    }
}