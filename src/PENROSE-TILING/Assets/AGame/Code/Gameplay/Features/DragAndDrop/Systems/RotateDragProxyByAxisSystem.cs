using AGame.Code.Gameplay.Services.TimeService;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.DragAndDrop.Systems
{
    public class RotateDragProxyByAxisSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _input;
        private readonly IGroup<GameEntity> _draggableProxy;
        private readonly ITimeService _time;

        private const float RotationSpeed = -180f;

        public RotateDragProxyByAxisSystem(InputContext input, GameContext game, ITimeService time)
        {
            _time = time;
            _input = input.GetGroup(InputMatcher.InputAxis);
            _draggableProxy = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragProxy, GameMatcher.Rotation));
        }

        public void Execute()
        {
            foreach (GameEntity proxy in _draggableProxy)
            foreach (InputEntity input in _input)
            {
                Quaternion deltaRotation = Quaternion.Euler(0, 0, input.InputAxis.x * RotationSpeed * _time.DeltaTime);
                Quaternion newRotation = proxy.Rotation * deltaRotation;
                
                proxy.ReplaceRotation(newRotation);
            }
        }
    }
}