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
        }

        private void OnStartButtonClickedHandler()
        {
            // Fly
        }
    }
}
