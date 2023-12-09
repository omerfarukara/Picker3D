using System;

namespace Picker3D.UI
{
    public class UIManager : MonoSingleton<UIManager>
    {
        public static Action OnStartButtonClicked { get; set; }
        public static Action OnNextLevelButtonClicked { get; set; }
    }
}
