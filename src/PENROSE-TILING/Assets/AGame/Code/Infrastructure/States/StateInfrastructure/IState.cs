namespace AGame.Code.Infrastructure.States.StateInfrastructure
{
  public interface IState: IExitableState
  {
    void Enter();
  }
}