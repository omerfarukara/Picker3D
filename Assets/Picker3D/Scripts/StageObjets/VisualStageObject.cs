using Picker3D.LevelSystem;
using UnityEngine;

namespace Picker3D.Scripts.StageObjets
{
    public class VisualStageObject : MonoBehaviour
    {
        [SerializeField] private CollectableType collectableType;
        public CollectableType CollectableType => collectableType;
    }
}