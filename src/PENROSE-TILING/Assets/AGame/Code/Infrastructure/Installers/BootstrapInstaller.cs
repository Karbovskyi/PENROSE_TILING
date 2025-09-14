using AGame.Code.Gameplay.EntityIndices;
using AGame.Code.Gameplay.Features.Tiles.Factory;
using AGame.Code.Gameplay.Services.CameraProvider;
using AGame.Code.Gameplay.Services.DraggableService;
using AGame.Code.Gameplay.Services.InputService;
using AGame.Code.Gameplay.Services.TimeService;
using AGame.Code.Infrastructure.AssetProvider;
using AGame.Code.Infrastructure.Ecs.Features.BindView.Factory;
using AGame.Code.Infrastructure.Ecs.Systems;
using AGame.Code.Infrastructure.Identifiers;
using AGame.Code.Infrastructure.Loading;
using AGame.Code.Infrastructure.Progress.Provider;
using AGame.Code.Infrastructure.Progress.SaveLoad;
using AGame.Code.Infrastructure.States.Factory;
using AGame.Code.Infrastructure.States.GameStates.Boot;
using AGame.Code.Infrastructure.States.GameStates.HomeScene;
using AGame.Code.Infrastructure.States.GameStates.TillingScene;
using AGame.Code.Infrastructure.States.StateMachine;
using RSG;
using UnityEngine;
using Zenject;

namespace AGame.Code.Infrastructure.Installers
{
  public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
  {
    public override void InstallBindings()
    {
      BindInputService();
      BindInfrastructureServices();
      BindAssetManagementServices();
      BindCommonServices();
      BindSystemFactory();
      BindContexts();
      BindCameraProvider();
      BindGameplayFactories();
      BindStateMachine();
      BindStateFactory();
      BindGameStates();
      BindProgressServices();
      BindEntityIndices();
    }

    private void BindStateMachine()
    {
      Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
    }

    private void BindStateFactory()
    {
      Container.BindInterfacesAndSelfTo<StateFactory>().AsSingle();
    }

    private void BindGameStates()
    {
      Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
      Container.BindInterfacesAndSelfTo<LoadProgressState>().AsSingle();
      
      Container.BindInterfacesAndSelfTo<LoadingHomeScreenState>().AsSingle();
      Container.BindInterfacesAndSelfTo<HomeScreenState>().AsSingle();
      
      Container.BindInterfacesAndSelfTo<LoadingTillingSceneState>().AsSingle();
      Container.BindInterfacesAndSelfTo<TillingSceneInitializeState>().AsSingle();
      Container.BindInterfacesAndSelfTo<TillingSceneLoopState>().AsSingle();
    }

    private void BindContexts()
    {
      Container.Bind<Contexts>().FromInstance(Contexts.sharedInstance).AsSingle();
      
      Container.Bind<GameContext>().FromInstance(Contexts.sharedInstance.game).AsSingle();
      Container.Bind<InputContext>().FromInstance(Contexts.sharedInstance.input).AsSingle();
      Container.Bind<MetaContext>().FromInstance(Contexts.sharedInstance.meta).AsSingle();
    }

    private void BindCameraProvider()
    {
      Container.BindInterfacesAndSelfTo<CameraProvider>().AsSingle();
    }

    private void BindProgressServices()
    {
      Container.Bind<IProgressProvider>().To<ProgressProvider>().AsSingle();
      Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
    }

    private void BindGameplayFactories()
    {
      Container.Bind<IEntityViewFactory>().To<EntityViewFactory>().AsSingle();
      Container.Bind<ITilesFactory>().To<TilesFactory>().AsSingle();
    }

    private void BindSystemFactory()
    {
      Container.Bind<ISystemFactory>().To<SystemFactory>().AsSingle();
    }

    private void BindInfrastructureServices()
    {
      Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this).AsSingle();
      Container.Bind<IIdentifierService>().To<IdentifierService>().AsSingle();
    }

    private void BindAssetManagementServices()
    {
      Container.Bind<IAssetProvider>().To<AssetProvider.AssetProvider>().AsSingle();
    }

    private void BindCommonServices()
    {
      Container.Bind<ITimeService>().To<UnityTimeService>().AsSingle();
      Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
      Container.Bind<IDraggableHitTestService>().To<DraggableHitTestService>().AsSingle();
    }

    private void BindInputService()
    {
      Container.Bind<MyInput>().FromInstance(new MyInput()).AsSingle();
      Container.Bind<IInputService>().To<InputService>().AsSingle();
    }
    
    private void BindEntityIndices()
    {
      Container.BindInterfacesAndSelfTo<GameEntityIndices>().AsSingle();
    }
    
    public void Initialize()
    {
      Promise.UnhandledException += LogPromiseException;
      Container.Resolve<IGameStateMachine>().Enter<BootstrapState>();
    }

    private void LogPromiseException(object sender, ExceptionEventArgs e)
    {
      Debug.LogError(e.Exception);
    }
  }
}