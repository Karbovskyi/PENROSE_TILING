using AGame.Code.Gameplay.Features;
using AGame.Code.Infrastructure.Ecs.Systems;
using AGame.Code.Infrastructure.States.StateInfrastructure;

namespace AGame.Code.Infrastructure.States.GameStates.TillingScene
{
  public class TillingSceneLoopState : EndOfFrameExitState
  {
    private readonly ISystemFactory _systems;
    private readonly InputContext _inputContext;
    private readonly GameContext _gameContext;
    private TillingSceneFeature _tillingSceneFeature;

    public TillingSceneLoopState(ISystemFactory systems, InputContext inputContext, GameContext gameContext)
    {
      _systems = systems;
      _inputContext = inputContext;
      _gameContext = gameContext;
    }
    
    public override void Enter()
    {
      _tillingSceneFeature = _systems.Create<TillingSceneFeature>();
      _tillingSceneFeature.Initialize();
    }

    protected override void OnUpdate()
    {
      _tillingSceneFeature.Execute();
      _tillingSceneFeature.Cleanup();
    }

    protected override void ExitOnEndOfFrame()
    {
      _tillingSceneFeature.DeactivateReactiveSystems();
      _tillingSceneFeature.ClearReactiveSystems();

      DestructEntities();
      
      _tillingSceneFeature.Cleanup();
      _tillingSceneFeature.TearDown();
      _tillingSceneFeature = null;
    }

    private void DestructEntities()
    {
      foreach (InputEntity entity in _inputContext.GetEntities()) 
        entity.Destroy();
      
      foreach (GameEntity entity in _gameContext.GetEntities()) 
        entity.isDestructed = true;
    }
  }
}