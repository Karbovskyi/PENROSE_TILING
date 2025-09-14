using System.Collections.Generic;
using Entitas;

namespace AGame.Code.Infrastructure.Ecs.Features.BindView.Systems
{
  public class BindEntityViewFromPathSystem : IExecuteSystem
  {
    private readonly Factory.IEntityViewFactory _entityViewFactory;
    private readonly IGroup<GameEntity> _entities;
    private readonly List<GameEntity> _buffer = new(32);

    public BindEntityViewFromPathSystem(GameContext game, Factory.IEntityViewFactory entityViewFactory)
    {
      _entityViewFactory = entityViewFactory;
      _entities = game.GetGroup(GameMatcher
        .AllOf(GameMatcher.ViewPath)
        .NoneOf(GameMatcher.View));
    }

    public void Execute()
    {
      foreach (GameEntity entity in _entities.GetEntities(_buffer))
      {
        _entityViewFactory.CreateViewForEntity(entity);
      }
    }
  }
}