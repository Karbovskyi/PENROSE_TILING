using AGame.Code.Infrastructure.Progress.Data;

namespace AGame.Code.Infrastructure.Progress.Provider
{
  public interface IProgressProvider
  {
    ProgressData ProgressData { get; }
    EntityData EntityData { get; }
    void SetProgressData(ProgressData data);
  }
}