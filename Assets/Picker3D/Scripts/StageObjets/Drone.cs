using System;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using Picker3D.UI;
using UnityEngine;

namespace Picker3D.StageObjects
{
    public class Drone : BaseCollectableObject
    {
        protected override void OnBuild()
        {
            
        }

        public override void Build()
        {
            base.Build();
            UIManager.OnStartButtonClicked += OnStartButtonClickedHandler;
        }

        private void OnStartButtonClickedHandler()
        {
            // Fly
        }
    }
}
