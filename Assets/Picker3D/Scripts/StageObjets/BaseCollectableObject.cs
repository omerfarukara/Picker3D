using System.Collections.Generic;
using Exploder.Utils;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class BaseCollectableObject : PoolObject
{
    [SerializeField] protected List<VisualStageObject> visualObjects;

    [SerializeField] private bool isManualGravity;

    [ShowIf(nameof(IsManualGravity))] [SerializeField]
    private float gravityScale;


    protected bool IsManualGravity
    {
        get => isManualGravity;
        set => isManualGravity = value;
    }


    protected float GravityScale
    {
        get => gravityScale;
        set => gravityScale = value;
    }

    public bool IsThrow { get; protected set; }
    protected Rigidbody Rigidbody { get; set; }
    
    protected virtual void FixedUpdate()
    {
        if (Rigidbody == null || !IsManualGravity || !Rigidbody.useGravity) return;
        Vector3 velocity = Rigidbody.velocity;
        velocity.y -= gravityScale;
        Rigidbody.velocity = velocity;
    }

    protected abstract void OnBuild();

    public override void Build()
    {
        foreach (VisualStageObject visual in visualObjects)
        {
            if (visual.CollectableType == CollectableType)
            {
                visual.gameObject.SetActive(true);
                visual.Build();
                Rigidbody = visual.Rigidbody;
            }
            else
            {
                visual.gameObject.SetActive(false);
            }
        }
        OnBuild();
    }

    public override void CloseObject()
    {
        foreach (VisualStageObject visual in visualObjects)
        {
            visual.gameObject.SetActive(false);
        }
    }
}