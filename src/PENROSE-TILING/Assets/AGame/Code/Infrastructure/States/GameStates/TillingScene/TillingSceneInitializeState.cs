using AGame.Code.Infrastructure.Ecs.Systems;
using AGame.Code.Infrastructure.States.StateInfrastructure;
using AGame.Code.Infrastructure.States.StateMachine;

namespace AGame.Code.Infrastructure.States.GameStates.TillingScene
{
  public class TillingSceneInitializeState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;

    private readonly ISystemFactory _systems;
    private readonly GameContext _gameContext;


    public TillingSceneInitializeState(
      IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public override void Enter()
    {
      _stateMachine.Enter<TillingSceneLoopState>();
    }
  }
}