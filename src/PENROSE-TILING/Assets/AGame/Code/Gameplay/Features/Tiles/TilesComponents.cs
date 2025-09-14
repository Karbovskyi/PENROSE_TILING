using System.Collections.Generic;
using AGame.Code.Gameplay.Features.Tiles.Behaviours;
using AGame.Code.Gameplay.Features.Tiles.Data;
using Entitas;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles
{
    [Game] public class TileSpawnButton : IComponent { }
    [Game] public class ViewportCoordinatesRect : IComponent { public Rect Value; }
    [Game] public class TileSpawnButtonsZone : IComponent { }
    
    [Game] public class Tile : IComponent { }
    [Game] public class TileTypeId : IComponent { public int Value;}
    [Game] public class TileVerticesIds : IComponent { public int[] Value; }
    [Game] public class TileTriangles : IComponent { public TileTriangleIndices[] Value; }
    [Game] public class SnapLinks : IComponent { public List<SnapLinkData> Value; }
    [Game] public class TilePositionInvalid : IComponent { }
    [Game] public class TileViewInitializationRequest : IComponent { }
    [Game] public class TileViewComponent : IComponent { public TileView Value;}

    [Game] public class Vertex : IComponent { }
    [Game] public class VertexConnectionCount : IComponent { public int Value; }
    [Game] public class TileStaticData : IComponent { public Data.TileStaticData Value; }
}