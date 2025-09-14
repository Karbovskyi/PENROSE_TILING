using AGame.Code.Gameplay.Services.CameraProvider;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Features.TileSpawn.Systems
{
    public class ViewportRectToWorldRectSystem : IExecuteSystem
    {
        private readonly ICameraProvider _cameraProvider;

        private readonly IGroup<GameEntity> _rects;
        
        public ViewportRectToWorldRectSystem(GameContext game, ICameraProvider cameraProvider)
        {
            _cameraProvider = cameraProvider;

            _rects = game.GetGroup(GameMatcher.ViewportCoordinatesRect);
        }
        
        public void Execute()
        {
            var cam = _cameraProvider.MainCamera;

            foreach (GameEntity entity in _rects)
            {
                Rect viewportRect = entity.ViewportCoordinatesRect;

                // Нижній лівий і верхній правий кути у світових координатах
                Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(viewportRect.xMin, viewportRect.yMin, cam.nearClipPlane));
                Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(viewportRect.xMax, viewportRect.yMax, cam.nearClipPlane));

                // Центр прямокутника
                Vector3 center = (bottomLeft + topRight) * 0.5f;

                // Прямокутник у локальних координатах (відносно центру)
                Rect worldRect = new Rect(
                    0, 0,
                    topRight.x - bottomLeft.x,
                    topRight.y - bottomLeft.y
                );

                // Ротація прямокутника = ротація камери
                Quaternion rotation = cam.transform.rotation;

                // Зберігаємо у сутність
               // entity.ReplaceRectWithTransform(center, rotation, worldRect);

                // Або малюємо безпосередньо
                //Draw.RectangleBorder(center, rotation, worldRect, 5f);
            }
        }
    }
}