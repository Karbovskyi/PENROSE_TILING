using System.Collections.Generic;
using AGame.Code.Extensions;
using AGame.Code.Infrastructure.Ecs.Features.Entity;
using Entitas;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    /*
    public class RegisterCameraControlSystem : IExecuteSystem
    {
        private readonly InputContext _input;
        private readonly GameContext _game;

        private readonly IGroup<InputEntity> _touches;
        private readonly List<InputEntity> _buffer = new(10);

        public RegisterCameraControlSystem(InputContext input, GameContext game)
        {
            _input = input;
            _game = game;

            _touches = input.GetGroup(InputMatcher
                .AllOf(InputMatcher.TouchId)
                .NoneOf(InputMatcher.Processed));
        }

        public void Execute()
        {
            if(_game.isPlayerMoveDraggable) return;
            
            if (_touches.count >= 2  && !_input.isPlayerControlledCamera)
            {
                List<InputEntity> touches = _touches.GetEntities(_buffer);
                InputEntity first = touches[0];
                InputEntity second = touches[1];
                first.isProcessed = true;
                second.isProcessed = true;

                CreatePlayerCameraControlEntity(first, second);
            }
        }

        private static void CreatePlayerCameraControlEntity(InputEntity first, InputEntity second)
        {
            CreateEntity.InputEmpty()
                .With(x => x.isPlayerControlledCamera = true)
                .AddCameraControlTouches(first.TouchId, second.TouchId);
        }
    }
    */
    
    public class RegisterCameraControlSystem : IExecuteSystem
    {
        private readonly GameContext _game;

        private readonly IGroup<InputEntity> _touches;
        private readonly List<InputEntity> _buffer = new(10);

        public RegisterCameraControlSystem(InputContext input, GameContext game)
        {
            _game = game;

            _touches = input.GetGroup(InputMatcher
                .AllOf( InputMatcher.TouchStarted, InputMatcher.TouchId)
                .NoneOf(InputMatcher.Processed));
        }

        public void Execute()
        {
            if(_game.isPlayerMoveDraggable) return;
            if(_game.isPlayerMoveCamera) return;
            
            if (_touches.count > 0)
            {
                List<InputEntity> touches = _touches.GetEntities(_buffer);
                InputEntity first = touches[0];
                
                first.isProcessed = true;

                CreatePlayerMoveCameraEntity(first, first);
            }
        }

        private void CreatePlayerMoveCameraEntity(InputEntity first, InputEntity second)
        {
            CreateEntity.GameEmpty()
                .With(x => x.isPlayerMoveCamera = true)
                .AddCameraControlTouches(first.TouchId, second.TouchId);
        }
    }
}