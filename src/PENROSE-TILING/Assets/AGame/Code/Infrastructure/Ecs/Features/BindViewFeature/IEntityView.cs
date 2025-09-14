using UnityEngine;

namespace AGame.Code.Infrastructure.Ecs.Features.BindView
{
  public interface IEntityView
  {
    GameEntity Entity { get; }
    void SetEntity(GameEntity entity);
    void ReleaseEntity();
    
    GameObject gameObject { get; }
  }
}