using System.Collections.Generic;
using AGame.Code.Gameplay.EntityIndices;
using AGame.Code.Gameplay.Features.Tiles.Factory;
using AGame.Code.Gameplay.Services.CameraProvider;
using Entitas;
using Shapes;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileSpawn.Systems
{
    public class InitializeTileView2 : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly ICameraProvider _cameraProvider;
        private readonly ITilesFactory _tilesFactory;

        Matrix4x4 _scaleMatrix = Matrix4x4.Scale(new Vector3(0.9f, 0.9f, 1f));
        private readonly IGroup<GameEntity> _draggingTiles;

        public InitializeTileView2(GameContext game, ICameraProvider cameraProvider, ITilesFactory tilesFactory)
        {
            _game = game;
            _cameraProvider = cameraProvider;
            _tilesFactory = tilesFactory;
            
            _draggingTiles = game.GetGroup(GameMatcher.AllOf( GameMatcher.Dragging, GameMatcher.Tile));
        }

        public void Execute()
        {
            using(Draw.Command( _cameraProvider.MainCamera ))
            {
                int[] ids = _tilesFactory.GetTilesTypeIds();
                
                // Draw static tiles
                for (int i = 0; i < ids.Length; i++)
                {
                    HashSet<GameEntity> tiles = _game.StaticTilesOfType(ids[i]);
                    
                    foreach (GameEntity tile in tiles)
                    {
                        DrawTile(tile);
                    }
                }
                
                // Draw Dragging Tiles
                foreach (GameEntity tile in _draggingTiles)
                {
                    DrawTile(tile);
                }
                
                
                // 🔥 Малюємо прямокутник, «прикріплений» до камери
                DrawCameraRect(_cameraProvider.MainCamera, 0f, 1f, 0f, 0.2f, Color.green);
            }
        }

        private void DrawTile(GameEntity tile)
        {
            Matrix4x4 baseMatrix = Matrix4x4.TRS(tile.WorldPosition, tile.Rotation, Vector3.one);
                        
            Data.TileStaticData staticData = tile.TileStaticData;
            Draw.UseGradientFill = false;
            Draw.Matrix = baseMatrix;
            Draw.Polygon(staticData.PolygonPath, staticData.ValidBorderColor);
                        
            Draw.UseGradientFill = true;
            Draw.GradientFill = staticData.GradientFill;
            Draw.Matrix = baseMatrix * _scaleMatrix;
            Draw.Polygon(staticData.PolygonPath);
        }
        
        
        /// <summary>
        /// Малює прямокутник у координатах viewport (нормалізованих 0..1),
        /// який завжди прилипає до камери з orthographicSize.
        /// </summary>
        private void DrawCameraRect(Camera cam, float xMin, float xMax, float yMin, float yMax, Color color)
        {
            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            // Нижній та верхній Y
            float y0 = -halfHeight + (yMin * 2f * halfHeight);
            float y1 = -halfHeight + (yMax * 2f * halfHeight);

            // Лівий та правий X
            float x0 = -halfWidth + (xMin * 2f * halfWidth);
            float x1 = -halfWidth + (xMax * 2f * halfWidth);

            // Офсет перед камерою
            float zOffset = -1f;

            // Кути прямокутника у локальних координатах камери
            Vector3 bl = new Vector3(x0, y0, zOffset);
            Vector3 br = new Vector3(x1, y0, zOffset);
            Vector3 tr = new Vector3(x1, y1, zOffset);
            Vector3 tl = new Vector3(x0, y1, zOffset);

            // Створюємо PolygonPath
            PolygonPath rect = new PolygonPath();
            rect.AddPoint(bl);
            rect.AddPoint(br);
            rect.AddPoint(tr);
            rect.AddPoint(tl);

            // Малюємо у локальних координатах камери
            Draw.UseGradientFill = false;
            Draw.Matrix = cam.transform.localToWorldMatrix;
            Draw.Polygon(rect, color);
        }
    }
}