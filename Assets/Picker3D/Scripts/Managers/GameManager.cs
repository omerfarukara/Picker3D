using System;
using Picker3D.Scripts.Helpers;

namespace Picker3D.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static Action OnCompleteStage { get; set; }
        public static Action OnPassedStage { get; set; }

        public static Action OnFailedStage { get; set; }
    }
}