using Entitas;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class UnregisterCameraControlSystem : ICleanupSystem
    {
        private readonly InputContext _input;
        private readonly GameContext _game;

        public UnregisterCameraControlSystem(InputContext input, GameContext game)
        {
            _input = input;
            _game = game;
        }

        public void Cleanup()
        {
            if (!_game.isPlayerMoveCamera) return;
            
            GameEntity entity = _game.playerMoveCameraEntity;
            InputEntity firstTouch = _input.GetEntityWithTouchId(entity.cameraControlTouches.First);
            InputEntity secondTouch = _input.GetEntityWithTouchId(entity.cameraControlTouches.Second);
            if (firstTouch == null)
            {
                entity.Destroy();
            }
            else if(secondTouch == null)
            {
                entity.ReplaceCameraControlTouches(entity.cameraControlTouches.First, entity.cameraControlTouches.First);
            }
        }
    }
}