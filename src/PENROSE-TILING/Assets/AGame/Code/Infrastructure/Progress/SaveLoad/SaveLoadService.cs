using System.Collections.Generic;
using System.Linq;
using AGame.Code.Gameplay.Services.TimeService;
using AGame.Code.Infrastructure.Progress.Data;
using AGame.Code.Infrastructure.Progress.Provider;
using AGame.Code.Infrastructure.Serialization;
using UnityEngine;

namespace AGame.Code.Infrastructure.Progress.SaveLoad
{
  public class SaveLoadService : ISaveLoadService
  {
    private const string ProgressKey = "PlayerProgress";

    private readonly MetaContext _metaContext;
    private readonly IProgressProvider _progressProvider;
    private readonly ITimeService _timeService;

    public bool HasSavedProgress => PlayerPrefs.HasKey(ProgressKey);

    public SaveLoadService(MetaContext metaContext, IProgressProvider progressProvider, ITimeService timeService)
    {
      _timeService = timeService;
      _metaContext = metaContext;
      _progressProvider = progressProvider;
    }

    public void CreateProgress()
    {
      _progressProvider.SetProgressData(new ProgressData()
      {
        LastSimulationTickTime = _timeService.UtcNow
      });
    }

    public void SaveProgress()
    {
      PreserveMetaEntities();
      PlayerPrefs.SetString(ProgressKey, _progressProvider.ProgressData.ToJson());
      PlayerPrefs.Save();
    }

    public void LoadProgress()
    {
      HydrateProgress(PlayerPrefs.GetString(ProgressKey));
    }

    private void HydrateProgress(string serializedProgress)
    {
      _progressProvider.SetProgressData(serializedProgress.FromJson<ProgressData>());
      HydrateMetaEntities();
    }

    private void HydrateMetaEntities()
    {
      List<EntitySnapshot> snapshots = _progressProvider.EntityData.MetaEntitySnapshots;
      foreach (EntitySnapshot snapshot in snapshots)
      {
        _metaContext
          .CreateEntity()
          .HydrateWith(snapshot);
      }
    }

    private void PreserveMetaEntities()
    {
      _progressProvider.EntityData.MetaEntitySnapshots = _metaContext
        .GetEntities()
        .Where(RequiresSave)
        .Select(e => e.AsSavedEntity())
        .ToList();
    }

    private static bool RequiresSave(MetaEntity e)
    {
      return e.GetComponents().Any(c => c is ISavedComponent);
    }
  }
}