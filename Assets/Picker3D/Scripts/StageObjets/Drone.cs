using System;
using Picker3D.PoolSystem;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.StageObjects
{
    public class Drone : PoolObject
    {
        public override void Build()
        {
            UIManager.OnStartButtonClicked += OnStartButtonClickedHandler;
            visualObject.SetActive(true);
        }

        public override void CloseObject()
        {
            visualObject.SetActive(false);
        }

        private void OnStartButtonClickedHandler()
        {
            // Fly
        }
    }
}
