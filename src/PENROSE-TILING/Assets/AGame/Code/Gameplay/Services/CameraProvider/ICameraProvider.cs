using Unity.Cinemachine;
using UnityEngine;

namespace AGame.Code.Gameplay.Services.CameraProvider
{
  public interface ICameraProvider
  {
    CinemachineCamera MainCinemachineCamera { get; }
    Camera MainCamera { get; }
    float WorldScreenHeight { get; }
    float WorldScreenWidth { get; }
    void SetMainCamera(Camera camera);
    void SetMainCinemachineCamera(CinemachineCamera camera);
    
  }
}