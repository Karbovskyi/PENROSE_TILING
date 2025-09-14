using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class MoveDragProxySystem : IExecuteSystem
    {
        private readonly InputContext _input;
        private readonly IGroup<GameEntity> _proxy;
        private int _layerMask;

        public MoveDragProxySystem(InputContext input, GameContext game)
        {
            _input = input;
            _proxy = game.GetGroup(GameMatcher.DragProxy);
        }

        public void Execute()
        {
            foreach (GameEntity proxy in _proxy)
            {
                InputEntity touch = _input.GetEntityWithTouchId(proxy.DragTouchId);
                proxy.ReplaceWorldPosition(touch.WorldPosition + proxy.DragOffset);
            }
        }
    }
}