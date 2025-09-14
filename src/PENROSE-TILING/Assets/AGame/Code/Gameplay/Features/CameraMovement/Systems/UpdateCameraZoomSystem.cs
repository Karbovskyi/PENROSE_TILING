using System.Collections.Generic;
using Entitas;
using Unity.Cinemachine;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class UpdateCameraZoomSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _cameraTargets;
        private readonly List<GameEntity> _buffer = new(1);
        
        private float _minOrthographicSize = 1f;
        private float _maxOrthographicSize = 100;

        public UpdateCameraZoomSystem(GameContext game)
        {
            _cameraTargets = game.GetGroup(GameMatcher.AllOf(GameMatcher.Camera, GameMatcher.CinemachineCamera, GameMatcher.ZoomOffset));
        }

        public void Execute()
        {
            foreach (GameEntity target in _cameraTargets.GetEntities(_buffer))
            {
                float zoomOffset = target.ZoomOffset;
                CinemachineCamera cinemachineCamera = target.CinemachineCamera;

                float newOrthographicSize = cinemachineCamera.Lens.OrthographicSize - zoomOffset;
                cinemachineCamera.Lens.OrthographicSize = Mathf.Clamp(newOrthographicSize, _minOrthographicSize, _maxOrthographicSize);

                target.RemoveZoomOffset();
            }
        }
    }
}