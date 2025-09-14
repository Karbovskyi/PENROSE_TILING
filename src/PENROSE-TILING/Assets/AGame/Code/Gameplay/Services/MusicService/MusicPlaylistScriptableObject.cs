using UnityEngine;

namespace AGame.Code.Gameplay.Services.MusicService
{
    [CreateAssetMenu(menuName = "CreateScriptableObjects/MusicPlaylist", fileName = "Playlist", order = 0)]
    public class MusicPlaylistScriptableObject : ScriptableObject
    {
        public AudioClip[] Tracks;
    }
}