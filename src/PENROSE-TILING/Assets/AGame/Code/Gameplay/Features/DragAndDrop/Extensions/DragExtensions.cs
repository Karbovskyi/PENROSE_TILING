using AGame.Code.Extensions;
using AGame.Code.Infrastructure.Ecs.Features.Entity;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Extensions
{
    public static class DragExtensions
    {
        public static void StartDrag(this GameEntity draggable, InputEntity touch)
        {
            TryCreatePlayerMoveDraggableEntity();
            
            touch.isProcessed = true;
            
            draggable.isDragging = true;
            draggable.isDragStarted = true;
            Vector3 dragOffset = draggable.WorldPosition - touch.WorldPosition;
            
            CreateEntity.GameEmpty()
                .With(x => x.isDragProxy = true)
                .With(x => x.isDragStarted = true)
                .AddWorldPosition(draggable.WorldPosition )
                .AddRotation(draggable.Rotation)
                .AddDragProxyTargetId(draggable.Id)
                .AddDragOffset(dragOffset)
                .AddDragTouchId(touch.TouchId);
        }
        
        private static void TryCreatePlayerMoveDraggableEntity()
        {
            if(Contexts.sharedInstance.game.isPlayerMoveDraggable) return;
            
            CreateEntity.GameEmpty()
                .isPlayerMoveDraggable = true;
        }
    }
}