using RSG;

namespace AGame.Code.Infrastructure.States.StateInfrastructure
{
  public interface IExitableState
  {
    IPromise BeginExit();
    void EndExit();
  }
}