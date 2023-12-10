using System;

namespace Picker3D.Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public static Action OnCompleteStage { get; set; }
        public static Action OnPassedStage { get; set; }
        public static Action OnFailedStage { get; set; }
        public static Action OnStageThrowControl { get; set; }
        public static Action OnPitControl { get; set; }
    }
}