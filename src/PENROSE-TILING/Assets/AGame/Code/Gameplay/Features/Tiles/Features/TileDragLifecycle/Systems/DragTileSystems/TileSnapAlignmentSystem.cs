using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class TileSnapAlignmentSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _dragProxy;

        public TileSnapAlignmentSystem(GameContext game)
        {
            _game = game;
            _dragProxy = game.GetGroup(GameMatcher.DragProxy);
        }
        
        public void Execute()
        {
            foreach (GameEntity proxy in _dragProxy)
            {
                if (proxy.SnapLinks.Count < 2) continue;
                
                Vector2[] verticesLocal = proxy.TileStaticData.Vertices;
                Vector2 localA = verticesLocal[proxy.SnapLinks[0].SourceVertexIndex];
                Vector2 localB = verticesLocal[proxy.SnapLinks[1].SourceVertexIndex];
                Vector3 worldA = _game.GetEntityWithId(proxy.SnapLinks[0].TargetVertexId).WorldPosition;
                Vector3 worldB = _game.GetEntityWithId(proxy.SnapLinks[1].TargetVertexId).WorldPosition;

                (Vector3, Quaternion) positionAndRotation = GetPositionAndRotation(localA, localB, worldA, worldB);

                GameEntity tile = _game.GetEntityWithId(proxy.DragProxyTargetId);
                tile.ReplaceWorldPosition(positionAndRotation.Item1);
                tile.ReplaceRotation(positionAndRotation.Item2);
            }
        }

        private (Vector3, Quaternion) GetPositionAndRotation(Vector2 localA, Vector2 localB, Vector3 worldA, Vector3 worldB)
        {
            Vector2 localDir = (localB - localA).normalized;
            Vector2 worldDir = (worldB - worldA).normalized;
            float angle = Vector2.SignedAngle(localDir, worldDir);
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            Vector3 localMid = (localA + localB) / 2f;
            Vector3 rotatedMid = rotation * localMid;
            Vector3 worldMid = (worldA + worldB) / 2f;
            Vector3 newPosition = worldMid - rotatedMid;
            return (newPosition, rotation);
        }
    }
}