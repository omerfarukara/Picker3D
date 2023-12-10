using UnityEngine;
using UnityEngine.UI;

namespace Picker3D.UI
{
    public class UIFailScreenController : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private Button tryAgainButton;

        private void Awake()
        {
            tryAgainButton.onClick.AddListener(ButtonTryAgainClick);
        }

        private void OnFailLevelHandler()
        {
            panel.SetActive(true);
        }

        private void ButtonTryAgainClick()
        {
            UIManager.OnRestartLevelButtonClicked?.Invoke();
            panel.SetActive(false);
        }
    }
}
