using System;
using Exploder.Utils;
using Picker3D.LevelSystem;
using UnityEngine;

namespace Picker3D.Scripts.StageObjets
{
    public class VisualStageObject : MonoBehaviour
    {
        [SerializeField] private CollectableType collectableType;
        [SerializeField] private Material collectedMaterial;
        [SerializeField] private Material nonCollectedMaterial;

        private MeshRenderer _meshRenderer;
        public Rigidbody Rigidbody { get; private set; }

        public CollectableType CollectableType => collectableType;
        
        private void Awake()
        {
            if (Rigidbody == null)
            {
                Rigidbody = GetComponent<Rigidbody>();
            }

            _meshRenderer = GetComponent<MeshRenderer>();
        }

        public void ColorChange(bool isCollected)
        {
            _meshRenderer.sharedMaterial = isCollected ? collectedMaterial : nonCollectedMaterial;
        }

        public void Build()
        {
            if (Rigidbody == null)
            {
                Rigidbody = GetComponent<Rigidbody>();
            }
            ColorChange(false);
            Rigidbody.velocity = Vector3.zero;
            Rigidbody.angularVelocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void Explode()
        {
            ExploderSingleton.Instance.ExplodeObject(gameObject);
        }
    }
}