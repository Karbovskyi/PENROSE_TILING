using Unity.Cinemachine;
using UnityEngine;

namespace AGame.Code.Gameplay.Services.CameraProvider
{
  public class CameraProvider : ICameraProvider
  {
    public CinemachineCamera MainCinemachineCamera { get; private set; }
    public Camera MainCamera { get; private set; }

    public float WorldScreenHeight { get; private set; }
    public float WorldScreenWidth { get; private set; }
    
    
    public void SetMainCamera(Camera camera)
    {
      MainCamera = camera;

      RefreshBoundaries();
    }

    public void SetMainCinemachineCamera(CinemachineCamera camera)
    {
      MainCinemachineCamera = camera;
    }

    private void RefreshBoundaries()
    {
      Vector2 bottomLeft = MainCamera.ViewportToWorldPoint(new Vector3(0, 0, MainCamera.nearClipPlane));
      Vector2 topRight = MainCamera.ViewportToWorldPoint(new Vector3(1, 1, MainCamera.nearClipPlane));
      WorldScreenWidth = topRight.x - bottomLeft.x;
      WorldScreenHeight = topRight.y - bottomLeft.y;
    }
  }
}