using AGame.Code.Gameplay.Features.CameraMovement.Systems;
using AGame.Code.Infrastructure.Ecs.Systems;

namespace AGame.Code.Gameplay.Features.CameraMovement
{
    public class CameraMovementFeature : Feature
    {
        public CameraMovementFeature(ISystemFactory systems)
        {
            Add(systems.Create<InitializeCameraTargetSystem>());
            Add(systems.Create<RegisterCameraControlSystem>());
            Add(systems.Create<RegisterCameraControlSystem2>());
            Add(systems.Create<UnregisterCameraControlSystem>());
            Add(systems.Create<CameraMoveSystem>());
            Add(systems.Create<RotateCameraByTouchesSystem>());
            Add(systems.Create<RotateCameraByAxisSystem>());
            Add(systems.Create<CalculateCameraZoomByTouchesSystem>());
            Add(systems.Create<CalculateCameraZoomByAxisSystem>());
            Add(systems.Create<CalculateCameraZoomByScrollWheelSystem>());
            Add(systems.Create<UpdateCameraZoomSystem>());
        }
    }
}