using System;
using UnityEngine;
using UnityEngine.UI;

namespace Picker3D.UI
{
    public class UIWinScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button nextLevelButton;

        private void Awake()
        {
            nextLevelButton.onClick.AddListener(ButtonNextLevelClick);
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
