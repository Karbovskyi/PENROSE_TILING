using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class CalculateCameraZoomByTouchesSystem : IExecuteSystem
    {
        private readonly InputContext _input;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _cameraTargets;

        private const float ZoomSensitivity = 0.005f;

        public CalculateCameraZoomByTouchesSystem(InputContext input, GameContext game)
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
        
            float zoomOffset = GetZoomOffset(firstTouch, secondTouch) * ZoomSensitivity;

            foreach (GameEntity target in _cameraTargets)
            {
                target.ReplaceZoomOffset(zoomOffset);
            }
        }

        private static float GetZoomOffset(InputEntity firstTouch, InputEntity secondTouch)
        {
            float previousDistance = Vector2.Distance(firstTouch.PreviousTouchPosition, secondTouch.PreviousTouchPosition);
            float currentDistance = Vector2.Distance(firstTouch.TouchPosition, secondTouch.TouchPosition);
            float deltaDistance = currentDistance - previousDistance;
            return deltaDistance;
        }
    }
}