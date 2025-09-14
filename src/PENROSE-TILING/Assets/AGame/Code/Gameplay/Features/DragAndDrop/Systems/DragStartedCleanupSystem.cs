using System.Collections.Generic;
using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class DragStartedCleanupSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _dragStarted;
        List<GameEntity> _buffer = new(12);

        public DragStartedCleanupSystem(GameContext game)
        {
            _dragStarted = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragStarted));
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _dragStarted.GetEntities(_buffer))
            { 
                entity.isDragStarted = false; 
            }
        }
    }
}