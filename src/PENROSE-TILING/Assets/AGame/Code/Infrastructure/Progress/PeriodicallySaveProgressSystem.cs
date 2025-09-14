using AGame.Code.Gameplay.Services.TimeService;
using AGame.Code.Infrastructure.Ecs.Systems;
using AGame.Code.Infrastructure.Progress.SaveLoad;

namespace AGame.Code.Infrastructure.Progress
{
  public class PeriodicallySaveProgressSystem : TimerExecuteSystem
  {
    private readonly ISaveLoadService _saveLoadService;

    public PeriodicallySaveProgressSystem(float executeIntervalSeconds, ITimeService time,
      ISaveLoadService saveLoadService) : base(executeIntervalSeconds, time)
    {
      _saveLoadService = saveLoadService;
    }

    protected override void Execute()
    {
      _saveLoadService.SaveProgress();
    }
  }
}