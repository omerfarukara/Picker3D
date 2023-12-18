using Picker3D.Scripts.Helpers;
using UnityEngine;

namespace Picker3D.UI
{
    public class UIInput : MonoSingleton<UIInput>
    {
        [SerializeField] internal Joystick joystick;

        internal float GetHorizontal()
        {
            return joystick.Horizontal;
        }   
    }
}
