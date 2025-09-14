using AGame.Code.Gameplay.Services.TimeService;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class RotateCameraByAxisSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _input;
        private readonly IGroup<GameEntity> _cameraTargets;
        private readonly GameContext _game;
        private readonly ITimeService _time;

        private const float RotationSpeed = -180f;

        public RotateCameraByAxisSystem(InputContext input, GameContext game, ITimeService time)
        {
            _game = game;
            _time = time;
            _input = input.GetGroup(InputMatcher.InputAxis);
            _cameraTargets = game.GetGroup(GameMatcher.AllOf(GameMatcher.Camera, GameMatcher.CinemachineCamera));
        }

        public void Execute()
        {
            foreach (GameEntity target in _cameraTargets)
            foreach (InputEntity input in _input)
            {
                if (_game.isPlayerMoveDraggable) return;
                
                Quaternion deltaRotation = Quaternion.Euler(0, 0, input.InputAxis.x * RotationSpeed * _time.DeltaTime);
                Quaternion newRotation = target.Rotation * deltaRotation;
                
                target.ReplaceRotation(newRotation);
            }
        }
    }
}