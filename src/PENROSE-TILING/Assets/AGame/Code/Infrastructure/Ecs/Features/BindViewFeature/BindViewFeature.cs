using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Infrastructure.Ecs.Features.BindView
{
  public sealed class BindViewFeature : Feature
  {
    public BindViewFeature(ISystemFactory systems)
    {
      Add(systems.Create<Systems.BindEntityViewFromPathSystem>());
      Add(systems.Create<Systems.BindEntityViewFromPrefabSystem>());
    }
  }
}