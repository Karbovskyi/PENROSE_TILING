using AGame.Code.Infrastructure.Loading;
using AGame.Code.Infrastructure.States.StateInfrastructure;
using AGame.Code.Infrastructure.States.StateMachine;

namespace AGame.Code.Infrastructure.States.GameStates.HomeScene
{
  public class LoadingHomeScreenState : SimpleState
  {
    private const string HomeSceneName = "Home";
    private readonly IGameStateMachine _stateMachine;
    private readonly ISceneLoader _sceneLoader;

    public LoadingHomeScreenState(IGameStateMachine stateMachine, ISceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }
    
    public override void Enter()
    {
      _sceneLoader.LoadScene(HomeSceneName, EnterHomeScreenState);
    }

    private void EnterHomeScreenState()
    {
      _stateMachine.Enter<HomeScreenState>();
    }
  }
}