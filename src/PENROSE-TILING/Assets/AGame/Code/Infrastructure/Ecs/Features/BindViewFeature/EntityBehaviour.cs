using UnityEngine;

namespace AGame.Code.Infrastructure.Ecs.Features.BindView
{
  public class EntityBehaviour : MonoBehaviour, IEntityView
  {
    private GameEntity _entity;
    public GameEntity Entity => _entity;

    public void SetEntity(GameEntity entity)
    {
      _entity = entity;
      _entity.AddView(this);
      _entity.Retain(this);

      foreach (Registrars.IEntityComponentRegistrar registrar in GetComponentsInChildren<Registrars.IEntityComponentRegistrar>()) 
        registrar.RegisterComponents();
    }

    public void ReleaseEntity()
    {
      foreach (Registrars.IEntityComponentRegistrar registrar in GetComponentsInChildren<Registrars.IEntityComponentRegistrar>()) 
        registrar.UnregisterComponents();
      
      _entity.Release(this);
      _entity = null;
    }
  }
}