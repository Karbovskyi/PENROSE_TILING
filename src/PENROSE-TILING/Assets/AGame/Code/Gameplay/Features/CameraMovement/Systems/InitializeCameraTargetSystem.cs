using AGame.Code.Extensions;
using AGame.Code.Gameplay.Services.CameraProvider;
using AGame.Code.Infrastructure.Ecs.Features.Entity;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class InitializeCameraTargetSystem : IInitializeSystem
    {
        private readonly ICameraProvider _cameraProvider;

        public InitializeCameraTargetSystem(ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
        }
        
        public void Initialize()
        {
            Transform target = _cameraProvider.MainCinemachineCamera.Follow;
            
            CreateEntity
                .GameEmpty()
                .With(x=>x.isCameraTarget = true)
                .AddTransform(target)
                .AddWorldPosition(target.position)
                .AddRotation(target.rotation)
                .AddCinemachineCamera(_cameraProvider.MainCinemachineCamera)
                .AddCamera(_cameraProvider.MainCamera);
        }
    }
}