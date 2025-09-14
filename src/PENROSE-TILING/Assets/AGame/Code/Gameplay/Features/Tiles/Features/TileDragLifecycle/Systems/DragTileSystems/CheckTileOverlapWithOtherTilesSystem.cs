using System.Collections.Generic;
using AGame.Code.Gameplay.Features.Tiles.Data;
using AGame.Code.Gameplay.StaticServices.GeometryService;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileDragLifecycle.Systems.DragTileSystems
{
    public class CheckTileOverlapWithOtherTilesSystem : IExecuteSystem
    {
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _draggingTiles;
        private readonly IGroup<GameEntity> _staticTiles;
        
        private readonly List<Triangle> _dragTrianglesBuffer = new List<Triangle>(2);
        private readonly List<Triangle> _staticTrianglesBuffer = new List<Triangle>(2);

        public CheckTileOverlapWithOtherTilesSystem(GameContext game)
        {
            _game = game;
            _draggingTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.Dragging, GameMatcher.SnapLinks));
            _staticTiles = game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Tile, GameMatcher.Rect)
                .NoneOf(GameMatcher.Dragging));
        }
        
        public void Execute()
        {
            foreach (GameEntity dragTile in _draggingTiles)
            foreach (GameEntity staticTile in _staticTiles)
            {
                if(!IsTileRectsOverlap(dragTile, staticTile)) continue;
                
                GetWorldTriangles(_dragTrianglesBuffer, dragTile.TileTriangles, dragTile.TileVerticesIds);
                GetWorldTriangles(_staticTrianglesBuffer, staticTile.TileTriangles, staticTile.TileVerticesIds);

                if (GeometryService.PolygonsIntersect(_dragTrianglesBuffer, _staticTrianglesBuffer))
                {
                    dragTile.isTilePositionInvalid = true;
                    break;
                }
            }
        }

        private bool IsTileRectsOverlap(GameEntity dragTile, GameEntity staticTile)
        {
            return dragTile.Rect.Overlaps(staticTile.Rect);
        }

        private void GetWorldTriangles(List<Triangle> buffer, TileTriangleIndices[] triangles, int[] vertexes)
        {
            buffer.Clear();

            foreach (TileTriangleIndices triangle in triangles)
            {
                Vector3 positionA = _game.GetEntityWithId(vertexes[triangle.A]).WorldPosition;
                Vector3 positionB = _game.GetEntityWithId(vertexes[triangle.B]).WorldPosition;
                Vector3 positionC = _game.GetEntityWithId(vertexes[triangle.C]).WorldPosition;
                    
                buffer.Add(new Triangle(positionA, positionB, positionC));
            }
        }
    }
}