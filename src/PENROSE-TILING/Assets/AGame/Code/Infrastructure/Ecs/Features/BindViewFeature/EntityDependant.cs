using UnityEngine;

namespace AGame.Code.Infrastructure.Ecs.Features.BindView
{
  public abstract class EntityDependant : MonoBehaviour
  {
    public EntityBehaviour EntityView;

    public GameEntity Entity => EntityView != null ? EntityView.Entity : null;

    private void Awake()
    {
      if (!EntityView)
        EntityView = GetComponent<EntityBehaviour>();
    }
  }
}