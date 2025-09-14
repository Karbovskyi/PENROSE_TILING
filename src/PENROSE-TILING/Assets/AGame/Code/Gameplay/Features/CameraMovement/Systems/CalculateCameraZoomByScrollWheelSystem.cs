using AGame.Code.Gameplay.Services.InputService;
using Entitas;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class CalculateCameraZoomByScrollWheelSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly IGroup<GameEntity> _cameraTargets;

        private const float ZoomSpeed = 1f; 

        public CalculateCameraZoomByScrollWheelSystem(GameContext game, IInputService inputService)
        {
            _inputService = inputService;
            _cameraTargets = game.GetGroup(GameMatcher.AllOf(GameMatcher.Camera, GameMatcher.CinemachineCamera));
        }

        public void Execute()
        {
            if (!_inputService.HasScrollWheel()) 
                return;

            float scrollValue = _inputService.GetScrollWheelValue(); 
            float zoomOffset = scrollValue * ZoomSpeed;

            foreach (GameEntity target in _cameraTargets)
            {
                target.ReplaceZoomOffset(zoomOffset);
            }
        }
    }
}