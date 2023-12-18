using Picker3D.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Picker3D.UI
{
    public class UIWinScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button nextLevelButton;

        private void OnEnable()
        {
            GameManager.OnCompleteLevel += OnWinLevelHandler;
        }
        
        private void Awake()
        {
            nextLevelButton.onClick.AddListener(ButtonNextLevelClick);
        }

        private void OnDisable()
        {
            GameManager.OnCompleteLevel -= OnWinLevelHandler;
        }


        private void OnWinLevelHandler()
        {
            panel.SetActive(true);
        }
        
        private void ButtonNextLevelClick()
        {
            UIManager.OnNextLevelButtonClicked?.Invoke();
            panel.SetActive(false);
        }
    }
}
