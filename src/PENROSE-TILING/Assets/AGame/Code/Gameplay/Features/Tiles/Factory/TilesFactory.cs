using System;
using System.Collections.Generic;
using System.Linq;
using AGame.Code.Extensions;
using AGame.Code.Gameplay.Features.Tiles.ScriptableObjects;
using AGame.Code.Infrastructure.Ecs.Features.Entity;
using AGame.Code.Infrastructure.Identifiers;
using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Factory
{
  public class TilesFactory : ITilesFactory
  {
    private readonly IIdentifierService _identifiers;
    
    private int[] _tilesTypeIds = Array.Empty<int>();
    private Dictionary<int, Data.TileStaticData> _tilesStaticData;

    public TilesFactory(IIdentifierService identifiers)
    {
      _identifiers = identifiers;
    }

    public void SetTilesLib(TilesLibScriptableObject lib)
    {
      CreateTilesData(lib.Tiles);
    }

    public GameEntity CreateTile(int tileTypeId, Vector2 at)
    {
      int[] vertexIds = CreateVertexEntities(tileTypeId, at)
        .Select(x => x.Id)
        .ToArray();

      return CreateTileEntity(tileTypeId, at, vertexIds);
    }

    public GameEntity CopyVertex(GameEntity vertex)
    {
      return CreateEntity.GameEmpty()
          .With(x => x.isVertex = true)
          .AddId(_identifiers.Next())
          .AddWorldPosition(vertex.WorldPosition)
          .AddVertexConnectionCount(0);
    }

    public int[] GetTilesTypeIds() 
      => _tilesTypeIds;

    public Data.TileStaticData[] GetTilesStaticData() 
      => _tilesStaticData.Values.ToArray();

    private void CreateTilesData(TileScriptableObject[] tiles)
    {
      _tilesStaticData = new Dictionary<int, Data.TileStaticData>(tiles.Length);

      for (int i = 0; i < tiles.Length; i++)
      {
        Data.TileStaticData tileStaticData = new Data.TileStaticData(i, tiles[i]);
        _tilesStaticData.Add(tileStaticData.Id, tileStaticData);
      }
      
      _tilesTypeIds = _tilesStaticData.Keys.ToArray();
    }

    private GameEntity[] CreateVertexEntities(int tileDataId, Vector2 at)
    {
      Vector2[] verticesPositions = _tilesStaticData[tileDataId].Vertices;
      int count = verticesPositions.Length;
      
      GameEntity[] vertices = new GameEntity[count];

      for (int i = 0; i < count; i++)
      {
        vertices[i] = CreateEntity.GameEmpty()
          .With(x => x.isVertex = true)
          .AddId(_identifiers.Next())
          .AddWorldPosition(at + verticesPositions[i])
          .AddVertexConnectionCount(0);
      }

      return vertices;
    }

    private GameEntity CreateTileEntity(int tileTypeId, Vector3 at, int[] vertexIds)
    {
      Data.TileStaticData tileStaticData = _tilesStaticData[tileTypeId];
      return CreateEntity.GameEmpty()
          .With(x=> x.isTile = true)
          .AddId(_identifiers.Next())
          .AddTileTypeId(tileTypeId)
          .AddTileStaticData(tileStaticData)
          .AddTileVerticesIds(vertexIds)
          .AddTileTriangles(tileStaticData.Triangles)
          .AddWorldPosition(at)
          .AddRotation(Quaternion.identity)
          .AddRect(tileStaticData.GetRect(at, Quaternion.identity, Vector3.one))
          .With(x=> x.isDraggable = true)
        ;
    }
  }
}