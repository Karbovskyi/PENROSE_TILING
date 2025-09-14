using System.Collections.Generic;
using AGame.Code.Gameplay.Services.InputService;
using AGame.Code.Infrastructure.Ecs.Features.Entity;
using Entitas;

namespace AGame.Code.Gameplay.Features.Input.Systems
{
    public class InputAxisDetectSystem : IExecuteSystem
    {
        private readonly IInputService _inputService;
        private readonly List<InputEntity> _buffer = new (1);
        private readonly IGroup<InputEntity> _inputs;
        private readonly IGroup<InputEntity> _axis;

        public InputAxisDetectSystem(InputContext input, IInputService inputService)
        {
            _inputService = inputService;
            _inputs = input.GetGroup(InputMatcher.Input);
            _axis = input.GetGroup(InputMatcher.InputAxis);
        }
    
        public void Execute()
        {
            foreach (InputEntity input in _inputs)
            {
                if (_inputService.HasInputAxis())
                {
                    CreateOrUpdateInputAxisEntity();
                }
                else
                {
                    DestroyInputAxisEntity();
                }
            }
        }

        private void CreateOrUpdateInputAxisEntity()
        {
            if (_axis.count == 0)
            {
                CreateEntity.InputEmpty()
                    .AddInputAxis(_inputService.GetInputAxis());
            }
            else
            {
                foreach (InputEntity axis in _axis.GetEntities(_buffer))
                {
                    axis.ReplaceInputAxis(_inputService.GetInputAxis());
                }
            }
        }

        private void DestroyInputAxisEntity()
        {
            foreach (InputEntity axis in _axis.GetEntities(_buffer))
            {
                axis.Destroy();
            }
        }
    }
}