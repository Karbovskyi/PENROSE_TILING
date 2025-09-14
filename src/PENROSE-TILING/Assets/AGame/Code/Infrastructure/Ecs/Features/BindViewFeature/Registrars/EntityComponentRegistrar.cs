namespace AGame.Code.Infrastructure.Ecs.Features.BindView.Registrars
{
  public abstract class EntityComponentRegistrar : EntityDependant, IEntityComponentRegistrar
  {
    public abstract void RegisterComponents();
    public abstract void UnregisterComponents();
  }
}