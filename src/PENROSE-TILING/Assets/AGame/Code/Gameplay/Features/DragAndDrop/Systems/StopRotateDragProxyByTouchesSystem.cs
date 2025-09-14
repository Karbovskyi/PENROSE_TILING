using System.Collections.Generic;
using AGame.Code.Gameplay.Services.DraggableService;
using AGame.Code.Gameplay.Services.TimeService;
using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class StopRotateDragProxyByTouchesSystem : IExecuteSystem
    {

        private readonly InputContext _input;

        private readonly IGroup<GameEntity> _draggableProxy;

        private readonly IDraggableHitTestService _draggableHitTestService;
        private readonly ITimeService _time;
        
        List<GameEntity> _buffer = new(16);
        
        public StopRotateDragProxyByTouchesSystem(GameContext game, InputContext input)
        {
            _input = input;
            _draggableProxy = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragProxy, GameMatcher.Rotation, GameMatcher.RotateTouchId));
        }

        public void Execute()
        {
            foreach (GameEntity proxy in _draggableProxy.GetEntities(_buffer))
            {
                var touch = _input.GetEntityWithTouchId(proxy.RotateTouchId);
                if (touch == null)
                {
                    proxy.RemoveRotateTouchId();
                }
            }
        }
    }
}