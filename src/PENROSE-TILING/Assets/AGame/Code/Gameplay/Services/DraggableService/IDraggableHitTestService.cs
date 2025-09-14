using UnityEngine;

namespace AGame.Code.Gameplay.Services.DraggableService
{
    public interface IDraggableHitTestService
    {
        bool ContainsPoint(Vector2 point, GameEntity draggable);
    }
}