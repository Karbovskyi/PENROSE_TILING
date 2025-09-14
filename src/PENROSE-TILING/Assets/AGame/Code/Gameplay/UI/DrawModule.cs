using System.Collections.Generic;
using AGame.Code.Extensions;
using AGame.Code.Gameplay.EntityIndices;
using AGame.Code.Gameplay.Features.Tiles.Data;
using AGame.Code.Gameplay.Features.Tiles.Factory;
using AGame.Code.Gameplay.Services.CameraProvider;
using Entitas;
using Shapes;
using UnityEngine;
using Zenject;

namespace AGame.Code.Gameplay.UI
{
    public class DrawModule : ImmediateModeShapeDrawer
    {
        [SerializeField] private Color ButtonColor;
        [SerializeField] private Color ButtonBoarderColor;
    
        private Vector4 _buttonsCorner = new Vector4(0.15f,0.15f,0.15f,0.15f);
        private Matrix4x4 _scaleMatrix = Matrix4x4.Scale(new Vector3(0.9f, 0.9f, 1f));
        private float _cameraDistance = 5f;

        private GameContext _game;
        private ITilesFactory _tilesFactory;
        private ICameraProvider _cameraProvider;
    
        private IGroup<GameEntity> _allStaticTiles;
        private IGroup<GameEntity> _draggingTiles;
        private IGroup<GameEntity> _spawnButtons;
    
        private List<ButtonData> _buttonsDataBuffer = new List<ButtonData>();
    
