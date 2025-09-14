using System.Collections.Generic;
using AGame.Code.Gameplay.Features.DragAndDrop.Extensions;
using AGame.Code.Gameplay.Services.DraggableService;
using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class DragStartSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private IDraggableHitTestService _draggableHitTestService;
        
        private readonly IGroup<InputEntity> _touch;
        private readonly IGroup<GameEntity> _draggables;
        
        List<InputEntity> _buffer1 = new(2);
        List<GameEntity> _buffer2 = new(16);
        
        public DragStartSystem(InputContext input, GameContext game, IDraggableHitTestService draggableHitTestService)
        {
            _game = game;
            _draggableHitTestService = draggableHitTestService;
            
            _touch = input.GetGroup(InputMatcher.AllOf(InputMatcher.TouchId, InputMatcher.TouchStarted)
                .NoneOf(InputMatcher.Processed));
            
            _draggables = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Draggable, GameMatcher.WorldPosition)
                .NoneOf(GameMatcher.Dragging));
        }

        public void Execute()
        {
            if(_game.isPlayerMoveCamera) return;
            
            foreach (InputEntity touch in _touch.GetEntities(_buffer1))
            foreach (GameEntity draggable in _draggables.GetEntities(_buffer2))
            {
                if(!_draggableHitTestService.ContainsPoint(touch.WorldPosition, draggable)) continue;
                
                draggable.StartDrag(touch);
            }
        }
    }
}