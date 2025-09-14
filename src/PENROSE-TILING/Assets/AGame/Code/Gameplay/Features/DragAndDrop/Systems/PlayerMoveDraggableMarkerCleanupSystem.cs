using Entitas;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class PlayerMoveDraggableMarkerCleanupSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _entities;

        public PlayerMoveDraggableMarkerCleanupSystem(GameContext game)
        {
            _game = game;
            _entities = game.GetGroup(GameMatcher.AllOf(GameMatcher.Dragging));
        }

        public void Cleanup()
        {
            if(!_game.isPlayerMoveDraggable) return;
            
            if (_entities.count == 0)
            {
                _game.playerMoveDraggableEntity.Destroy();
            }
        }
    }
}