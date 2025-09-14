using System.Collections.Generic;
using AGame.Code.Gameplay.Features.DragAndDrop.Extensions;
using AGame.Code.Gameplay.Features.Tiles.Factory;
using AGame.Code.Gameplay.Services.CameraProvider;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileSpawn.Systems
{
    public class SpawnTileSystem : IExecuteSystem
    {
        private readonly ITilesFactory _tilesFactory;
        private readonly ICameraProvider _cameraProvider;
        private readonly IGroup<InputEntity> _touch;
        private readonly IGroup<GameEntity> _buttons;
        List<InputEntity> _buffer = new(16);
        
        public SpawnTileSystem(InputContext input, GameContext game, ITilesFactory tilesFactory, ICameraProvider cameraProvider)
        {
            _tilesFactory = tilesFactory;
            _cameraProvider = cameraProvider;
            _touch = input.GetGroup(InputMatcher.AllOf(InputMatcher.TouchId, InputMatcher.TouchStarted)
                .NoneOf(InputMatcher.Processed));
            _buttons = game.GetGroup(GameMatcher.AllOf(GameMatcher.TileSpawnButton, GameMatcher.ViewportCoordinatesRect, GameMatcher.TileStaticData));
        }
        
        public void Execute()
        {
            foreach (InputEntity touch in _touch.GetEntities(_buffer))
            {
                Vector3 viewportPoint = _cameraProvider.MainCamera.ScreenToViewportPoint(touch.TouchPosition);

                foreach (GameEntity button in _buttons)
                {
                    if (button.ViewportCoordinatesRect.Contains(viewportPoint))
                    {
                        GameEntity tile = _tilesFactory.CreateTile(button.TileStaticData.Id, touch.WorldPosition);
                        tile.ReplaceRotation(_cameraProvider.MainCamera.transform.rotation);
                        
                        tile.StartDrag(touch);
                    }
                }
            }
        }
    }
}