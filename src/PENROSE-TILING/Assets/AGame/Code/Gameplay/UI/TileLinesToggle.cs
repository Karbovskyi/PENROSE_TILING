using UnityEngine;
using UnityEngine.UI;

namespace AGame.Code.Gameplay.UI
{
    public class TileLinesToggle : MonoBehaviour
    {
        public Toggle Toggle;
        public static bool IsOn;

        void Start()
        {
            IsOn = Toggle.isOn;
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        void OnToggleValueChanged(bool isOn)
        {
            IsOn = isOn;
        }
    }
}