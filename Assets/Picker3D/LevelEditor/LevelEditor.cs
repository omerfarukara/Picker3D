using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        private const string ButtonGroup = "buttonGroup";

        [SerializeField] private int level;

        [ButtonGroup(ButtonGroup, 1)]
        private void EditLevel()
        {
            LevelEditorWindow.ShowWindow(level);
        }
        
        [ButtonGroup(ButtonGroup, 2)]
        private void NewLevel()
        {
            LevelEditorWindow.ShowWindow(-1);
        }
        
    }
}
