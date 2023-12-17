using System;
using System.Collections;
using System.Collections.Generic;
using Exploder.Utils;
using Picker3D.LevelSystem;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using UnityEngine;

public class BaseCollectableObject : PoolObject
{
    [SerializeField] private List<VisualStageObject> visualObjects;

    protected  Rigidbody Rigidbody { get; set; }
    public bool IsThrow { get; protected set; }
    

    public virtual void Explode()
    {
        ExploderSingleton.Instance.ExplodeObject(gameObject);
    }

    public override void Build()
    {
        foreach (VisualStageObject visual in visualObjects)
        {
            if (visual.CollectableType == CollectableType)
            {
                visualObject = visual.gameObject;
                visual.gameObject.SetActive(true);
                Rigidbody = GetComponentInChildren<Rigidbody>();
            }
            else
            {
                visual.gameObject.SetActive(false);
            }
        }
    }

    public override void CloseObject()
    {
        foreach (VisualStageObject visual in visualObjects)
        {
            visual.gameObject.SetActive(false);
        }
    }
}