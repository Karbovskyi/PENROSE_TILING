using System.Collections.Generic;
using AGame.Code.Gameplay.Features.Tiles;
using Entitas;
using Zenject;

namespace AGame.Code.Gameplay.EntityIndices
{
    public class GameEntityIndices : IInitializable
    {
        public const string TilesOfType = "TilesOfType";
        
        private readonly GameContext _game;

        public GameEntityIndices(GameContext game)
        {
            _game = game;
        }
        public void Initialize()
        {
            _game.AddEntityIndex(new EntityIndex<GameEntity, int>(
                name: TilesOfType,
                _game.GetGroup(GameMatcher.AllOf(
                    GameMatcher.TileTypeId, 
                    GameMatcher.Tile)
                    .NoneOf(GameMatcher.Dragging)),
                getKey: GetKey
                ));
        }

        private int GetKey(GameEntity entity, IComponent component)
        {
            return (component as TileTypeId)?.Value ?? entity.TileTypeId;
        }
    }

    public static class ContextIndicesExtensions
    {
        public static HashSet<GameEntity> StaticTilesOfType(this GameContext context, int tileTypeId)
        {
            return ((EntityIndex<GameEntity, int>)context.GetEntityIndex(GameEntityIndices.TilesOfType))
                .GetEntities(tileTypeId);
        }
    }
    
}