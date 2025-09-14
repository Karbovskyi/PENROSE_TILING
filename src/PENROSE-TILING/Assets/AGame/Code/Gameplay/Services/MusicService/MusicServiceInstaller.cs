using UnityEngine;
using Zenject;

namespace AGame.Code.Gameplay.Services.MusicService
{
    public class MusicServiceInstaller : MonoInstaller
    {
        [SerializeField] private MusicPlaylistScriptableObject _playlist;

        public override void InstallBindings()
        {
            GameObject audioSourceGO = new GameObject("MusicAudioSource");
            DontDestroyOnLoad(audioSourceGO);
        
            AudioSource audioSource = audioSourceGO.AddComponent<AudioSource>();
        
            Container.BindInterfacesTo<MusicService>().AsSingle().WithArguments(audioSource, _playlist);
        }
    }
}