using UnityEditor;
using UnityEngine;

namespace Picker3D.LevelEditor
{
    public class LevelEditorWindow : EditorWindow
    {
        // Pencereyı açmak için menü öğesi ekleyin
        [MenuItem("Window/Custom Editor Window")]
        public static void ShowWindow()
        {
            GetWindow<LevelEditorWindow>("Custom Window");
        }

        void OnGUI()
        {
            // Pencere içeriğini burada oluşturun
            GUILayout.Label("Merhaba, bu özel bir pencere!");
        }
    }
}