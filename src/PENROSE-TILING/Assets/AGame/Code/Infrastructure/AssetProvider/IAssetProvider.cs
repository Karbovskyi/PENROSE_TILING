using UnityEngine;

namespace AGame.Code.Infrastructure.AssetProvider
{
    public interface IAssetProvider
    {
        GameObject LoadAsset(string path);
        T LoadAsset<T>(string path) where T : Component;
    }
}