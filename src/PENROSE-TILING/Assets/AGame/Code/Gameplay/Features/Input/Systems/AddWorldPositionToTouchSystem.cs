using AGame.Code.Gameplay.Services.CameraProvider;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Input.Systems
{
    public class AddWorldPositionToTouchSystem : IExecuteSystem
    {
        private readonly ICameraProvider _cameraProvider;
        private readonly IGroup<InputEntity> _touch;
        private int _layerMask;

        public AddWorldPositionToTouchSystem(InputContext input, GameContext game, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;
            _touch = input.GetGroup(InputMatcher.TouchId);
        }

        public void Execute()
        {
            foreach (InputEntity touch in _touch)
            {
                Vector2 screenPosition = touch.TouchPosition;
                Vector3 worldPosition = _cameraProvider.MainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, -_cameraProvider.MainCamera.transform.position.z));
                touch.ReplaceWorldPosition(worldPosition);
            }
        }
    }
}