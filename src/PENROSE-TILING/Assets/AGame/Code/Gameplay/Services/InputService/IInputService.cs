using UnityEngine;

namespace AGame.Code.Gameplay.Services.InputService
{
  public interface IInputService
  {
    bool HasInputAxis();
    Vector2 GetInputAxis();
    
    bool HasScrollWheel();
    float GetScrollWheelValue();

    bool IsAnyMouseButtonPressed(out Vector2 position);
  }
}