        [Inject]
        public void Inject(GameContext game, ITilesFactory tilesFactory, ICameraProvider cameraProvider)
        {
            _game = game;
            _tilesFactory = tilesFactory;
            _cameraProvider = cameraProvider;
            _allStaticTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.TileStaticData).NoneOf(GameMatcher.Dragging));
            _draggingTiles = game.GetGroup(GameMatcher.AllOf(GameMatcher.Tile, GameMatcher.TileStaticData, GameMatcher.Dragging));
            _spawnButtons = game.GetGroup(GameMatcher.AllOf(GameMatcher.TileSpawnButton, GameMatcher.ViewportCoordinatesRect, GameMatcher.TileStaticData));
        }
    
        public override void DrawShapes( Camera cam ){

            using(Draw.Command(cam))
            {
                DrawStaticTiles();
                DrawTileSpawnButtons();
                DrawDraggingTiles();
            }
        }

        private void DrawStaticTiles()
        {
            int[] ids = _tilesFactory.GetTilesTypeIds();

            for (int i = 0; i < ids.Length; i++)
            {
                HashSet<GameEntity> tiles = _game.StaticTilesOfType(ids[i]);
                    
                foreach (GameEntity tile in tiles)
                {
                    DrawTileEntity(tile);
                }
            }
        
            DrawLines(_allStaticTiles);
        }
    
        private void DrawTileSpawnButtons()
        {
            Camera cam = _cameraProvider.MainCamera;
            _buttonsDataBuffer.Clear();
        
            foreach (GameEntity entity in _spawnButtons)
            {
                Rect buttonViewportRect = entity.ViewportCoordinatesRect;
                Vector3 buttonViewportCenter = new Vector3(buttonViewportRect.center.x, buttonViewportRect.center.y, _cameraDistance);
                Vector3 buttonWorldCenter = cam.ViewportToWorldPoint(buttonViewportCenter);
                Vector3 buttonRightWorldPoint = cam.ViewportToWorldPoint(new Vector3(buttonViewportRect.xMax, buttonViewportRect.center.y, _cameraDistance));
                Vector3 buttonUpWorldPoint = cam.ViewportToWorldPoint(new Vector3(buttonViewportRect.center.x, buttonViewportRect.yMax, _cameraDistance));
                float buttonWorldWidth  = (buttonRightWorldPoint - buttonWorldCenter).magnitude * 2f;
                float buttonWorldHeight = (buttonUpWorldPoint - buttonWorldCenter).magnitude * 2f;
            
                _buttonsDataBuffer.Add(new ButtonData(buttonWorldWidth, buttonWorldHeight, buttonWorldCenter, cam.transform.rotation, entity.TileStaticData));
            }

            foreach (ButtonData data in _buttonsDataBuffer)
            {
                Rect buttonWorldRect = new Rect(-data.Width * 0.5f, -data.Height * 0.5f, data.Width, data.Height);
                DrawButton(data.Center, buttonWorldRect, data.Rotation, ButtonColor, ButtonBoarderColor, _buttonsCorner);
            }
        
            foreach (ButtonData data in _buttonsDataBuffer)
            {
                float tileScale = Mathf.Min(data.Width, data.Height) * 0.7f;
                Matrix4x4 baseMatrix = Matrix4x4.TRS(data.Center, data.Rotation, Vector3.one * tileScale);
                PolygonPath polygonPath = data.StaticData.PolygonPath;
                Color borderColor = data.StaticData.ValidBorderColor;
                GradientFill gradientFill = data.StaticData.GradientFill;
            
                DrawTile(baseMatrix, _scaleMatrix, polygonPath, borderColor, gradientFill);
            }
        }
    
        private void DrawDraggingTiles()
        {
            foreach (GameEntity tile in _draggingTiles) 
                DrawTileEntity(tile);
        
            DrawLines(_draggingTiles);
        }
    
        private void DrawTileEntity(GameEntity tile)
        {
            TileStaticData staticData = tile.TileStaticData;
        
            Matrix4x4 baseMatrix = Matrix4x4.TRS(tile.WorldPosition, tile.Rotation, Vector3.one);
            PolygonPath polygonPath = staticData.PolygonPath;
            Color borderColor = tile.isTilePositionInvalid ? staticData.InvalidBorderColor : staticData.ValidBorderColor;
            GradientFill gradientFill = staticData.GradientFill;
        
            DrawTile(baseMatrix, _scaleMatrix, polygonPath, borderColor, gradientFill);
        }

        private static void DrawTile(Matrix4x4 baseMatrix, Matrix4x4 scaledMatrix, PolygonPath polygonPath, Color borderColor, GradientFill gradientFill)
        {
            Draw.ThicknessSpace = ThicknessSpace.Meters;
        
            Draw.UseGradientFill = false;
            Draw.Matrix = baseMatrix;
            Draw.Polygon(polygonPath, borderColor);

            Draw.UseGradientFill = true;
            Draw.GradientFill = gradientFill;
            Draw.Matrix = baseMatrix * scaledMatrix;
            Draw.Polygon(polygonPath);
        }

        private void DrawLines(IGroup<GameEntity> tilesGroup)
        {
            if(!TileLinesToggle.IsOn) return;
            foreach (GameEntity tile in tilesGroup)
            {
                DrawTileLines(tile);
            }
        }

        private void DrawTileLines(GameEntity tile)
        {
            foreach (LineData line in tile.TileStaticData.Lines)
            {
                Draw.Matrix = Matrix4x4.TRS(tile.WorldPosition, tile.Rotation, Vector3.one);
                Draw.Thickness = 0.03f;
                Draw.Line(line.start, line.end, tile.TileStaticData.LineColor);
                Debug.DrawLine(line.start, line.end);
            }
        }
    
        private void DrawButton(Vector3 center, Rect rect, Quaternion rotation,
            Color color, Color boarderColor, Vector4 corners)
        {
            Draw.ThicknessSpace = ThicknessSpace.Noots;
            Draw.Matrix = Matrix4x4.identity;
            Draw.UseGradientFill = false;
        
            Draw.Rectangle(center, rotation, rect, 0.15f, boarderColor);
            Draw.Rectangle(center, rotation, rect.Scaled(0.95f), 0.15f, color);
        }
    
        struct ButtonData
        {
            public ButtonData(float width, float height, Vector3 center, Quaternion rotation, TileStaticData staticData)
            {
                Width = width;
                Height = height;
                Center = center;
                Rotation = rotation;
                StaticData = staticData;
            }
            public float Width;
            public float Height;
            public Vector3 Center;
            public Quaternion Rotation;
            public TileStaticData StaticData;
        }
    }
}

