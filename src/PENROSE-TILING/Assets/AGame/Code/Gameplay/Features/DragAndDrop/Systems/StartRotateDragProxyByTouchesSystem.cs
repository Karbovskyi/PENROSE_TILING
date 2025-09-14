using System.Collections.Generic;
using AGame.Code.Gameplay.Services.DraggableService;
using AGame.Code.Gameplay.Services.TimeService;
using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class StartRotateDragProxyByTouchesSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _touch;
        private readonly IGroup<GameEntity> _draggableProxy;
        private readonly ITimeService _time;

        List<InputEntity> _buffer1 = new(16);
        List<GameEntity> _buffer2 = new(16);
        
        public StartRotateDragProxyByTouchesSystem(InputContext input, GameContext game, IDraggableHitTestService draggableHitTestService)
        {
            _touch = input.GetGroup(InputMatcher.AllOf(InputMatcher.TouchId, InputMatcher.TouchStarted)
                .NoneOf(InputMatcher.Processed));
            
            _draggableProxy = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragProxy, GameMatcher.Rotation)
                .NoneOf(GameMatcher.RotateTouchId));
        }

        public void Execute()
        {
            foreach (InputEntity touch in _touch.GetEntities(_buffer1))
            foreach (GameEntity proxy in _draggableProxy.GetEntities(_buffer2))
            {
                touch.isProcessed = true;
                proxy.AddRotateTouchId(touch.TouchId);
            }
        }
    }
}