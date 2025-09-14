using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class SnapLinksCleanupSystem : ICleanupSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _snapLinks;


        public SnapLinksCleanupSystem(GameContext game)
        {
            _game = game;
            _snapLinks = game.GetGroup(GameMatcher.SnapLinks);
            
        }
        
        public void Cleanup()
        {
            foreach (GameEntity link in _snapLinks)
            {
                link.SnapLinks.Clear();
            }
        }
    }
}