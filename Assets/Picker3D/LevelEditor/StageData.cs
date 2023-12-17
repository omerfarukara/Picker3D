using Picker3D.General;
using Picker3D.LevelSystem;

namespace Picker3D.LevelEditor
{
    public class StageData
    {
        public int StageIndex { get; set; }

        public CollectableType[,] NormalCollectableNodeData { get; set; } = new CollectableType[GameConstants.NormalColumnCount, GameConstants.NormalRowCount];
        public CollectableType[,] BigCollectableNodeData { get; set; } = new CollectableType[GameConstants.BigColumnCount, GameConstants.BigRowCount];
        public StageType StageType { get; set; }
    }
}
