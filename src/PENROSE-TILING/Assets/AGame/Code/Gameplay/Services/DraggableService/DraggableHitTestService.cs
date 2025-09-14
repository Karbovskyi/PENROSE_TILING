using System.Linq;
using AGame.Code.Gameplay.Features.Tiles.Data;
using AGame.Code.Gameplay.StaticServices.GeometryService;
using UnityEngine;

namespace AGame.Code.Gameplay.Services.DraggableService
{
    public class DraggableHitTestService : IDraggableHitTestService
    {
        private readonly GameContext _game;

        public DraggableHitTestService(GameContext game)
        {
            _game = game;
        }
        
        public bool ContainsPoint(Vector2 point, GameEntity draggable)
        {
            if (!draggable.Rect.Contains(point)) return false;
            
            if (draggable.isTile)
                return IsTileContainsPoint(point, draggable);

            return true;
        }

        private bool IsTileContainsPoint(Vector2 point, GameEntity tileEntity)
        {
            return tileEntity.TileTriangles.Any(triangle =>
            {
                (Vector2 a, Vector2 b, Vector2 c) = GetTriangleWorldPositions(tileEntity, triangle);
                return GeometryService.IsPointInTriangle(point, a, b, c);
            });
        }

        private (Vector2 a, Vector2 b, Vector2 c) GetTriangleWorldPositions(GameEntity tileEntity, TileTriangleIndices tileTriangle)
        {
            Vector2 a = _game.GetEntityWithId(tileEntity.TileVerticesIds[tileTriangle.A]).WorldPosition;
            Vector2 b = _game.GetEntityWithId(tileEntity.TileVerticesIds[tileTriangle.B]).WorldPosition;
            Vector2 c = _game.GetEntityWithId(tileEntity.TileVerticesIds[tileTriangle.C]).WorldPosition;

            return (a, b, c);
        }
    }
}