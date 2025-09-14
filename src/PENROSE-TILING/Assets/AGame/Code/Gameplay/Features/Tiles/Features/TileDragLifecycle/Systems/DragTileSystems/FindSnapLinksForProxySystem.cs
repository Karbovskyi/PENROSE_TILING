using AGame.Code.Gameplay.Features.Tiles.Data;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class FindSnapLinksForProxySystem : IExecuteSystem
    {
        private const float SqrSnapDistance = 0.04f;
        private readonly IGroup<GameEntity> _draggableProxy;
        private readonly IGroup<GameEntity> _staticVerteces;

        public FindSnapLinksForProxySystem(GameContext game)
        {
            _draggableProxy = game.GetGroup(GameMatcher.AllOf(GameMatcher.DragProxy, GameMatcher.SnapLinks));
            
            _staticVerteces = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Vertex, GameMatcher.WorldPosition)
                .NoneOf(GameMatcher.Dragging));
        }
        
        public void Execute()
        {
            foreach (GameEntity proxy in _draggableProxy)
            {
                for (var i = 0; i < proxy.TileStaticData.Vertices.Length; i++)
                {
                    Vector3 proxyVertexPosition = proxy.TileStaticData.LocalVertexToWorld(i, proxy.WorldPosition, proxy.Rotation, Vector3.one);
                    
                    foreach (GameEntity vertex in _staticVerteces)
                    {
                        float sqrDistance = (vertex.WorldPosition - proxyVertexPosition).sqrMagnitude;

                        if (sqrDistance <= SqrSnapDistance)
                        {
                            proxy.SnapLinks.Add(new SnapLinkData(-1, i, vertex.Id));
                        }
                    }
                }
            }
        }
    }
}