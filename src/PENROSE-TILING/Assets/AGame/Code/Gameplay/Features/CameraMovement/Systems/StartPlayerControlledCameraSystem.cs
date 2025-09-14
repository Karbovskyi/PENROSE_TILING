using System.Collections.Generic;
using Entitas;

namespace AGame.Code.Gameplay.Features.CameraMovement.Systems
{
    public class StartPlayerControlledCameraSystem : ReactiveSystem<InputEntity>
    {
        public StartPlayerControlledCameraSystem(InputContext input) : base(input)
        {
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) =>
            context.CreateCollector(InputMatcher
                .AllOf(
                    InputMatcher.WorldPosition, InputMatcher.WorldPosition)
                .Added());

        protected override bool Filter(InputEntity entity) => true;//entity.isArmament && entity.hasEnchantVisuals;

        protected override void Execute(List<InputEntity> armaments)
        {
            foreach (InputEntity armament in armaments)
            {
                //armament.EnchantVisuals.ApplyPoison();
            }
        }
    }
}