using System.Collections.Generic;
using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class DragEndedCleanupSystem : ICleanupSystem
    {
        private readonly IGroup<GameEntity> _dragEnded;
        List<GameEntity> _buffer = new(12);

        public DragEndedCleanupSystem(GameContext game)
        {
            _dragEnded = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragEnded));
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _dragEnded.GetEntities(_buffer))
            { 
                entity.isDragEnded = false;
                entity.isDragging = false;
            }
        }
    }
}