using AGame.Code.Infrastructure.Ecs.Features.BindView;
using Entitas;
using Entitas.CodeGeneration.Attributes;
using UnityEngine;

namespace AGame.Code.Infrastructure.Ecs.Features
{
  [Game] public class Id : IComponent { [PrimaryEntityIndex] public int Value; }
  
  [Game] public class View : IComponent { public IEntityView Value; }
  [Game] public class ViewPath : IComponent { public string Value; }
  [Game] public class ViewPrefab : IComponent { public EntityBehaviour Value; }
  
  [Input, Game] public class Processed : IComponent { }
  [Game] public class Destructed : IComponent { }
  [Game] public class SelfDestructTimer : IComponent { public float Value; }
  
  [Game] public class TransformComponent : IComponent { public Transform Value; }
  [Game] public class RectComponent : IComponent { public Rect Value; }
  
  [Game, Input] public class WorldPosition : IComponent { public Vector3 Value; }
  [Game] public class Rotation : IComponent { public Quaternion Value; }
  [Game] public class Scale : IComponent { public Vector3 Value; }
  [Game] public class Direction : IComponent { public Vector3 Value; }
  [Game] public class RotatingDirection : IComponent { public Vector3 Value; }
}