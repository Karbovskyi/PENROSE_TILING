using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace AGame.Code.Gameplay.Features
{ 
    [Unique, Game] public class PlayerMoveCamera : IComponent { } 
    [Unique, Game] public class PlayerMoveDraggable : IComponent { }
}