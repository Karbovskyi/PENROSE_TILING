using System;
using Shapes;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Behaviours
{
    public class TileView : MonoBehaviour
    {
        [SerializeField] private Polygon _polygon;
        [SerializeField] private Polygon _innerPolygon;
        [SerializeField] private GameObject _kiteLines;
        [SerializeField] private GameObject _dartLines;
    
        [SerializeField] private Color _kiteColor1;
        [SerializeField] private Color _kiteColor2;
        [SerializeField] private Color _dartColor1;
        [SerializeField] private Color _dartColor2;
    
        public void SetPoints(Vector2[] points, int tileId)
        {
            _polygon.points.Clear();
            _innerPolygon.points.Clear();
            for (var i = 0; i < points.Length; i++)
            {
                _polygon.AddPoint(points[i]);
                _innerPolygon.AddPoint(points[i]);
            }
        
            _polygon.meshOutOfDate = true;
            _innerPolygon.meshOutOfDate = true;

            /*
            switch (tileId)
            {
                case TileType.Unknown:
                    break;
                case TileType.Kite:
                    _kiteLines.SetActive(true);
                    _innerPolygon.FillColorStart = _kiteColor1;
                    _innerPolygon.FillColorEnd = _kiteColor2;
                    break;
                case TileType.Dart:
                    _dartLines.SetActive(true);
                    _innerPolygon.FillColorStart = _dartColor1;
                    _innerPolygon.FillColorEnd = _dartColor2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(tileId), tileId, null);
            }
            */
        }

        public void SetValidHighlight(bool isValid)
        {
            _polygon.Color = isValid ? Color.white : Color.red;
        }
    }
}