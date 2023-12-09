using System.Collections.Generic;
using UnityEngine;
using Exploder.Utils;

namespace Exploder.Demo
{
    public class QuickStartDemo : MonoBehaviour
    {
        [SerializeField] private List<GameObject> objects;

        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 10, 120, 70), "Explode sphere"))
            {
                ExploderSingleton.Instance.ExplodeObjects(objects.ToArray());
                foreach (var obj in objects)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}