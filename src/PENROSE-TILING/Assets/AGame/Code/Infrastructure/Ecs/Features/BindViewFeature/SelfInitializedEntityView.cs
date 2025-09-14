using AGame.Code.Infrastructure.Ecs.Features.Entity;
using AGame.Code.Infrastructure.Identifiers;
using UnityEngine;
using Zenject;

namespace AGame.Code.Infrastructure.Ecs.Features.BindView
{
  public class SelfInitializedEntityView : MonoBehaviour
  {
    public EntityBehaviour EntityBehaviour;
    
    private IIdentifierService _identifiers;

    [Inject]
    private void Construct(IIdentifierService identifiers) => 
      _identifiers = identifiers;

    private void Awake()
    {
      GameEntity entity = CreateEntity.GameEmpty()
        .AddId(_identifiers.Next());
      
      EntityBehaviour.SetEntity(entity);
    }

  }
}