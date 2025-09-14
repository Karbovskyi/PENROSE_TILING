using Entitas;
using Entitas.CodeGeneration.Attributes;
using Unity.Cinemachine;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.CameraMovement
{
    [Game] public class CameraControlTouches : IComponent { public int First; public int Second; }
    [Game] public class CameraTarget : IComponent { }
    [Game] public class CameraComponent : IComponent { public Camera Value; }
    [Game] public class CinemachineCameraComponent : IComponent { public CinemachineCamera Value; }
    [Game] public class ZoomOffset : IComponent { public float Value; }
}