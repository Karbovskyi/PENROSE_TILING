using AGame.Code.Gameplay.Services.TimeService;
using Entitas;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class CalculateCameraZoomByAxisSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _input;
        private readonly IGroup<GameEntity> _cameraTargets;
        private readonly ITimeService _time;

        private const float ZoomSpeed = 2f;

        public CalculateCameraZoomByAxisSystem(InputContext input, GameContext game, ITimeService time)
        {
            _time = time;
            _input = input.GetGroup(InputMatcher.InputAxis);
            _cameraTargets = game.GetGroup(GameMatcher.AllOf(GameMatcher.Camera, GameMatcher.CinemachineCamera));
        }

        public void Execute()
        {
            foreach (GameEntity target in _cameraTargets)
            foreach (InputEntity input in _input)
            {
                float zoomOffset = input.InputAxis.y * ZoomSpeed * _time.DeltaTime;
                target.ReplaceZoomOffset(zoomOffset);
            }
        }
    }
}