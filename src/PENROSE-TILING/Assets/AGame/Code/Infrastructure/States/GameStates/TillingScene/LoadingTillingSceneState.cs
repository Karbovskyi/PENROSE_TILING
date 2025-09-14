using AGame.Code.Infrastructure.Loading;
using AGame.Code.Infrastructure.States.StateInfrastructure;
using AGame.Code.Infrastructure.States.StateMachine;

namespace AGame.Code.Infrastructure.States.GameStates.TillingScene
{
  public class LoadingTillingSceneState : SimpleState
  {
    private const string TillingSceneName = "Tilling";
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;

    public LoadingTillingSceneState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }
    
    public override void Enter()
    {
      _sceneLoader.LoadScene(TillingSceneName, EnterTillingSceneState);
    }

    private void EnterTillingSceneState()
    {
      _stateMachine.Enter<TillingSceneInitializeState>();
    }
  }
}