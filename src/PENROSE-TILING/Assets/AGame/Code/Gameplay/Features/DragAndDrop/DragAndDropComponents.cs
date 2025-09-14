using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.DragAndDrop
{
    [Game] public class DragProxy : IComponent { }
    [Game] public class DragProxyTargetId : IComponent { public int Value; }
    [Game] public class DragStarted : IComponent { }
    [Game] public class DragEnded : IComponent { }
    [Game] public class DragOffset : IComponent { public Vector3 Value; }
    [Game] public class DragTouchId : IComponent { public int Value; }
    [Game] public class RotateTouchId : IComponent { public int Value; }
    
    [Game] public class Draggable : IComponent { }
    [Game] public class Dragging : IComponent { }
}