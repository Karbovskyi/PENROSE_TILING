using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class RotateDragProxyByTouchesSystem : IExecuteSystem
    {
        private readonly InputContext _input;
        private readonly IGroup<GameEntity> _proxy;
        
        public RotateDragProxyByTouchesSystem(InputContext input, GameContext game)
        {
            _input = input;
            _proxy = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragProxy, GameMatcher.RotateTouchId, GameMatcher.DragTouchId));
        }

        public void Execute()
        {
            foreach (GameEntity proxy in _proxy)
            {
                InputEntity firstTouch = _input.GetEntityWithTouchId(proxy.DragTouchId);
                InputEntity secondTouch = _input.GetEntityWithTouchId(proxy.RotateTouchId);
                
                float previousAngle = GetAngleBetweenTouches(firstTouch.PreviousTouchPosition, secondTouch.PreviousTouchPosition);
                float currentAngle = GetAngleBetweenTouches(firstTouch.TouchPosition, secondTouch.TouchPosition);
                float deltaAngle = currentAngle-previousAngle;
                Quaternion rotationDelta = Quaternion.AngleAxis(deltaAngle, Vector3.forward);

                proxy.ReplaceRotation(proxy.Rotation * rotationDelta);
            }
        }


        private float GetAngleBetweenTouches(Vector2 p1, Vector2 p2)
        {
            Vector2 direction = p2 - p1;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }
}