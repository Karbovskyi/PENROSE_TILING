using AGame.Code.Extensions;
using AGame.Code.Gameplay.Features.Tiles.Factory;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileSpawn.Systems
{
    public class CreateButtonsSystem : IInitializeSystem
    {
        private readonly GameContext _game;
        private readonly ITilesFactory _tilesFactory;

        public CreateButtonsSystem(GameContext game, ITilesFactory tilesFactory)
        {
            _game = game;
            _tilesFactory = tilesFactory;
        }
        
        public void Initialize()
        {
            float buttonHeight = 0.2f;
            
            CreateButtonsZone(buttonHeight);
            CreateButtons(buttonHeight);
        }

        private void CreateButtonsZone(float buttonHeight)
        {
            Rect zoneViewportRect = new Rect(0, 0, 1, buttonHeight);
            _game.CreateEntity()
                .With(x => x.isTileSpawnButtonsZone = true)
                .AddViewportCoordinatesRect(zoneViewportRect);
        }

        private void CreateButtons(float buttonHeight)
        {
            var data = _tilesFactory.GetTilesStaticData();
            int count = data.Length;
            float buttonWidth = 1f / count;
            
            for (int i = 0; i < count; i++)
            {
                float xPosition = i * buttonWidth;

                Rect viewportRect = new Rect(xPosition, 0, buttonWidth, buttonHeight);

                _game.CreateEntity()
                    .With(x => x.isTileSpawnButton = true)
                    .AddViewportCoordinatesRect(viewportRect)
                    .AddTileStaticData(data[i]);
            }
        }
    }
}
