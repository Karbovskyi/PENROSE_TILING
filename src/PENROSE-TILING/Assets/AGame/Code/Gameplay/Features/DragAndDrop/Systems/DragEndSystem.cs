using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class DragEndSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<InputEntity> _touch;
        private readonly IGroup<GameEntity> _proxy;
        private int _layerMask;

        public DragEndSystem(InputContext input, GameContext game)
        {
            _game = game;
            _touch = input.GetGroup(InputMatcher.AllOf(InputMatcher.TouchId, InputMatcher.TouchEnded));
            _proxy = game.GetGroup(GameMatcher.DragProxy);
        }

        public void Execute()
        {
            foreach (InputEntity touch in _touch)
            foreach (GameEntity proxy in _proxy)
            {
                if(touch.TouchId != proxy.DragTouchId) continue;
                
                proxy.isDragEnded = true;
                proxy.isDestructed = true;
                proxy.ReplaceDragOffset(proxy.DragOffset);
                
                GameEntity draggable = _game.GetEntityWithId(proxy.DragProxyTargetId);
                draggable.isDragEnded = true;
            }
        }
    }
}