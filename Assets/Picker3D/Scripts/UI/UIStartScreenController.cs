using System;
using UnityEngine;
using UnityEngine.UI;

namespace Picker3D.UI
{
    public class UIStartScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button startButton;

        private void Awake()
        {
            startButton.onClick.AddListener(ButtonStartClick);
        }

        private void ButtonStartClick()
        {
            UIManager.OnStartButtonClicked?.Invoke();
            panel.SetActive(false);
        }
    }
}
