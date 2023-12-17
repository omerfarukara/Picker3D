using UnityEngine;

namespace Picker3D.Scripts.Helpers
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static volatile T instance = null;

        public static T Instance => instance;

        protected virtual void Awake()
        {
            Singleton();
        }

        private void Singleton()
        {
            if (instance == null)
            {
                instance = this as T;
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}