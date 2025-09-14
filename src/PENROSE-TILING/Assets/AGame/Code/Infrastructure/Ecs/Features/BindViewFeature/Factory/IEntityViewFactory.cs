namespace AGame.Code.Infrastructure.Ecs.Features.BindView.Factory
{
  public interface IEntityViewFactory
  {
    EntityBehaviour CreateViewForEntity(GameEntity entity);
    EntityBehaviour CreateViewForEntityFromPrefab(GameEntity entity);
  }
}