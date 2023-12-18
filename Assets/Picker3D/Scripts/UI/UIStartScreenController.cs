using UnityEngine;
using UnityEngine.UI;

namespace Picker3D.UI
{
    public class UIStartScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button startButton;

        private void OnEnable()
        {
            UIManager.OnNextLevelButtonClicked += OnCompleteStage;
            UIManager.OnRestartLevelButtonClicked += OnCompleteStage;
        }

        private void Awake()
        {
            startButton.onClick.AddListener(ButtonStartClick);
        }

        private void OnDisable()
        {
            UIManager.OnNextLevelButtonClicked -= OnCompleteStage;
            UIManager.OnRestartLevelButtonClicked -= OnCompleteStage;
        }

        private void OnCompleteStage()
        {
            panel.SetActive(true);
        }

        private void ButtonStartClick()
        {
            UIManager.OnStartButtonClicked?.Invoke();
            panel.SetActive(false);
        }
    }
}