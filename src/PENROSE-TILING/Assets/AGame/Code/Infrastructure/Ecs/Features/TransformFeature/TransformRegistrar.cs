using AGame.Code.Infrastructure.Ecs.Features.BindView.Registrars;

namespace AGame.Code.Infrastructure.Ecs.Features.TransformUpdators
{
  public class TransformRegistrar : EntityComponentRegistrar
  {
    public override void RegisterComponents()
    {
      Entity.AddTransform(transform);
    }

    public override void UnregisterComponents()
    {
      if (Entity.hasTransform)
        Entity.RemoveTransform();
    }
  }
}