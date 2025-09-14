using AGame.Code.Gameplay.Services.InputService;
using AGame.Code.Infrastructure.Ecs.Features.Entity;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Input.Systems
{
    public class MouseDetectSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly InputContext _input;

        public MouseDetectSystem(InputContext input, IInputService inputService)
        {
            _inputService = inputService;
            _input = input;
        }

        public void Execute()
        {
            if (_inputService.IsAnyMouseButtonPressed(out Vector2 position))
            {
                var entity = _input.GetEntityWithTouchId(-1);

                if (entity == null)
                {
                    CreateEntity.InputEmpty()
                        .AddTouchId(-1)
                        .AddTouchPosition(position)
                        .AddPreviousTouchPosition(position)
                        .isTouchStarted = true;
                }
                else
                {
                    entity.ReplacePreviousTouchPosition(entity.TouchPosition);
                    entity.ReplaceTouchPosition(position);
                }
            }
            else
            {
                var entity = _input.GetEntityWithTouchId(-1);
                if (entity != null)
                {
                    entity.isTouchEnded = true;
                }
            }
        }
    }
}