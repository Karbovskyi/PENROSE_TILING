using UnityEngine;
using UnityEngine.InputSystem;

namespace AGame.Code.Gameplay.Services.InputService
{
  public class InputService : IInputService
  {
    private MyInput _myInput;
    
    private bool _hasInputAxis;
    private Vector2 _inputAxis;
    
    private bool _hasScrollWheel;
    private float _scrollWheelValue;
    
    public bool HasInputAxis() => _hasInputAxis;
    public Vector2 GetInputAxis() => _inputAxis;
    
    public bool HasScrollWheel() => _hasScrollWheel;
    public float GetScrollWheelValue() => _scrollWheelValue;

    public InputService(MyInput myInput)
    {
      _myInput = myInput;
      _myInput.Input.Enable();
      
      _myInput.Input.InputAxis.started += InputAxisStarted;
      _myInput.Input.InputAxis.performed += InputAxisPerformed;
      _myInput.Input.InputAxis.canceled += InputAxisCanceled;
      
      _myInput.Input.ScrollWheel.started += OnScrollWheel1;
      _myInput.Input.ScrollWheel.performed += OnScrollWheel2;
      _myInput.Input.ScrollWheel.canceled += OnScrollWheel3;
    }
    
    public bool IsAnyMouseButtonPressed(out Vector2 position)
    {
      var mouse = Mouse.current;
      if (mouse == null)
      {
        position = default;
        return false;
      }

      position = mouse.position.ReadValue();

      return mouse.leftButton.isPressed || mouse.rightButton.isPressed || mouse.middleButton.isPressed;
    }
    
    private void InputAxisStarted(InputAction.CallbackContext obj)
    {
      _hasInputAxis = true;
      _inputAxis = _myInput.Input.InputAxis.ReadValue<Vector2>();
    }
    
    private void InputAxisPerformed(InputAction.CallbackContext obj)
    {
      _inputAxis = _myInput.Input.InputAxis.ReadValue<Vector2>();
    }
    
    private void InputAxisCanceled(InputAction.CallbackContext obj)
    {
      _hasInputAxis = false;
    }
    
    private void OnScrollWheel1(InputAction.CallbackContext obj)
    {
      _hasScrollWheel = true;
      _scrollWheelValue = obj.ReadValue<Vector2>().y;
    }
    private void OnScrollWheel2(InputAction.CallbackContext obj)
    {
      _scrollWheelValue = obj.ReadValue<Vector2>().y;
    }
    private void OnScrollWheel3(InputAction.CallbackContext obj)
    {
      _hasScrollWheel = false;
      _scrollWheelValue = 0;
    }
  }
}