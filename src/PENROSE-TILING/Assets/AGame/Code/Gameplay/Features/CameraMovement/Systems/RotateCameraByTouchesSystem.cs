using AGame.Code.Extensions;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class RotateCameraByTouchesSystem : IExecuteSystem
    {
        private readonly InputContext _input;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _cameraTargets;

        public RotateCameraByTouchesSystem(InputContext input, GameContext game)
        {
            _input = input;
            _game = game;
            _cameraTargets = game.GetGroup(GameMatcher.AllOf(GameMatcher.Camera, GameMatcher.CinemachineCamera));
        }

        public void Execute()
        {
            if (!_game.isPlayerMoveCamera) return;

            GameEntity gameEntity = _game.playerMoveCameraEntity;
            InputEntity firstTouch = _input.GetEntityWithTouchId(gameEntity.cameraControlTouches.First);
            InputEntity secondTouch = _input.GetEntityWithTouchId(gameEntity.cameraControlTouches.Second);
            
            if(firstTouch.WorldPosition.Approximately(secondTouch.WorldPosition)) return;
            
            Quaternion rotationOffset = GetRotationOffset(firstTouch, secondTouch);

            foreach (GameEntity target in _cameraTargets)
            {
                target.ReplaceRotation(target.Rotation * rotationOffset);
            }
        }

        private Quaternion GetRotationOffset(InputEntity firstTouch, InputEntity secondTouch)
        {
            float previousAngle = GetAngleBetweenTouches(firstTouch.PreviousTouchPosition, secondTouch.PreviousTouchPosition);
            float currentAngle = GetAngleBetweenTouches(firstTouch.TouchPosition, secondTouch.TouchPosition);
            float deltaAngle = previousAngle - currentAngle;
            Quaternion rotationOffset = Quaternion.AngleAxis(deltaAngle, Vector3.forward);
            return rotationOffset;
        }
        
        private float GetAngleBetweenTouches(Vector2 p1, Vector2 p2)
        {
            Vector2 direction = p2 - p1;
            return Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }
}