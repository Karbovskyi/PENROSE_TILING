using AGame.Code.Infrastructure.Ecs.Features.Entity;
using Entitas;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;


namespace AGame.Code.Gameplay.Features.Input.Systems
{
  public class TouchDetectSystem : IExecuteSystem, IInitializeSystem
  {
    private readonly InputContext _input;
    private readonly IGroup<InputEntity> _inputs;

    public TouchDetectSystem(InputContext input)
    {
      _input = input;
      _inputs = input.GetGroup(InputMatcher.Input);
    }

    public void Initialize()
    {
      EnhancedTouchSupport.Enable();
    }

    public void Execute()
    {
      foreach (InputEntity input in _inputs)
      { 
        foreach (var touch in Touch.activeTouches)
        {
          switch (touch.phase)
          {
            case TouchPhase.Began:
              CreateTouchEntity(touch);
              break;
            case TouchPhase.Ended:
              DestroyTouchEntity(touch);
              break;
            case TouchPhase.Moved:
              UpdateTouchEntity(touch);
              break;
            case TouchPhase.Canceled:
              DestroyTouchEntity(touch);
              break;
            case TouchPhase.Stationary:
              UpdateTouchEntity(touch);
              break;
          }
        }
      }
    }

    private void CreateTouchEntity(Touch touch)
    {
      // Sometimes duplicate touch events are received. 
      InputEntity entity = _input.GetEntityWithTouchId(touch.touchId);
      if(entity != null) return;
      
      CreateEntity.InputEmpty()
        .AddTouchId(touch.touchId)
        .AddTouchPosition(touch.screenPosition)
        .AddPreviousTouchPosition(touch.screenPosition)
        .isTouchStarted = true;
    }

    private void UpdateTouchEntity(Touch touch)
    {
      InputEntity entity = _input.GetEntityWithTouchId(touch.touchId);
      if(entity == null) return;
      
      entity.ReplacePreviousTouchPosition(entity.TouchPosition);
      entity.ReplaceTouchPosition(touch.screenPosition);
    }
    
    private void DestroyTouchEntity(Touch touch)
    {
      InputEntity entity = _input.GetEntityWithTouchId(touch.touchId);
      if(entity == null) return;
      
      entity.isTouchEnded = true;
    }
  }
}