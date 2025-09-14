using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class UpdateDraggableByDragProxySystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _draggableProxy;
        private int _layerMask;

        public UpdateDraggableByDragProxySystem(GameContext game)
        {
            _game = game;
            _draggableProxy = game.GetGroup(GameMatcher.DragProxy);
        }

        public void Execute()
        {
            foreach (GameEntity proxy in _draggableProxy)
            {
                GameEntity tile = _game.GetEntityWithId(proxy.DragProxyTargetId);
                tile.ReplaceWorldPosition(proxy.WorldPosition);
                tile.ReplaceRotation(proxy.Rotation);
            }
        }
    }
}