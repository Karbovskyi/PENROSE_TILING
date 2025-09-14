using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace AGame.Code.Gameplay.Services.MusicService
{
    public interface IMusicService
    {
        void Play();
        void Stop();
    }
    
    public class MusicService : IMusicService, ITickable, IInitializable
    {
        private readonly MusicPlaylistScriptableObject _playlist;
        private readonly AudioSource _audioSource;

        private bool _isPlaying;
        private Queue<int> _shuffleQueue;
        private int _lastIndex = -1;

        public MusicService(AudioSource audioSource, MusicPlaylistScriptableObject playlist)
        {
            _playlist = playlist;
            _audioSource = audioSource;
            _audioSource.loop = false;
            _audioSource.playOnAwake = false;
        }

        public void Initialize()
        {
            Play();
        }

        public void Play()
        {
            _isPlaying = true;
            RefillShuffleQueue();
            PlayNext();
        }

        public void Stop()
        {
            _isPlaying = false;
            _audioSource.Stop();
        }
        
        public void Tick()
        {
            if(!_isPlaying) return;
            
            if (!_audioSource.isPlaying && _playlist.Tracks.Length > 0)
            {
                PlayNext();
            }
        }

        private void PlayNext()
        {
            if (_playlist.Tracks.Length == 0) return;
            if (_shuffleQueue.Count == 0)
                RefillShuffleQueue();

            int nextIndex = _shuffleQueue.Dequeue();
            _lastIndex = nextIndex;

            _audioSource.clip = _playlist.Tracks[nextIndex];
            _audioSource.Play();
        }

        private void RefillShuffleQueue()
        {
            List<int> indices = new List<int>();
            for (int i = 0; i < _playlist.Tracks.Length; i++)
            {
                indices.Add(i);
            }

            // Перемішування Фішера-Йєйтса
            for (int i = indices.Count - 1; i > 0; i--)
            {
                int j = Random.Range(0, i + 1);
                (indices[i], indices[j]) = (indices[j], indices[i]);
            }

            // Якщо нова черга починається тим самим треком, що був останній — міняємо місцями
            if (indices.Count > 1 && indices[0] == _lastIndex)
            {
                int swapIndex = Random.Range(1, indices.Count);
                (indices[0], indices[swapIndex]) = (indices[swapIndex], indices[0]);
            }

            _shuffleQueue = new Queue<int>(indices);
        }
    }
}