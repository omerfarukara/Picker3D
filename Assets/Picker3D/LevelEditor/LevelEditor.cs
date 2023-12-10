using System;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelEditor : MonoBehaviour
    {
        private void OnDrawGizmosSelected()
        {
            LevelEditorWindow.ShowWindow();
        }
        
    }
}
