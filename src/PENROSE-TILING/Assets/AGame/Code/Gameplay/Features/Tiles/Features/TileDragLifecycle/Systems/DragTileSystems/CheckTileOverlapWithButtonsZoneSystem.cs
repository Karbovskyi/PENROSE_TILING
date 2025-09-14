using AGame.Code.Gameplay.Services.CameraProvider;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class CheckTileOverlapWithButtonsZoneSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ICameraProvider _cameraProvider;

        private readonly IGroup<GameEntity> _draggingTiles;
        private readonly IGroup<GameEntity> _buttonsZone;
        
        public CheckTileOverlapWithButtonsZoneSystem(GameContext game, ICameraProvider cameraProvider)
        {
            _game = game;
            _cameraProvider = cameraProvider;
            _draggingTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.Dragging));
            _buttonsZone = game.GetGroup(GameMatcher.AllOf(GameMatcher.TileSpawnButtonsZone, GameMatcher.ViewportCoordinatesRect));
        }
        
        public void Execute()
        {
            foreach (GameEntity buttonsZone in _buttonsZone)
            foreach (GameEntity dragTile in _draggingTiles)
            {
                foreach (int id in dragTile.TileVerticesIds)
                {
                    GameEntity vertex = _game.GetEntityWithId(id);
                    Vector3 viewportPoint = _cameraProvider.MainCamera.WorldToViewportPoint(vertex.WorldPosition);
                    
                    if (buttonsZone.ViewportCoordinatesRect.Contains(viewportPoint))
                    {
                        dragTile.isTilePositionInvalid = true;
                        break;
                    }
                }
            }
        }
    }
}