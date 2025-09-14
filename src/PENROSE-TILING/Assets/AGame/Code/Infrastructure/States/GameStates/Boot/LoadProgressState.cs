using AGame.Code.Infrastructure.Progress.SaveLoad;
using AGame.Code.Infrastructure.States.GameStates.HomeScene;
using AGame.Code.Infrastructure.States.StateInfrastructure;
using AGame.Code.Infrastructure.States.StateMachine;

namespace AGame.Code.Infrastructure.States.GameStates.Boot
{
  public class LoadProgressState : SimpleState
  {
    private readonly IGameStateMachine _stateMachine;
    private readonly ISaveLoadService _saveLoadService;

    public LoadProgressState(
      IGameStateMachine stateMachine,
      ISaveLoadService saveLoadService)
    {
      _saveLoadService = saveLoadService;
      _stateMachine = stateMachine;
    }
    
    public override void Enter()
    {
      InitializeProgress();

      _stateMachine.Enter<LoadingHomeScreenState>();
    }

    private void InitializeProgress()
    {
      if (_saveLoadService.HasSavedProgress)
        _saveLoadService.LoadProgress();
      else
        _saveLoadService.CreateProgress();
    }
  }
}