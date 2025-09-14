using Entitas;

namespace AGame.Code.Infrastructure.Ecs.Systems
{
  public interface ISystemFactory
  {
    T Create<T>() where T : ISystem;
    T Create<T>(params object[] args) where T : ISystem;
  }
}