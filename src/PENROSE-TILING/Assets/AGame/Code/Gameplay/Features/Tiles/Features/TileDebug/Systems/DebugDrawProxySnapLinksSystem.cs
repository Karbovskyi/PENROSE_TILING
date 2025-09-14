using AGame.Code.Gameplay.Features.Tiles.Data;
using AGame.Code.Gameplay.StaticServices.DebugService;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDebug.Systems
{
    public class DebugDrawProxySnapLinksSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _proxy;

        public DebugDrawProxySnapLinksSystem(GameContext game)
        {
            _game = game;
            _proxy = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragProxy, GameMatcher.SnapLinks));
        }

        public void Execute()
        {
            foreach (var proxy in _proxy)
            {
                foreach (SnapLinkData link in proxy.SnapLinks)
                {
                    Vector3 proxyVertex = proxy.TileStaticData.LocalVertexToWorld(link.SourceVertexIndex, proxy.WorldPosition, proxy.Rotation, Vector3.one);
                    Vector3 targetVertex = _game.GetEntityWithId(link.TargetVertexId).WorldPosition;
                    Debug.DrawLine(proxyVertex, targetVertex, Color.green);
                    DebugService.DrawDebugCircle(proxyVertex, 0.2f, 16, Color.green);
                }
            }
        }
    }
}