using AGame.Code.Gameplay.Services.CameraProvider;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class CameraMoveSystem : IExecuteSystem
    {
        private readonly InputContext _input;
        private readonly GameContext _game;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGroup<GameEntity> _cameraTargets;
        
        public CameraMoveSystem(InputContext input, GameContext game, ICameraProvider cameraProvider)
        {
            _input = input;
            _game = game;
            _cameraProvider = cameraProvider;
            _cameraTargets = game.GetGroup(GameMatcher.CameraTarget);
        }

        public void Execute()
        {
            if (!_game.isPlayerMoveCamera) return;

            GameEntity gameEntity = _game.playerMoveCameraEntity;
            InputEntity firstTouch = _input.GetEntityWithTouchId(gameEntity.cameraControlTouches.First);
            InputEntity secondTouch = _input.GetEntityWithTouchId(gameEntity.cameraControlTouches.Second);
            
            Vector3 offset = GetOffset(firstTouch, secondTouch);

            foreach (GameEntity target in _cameraTargets)
            {
                target.ReplaceWorldPosition(target.WorldPosition - offset);
            }
        }

        private Vector3 GetOffset(InputEntity firstTouch, InputEntity secondTouch)
        {
            Vector2 offsetInPixels = GetOffsetInPixels(firstTouch, secondTouch);
            return GetOffsetInUnits(offsetInPixels);
        }

        private Vector2 GetOffsetInPixels(InputEntity firstTouch, InputEntity secondTouch)
        {
            Vector2 previousMiddle = GetMiddlePoint(firstTouch.PreviousTouchPosition, secondTouch.PreviousTouchPosition);
            Vector2 currentMiddle = GetMiddlePoint(firstTouch.TouchPosition, secondTouch.TouchPosition);
            return currentMiddle - previousMiddle;
        }

        private Vector3 GetOffsetInUnits(Vector2 offsetInPixels)
        {
            Camera camera = _cameraProvider.MainCamera;
            return ScreenToWorldPoint(camera, offsetInPixels) - ScreenToWorldPoint(camera, Vector2.zero);
        }

        private Vector2 GetMiddlePoint(Vector2 p1, Vector2 p2) 
            => (p1 + p2) / 2;

        private Vector3 ScreenToWorldPoint(Camera camera, Vector2 offsetInPixels) 
            => camera.ScreenToWorldPoint(offsetInPixels);
    }
}