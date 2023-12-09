using UnityEngine;

namespace Picker3D.UI
{
    public class UIInput : MonoSingleton<UIInput>
    {
        [SerializeField] private Joystick _joystick;

        internal float GetHorizontal()
        {
            return _joystick.Horizontal;
        }   
    }
}
