using UnityEngine;

namespace AGame.Code.Gameplay.Features.Tiles.Factory
{
    public interface ITilesFactory
    {
        void SetTilesLib(TilesLibScriptableObject lib);
        GameEntity CreateTile(int tileTypeId, Vector2 at);
        GameEntity CopyVertex(GameEntity vertex);


        int[] GetTilesTypeIds();
        Data.TileStaticData[] GetTilesStaticData();
    }
}