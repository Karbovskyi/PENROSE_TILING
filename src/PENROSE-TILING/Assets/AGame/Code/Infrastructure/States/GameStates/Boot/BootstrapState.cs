using AGame.Code.Infrastructure.States.StateInfrastructure;
using AGame.Code.Infrastructure.States.StateMachine;

namespace AGame.Code.Infrastructure.States.GameStates.Boot
{
  public class BootstrapState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;

    public BootstrapState(IGameStateMachine stateMachine)
    {
      _stateMachine = stateMachine;
    }
    
    public override void Enter()
    { 
      _stateMachine.Enter<LoadProgressState>();
    }
  }
}