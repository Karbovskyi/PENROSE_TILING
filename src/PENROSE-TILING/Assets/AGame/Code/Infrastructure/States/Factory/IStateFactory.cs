using AGame.Code.Infrastructure.States.StateInfrastructure;

namespace AGame.Code.Infrastructure.States.Factory
{
  public interface IStateFactory
  {
    T GetState<T>() where T : class, IExitableState;
  }
}