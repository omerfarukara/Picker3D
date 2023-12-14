using Picker3D.LevelSystem;

namespace Picker3D.LevelEditor
{
    public class StageData
    {
        public int StageIndex { get; set; }
        public CollectableType[,] CollectableNodeData { get; set; }
        public StageType StageType { get; set; }
    }
}
