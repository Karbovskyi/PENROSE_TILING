using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Input
{
  [Input] public class Input : IComponent { }
  [Input] public class InputAxis : IComponent { public Vector2 Value; }
  
  [Input, Game] public class TouchId : IComponent { [PrimaryEntityIndex] public int Value; }
  [Input] public class TouchStarted : IComponent { }
  [Input] public class TouchEnded : IComponent { }
  [Input] public class TouchPosition : IComponent { public Vector2 Value; }
  [Input] public class PreviousTouchPosition : IComponent { public Vector2 Value; }
  
  
}