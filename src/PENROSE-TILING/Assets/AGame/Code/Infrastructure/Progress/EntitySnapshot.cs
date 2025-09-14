using System.Collections.Generic;
using Newtonsoft.Json;

namespace AGame.Code.Infrastructure.Progress
{
  public class EntitySnapshot
  {
    [JsonProperty("c")] public List<ISavedComponent> Components;
  }
}