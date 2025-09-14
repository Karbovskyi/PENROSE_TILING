using AGame.Code.Gameplay.Features.Tiles.Factory;
using AGame.Code.Infrastructure.States.GameStates.TillingScene;
using AGame.Code.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AGame.Code.Gameplay.UI
{
    public class UI_HomeScreen : MonoBehaviour
    {
        [SerializeField] private Button _play;
        [SerializeField] private Button _kiteDartButton;
        [SerializeField] private Button _rhombsButton;
        [SerializeField] private Button _allTilesButton;
        [SerializeField] private TilesLibScriptableObject _kiteDartLib;
        [SerializeField] private TilesLibScriptableObject _rhombsLib;
        [SerializeField] private TilesLibScriptableObject _allTilesLib;

        [Inject] private readonly IGameStateMachine _stateMachine;
        [Inject] private readonly ITilesFactory _tilesFactory;

        private Button _selectedButton;

        private void Awake()
        {
            _play.onClick.AddListener(StartGame);
            _play.interactable = false;

            _kiteDartButton.onClick.AddListener(() => SelectLib(_kiteDartButton, _kiteDartLib));
            _rhombsButton.onClick.AddListener(() => SelectLib(_rhombsButton, _rhombsLib));
            _allTilesButton.onClick.AddListener(() => SelectLib(_allTilesButton, _allTilesLib));
        }

        private void StartGame()
        {
            _stateMachine.Enter<LoadingTillingSceneState>();
        }

        private void SelectLib(Button button, TilesLibScriptableObject lib)
        {
            _tilesFactory.SetTilesLib(lib);
            Colorize(button);
            _play.interactable = true;
        }

        private void Colorize(Button selected)
        {
            if (_selectedButton != null)
                _selectedButton.image.color = Color.white;
        
            selected.image.color = Color.red;
            _selectedButton = selected;
        }
    }
}