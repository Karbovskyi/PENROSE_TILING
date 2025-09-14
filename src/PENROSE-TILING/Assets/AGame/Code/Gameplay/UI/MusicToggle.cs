using AGame.Code.Gameplay.Services.MusicService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AGame.Code.Gameplay.UI
{
    public class MusicToggle : MonoBehaviour
    {
        public Toggle Toggle;
        [Inject] private IMusicService _musicService;

        void Start()
        {
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        void OnToggleValueChanged(bool isOn)
        {
            if (isOn)
                _musicService.Play();
            else
                _musicService.Stop();
        }
    }
}