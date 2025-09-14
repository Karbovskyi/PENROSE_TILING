using AGame.Code.Gameplay.StaticServices.DebugService;
using Entitas;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDebug.Systems
{
    public class DebugDrawRectSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _rects;

        public DebugDrawRectSystem(GameContext game)
        {
            _rects = game.GetGroup(GameMatcher.Rect);
        }

        public void Execute()
        {
            foreach (var entity in _rects)
            {
                DebugService.DrawDebugRect(entity.Rect);
            }
        }
    }
}