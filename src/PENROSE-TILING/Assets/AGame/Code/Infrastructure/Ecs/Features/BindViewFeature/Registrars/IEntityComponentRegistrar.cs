namespace AGame.Code.Infrastructure.Ecs.Features.BindView.Registrars
{
  public interface IEntityComponentRegistrar
  {
    void RegisterComponents();
    void UnregisterComponents();
  }
}