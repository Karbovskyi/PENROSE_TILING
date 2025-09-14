using UnityEngine;
using UnityEngine.UI;

namespace AGame.Code.Gameplay.UI
{
    public class HelpToggle : MonoBehaviour
    {
        public Toggle Toggle;
        public GameObject HelpView;

        void Start()
        {
            HelpView.SetActive(Toggle.isOn);
            Toggle.onValueChanged.AddListener(OnToggleValueChanged);
        }

        void OnToggleValueChanged(bool isOn)
        {
            HelpView.SetActive(isOn);
        }
    }
}