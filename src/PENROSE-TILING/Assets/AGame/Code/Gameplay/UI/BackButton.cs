using AGame.Code.Infrastructure.States.GameStates.HomeScene;
using AGame.Code.Infrastructure.States.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace AGame.Code.Gameplay.UI
{
    public class BackButton : MonoBehaviour
    {
        [SerializeField] private Button _back;
    
        [Inject] private readonly IGameStateMachine _stateMachine;
    
        private void Awake()
        {
            _back.onClick.AddListener(StopGame);
        }
    
        private void StopGame()
        {
            _stateMachine.Enter<LoadingHomeScreenState>();
        }
    }
}