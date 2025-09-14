using JetBrains.Annotations;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace AGame.Code.Gameplay.Services.CameraProvider
{
    [UsedImplicitly]
    public class CameraInstaller : MonoInstaller
    {
        [SerializeField] private Camera _sceneCamera;
        [SerializeField] private CinemachineCamera _sceneCinemachineCamera;

        public override void InstallBindings()
        {
            ICameraProvider cameraProvider = Container.Resolve<ICameraProvider>();
            cameraProvider.SetMainCamera(_sceneCamera);
            cameraProvider.SetMainCinemachineCamera(_sceneCinemachineCamera);
        }
    }
}