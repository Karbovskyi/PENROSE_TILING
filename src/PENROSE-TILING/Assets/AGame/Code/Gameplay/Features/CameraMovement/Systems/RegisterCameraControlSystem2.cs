using System.Collections.Generic;
using Entitas;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class RegisterCameraControlSystem2 : IExecuteSystem
    {
        private readonly GameContext _game;

        private readonly IGroup<InputEntity> _touches;
        private readonly List<InputEntity> _buffer = new(10);

        public RegisterCameraControlSystem2(InputContext input, GameContext game)
        {
            _game = game;

            _touches = input.GetGroup(InputMatcher
                .AllOf( InputMatcher.TouchStarted, InputMatcher.TouchId)
                .NoneOf(InputMatcher.Processed));
        }

        public void Execute()
        {
            if(_game.isPlayerMoveDraggable) return;
            if(!_game.isPlayerMoveCamera) return;
            if (_touches.count <= 0) return;
            
            GameEntity entity = _game.playerMoveCameraEntity;
            if(entity.cameraControlTouches.First != entity.cameraControlTouches.Second) return;
            
            List<InputEntity> touches = _touches.GetEntities(_buffer);
            InputEntity second = touches[0];
                
            second.isProcessed = true;
            
            entity.ReplaceCameraControlTouches(entity.cameraControlTouches.First, second.TouchId);
        }
    }
}