using Entitas;

namespace AGame.Code.Infrastructure.Ecs.Features.Entity.ToStrings
{
  public interface INamedEntity : IEntity
  {
    string EntityName(IComponent[] components);
    string BaseToString();
  }
}