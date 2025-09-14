using System.Collections.Generic;
using Entitas;

namespace AGame.Code.Gameplay.Features.Input.Systems
{
    public class TouchEndedCleanupSystem : ICleanupSystem
    {
        private readonly IGroup<InputEntity> _touches;
        private int _layerMask;

        private readonly List<InputEntity> _buffer = new List<InputEntity>(10);

        public TouchEndedCleanupSystem(InputContext input)
        {
            _touches = input.GetGroup(InputMatcher.AllOf(InputMatcher.TouchId, InputMatcher.TouchEnded));
        }

        public void Cleanup()
        {
            foreach (InputEntity touch in _touches.GetEntities(_buffer))
            {
                touch.isTouchEnded = false;
                touch.Destroy();
            }
        }
    }
}