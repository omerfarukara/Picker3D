using System.Collections.Generic;
using Exploder.Utils;
using Picker3D.PoolSystem;
using Picker3D.Scripts.StageObjets;
using Sirenix.OdinInspector;
using UnityEngine;

public class BaseCollectableObject : PoolObject
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


    public virtual void FixedUpdate()
    {
        if (Rigidbody == null || !IsManualGravity) return;
        Vector3 velocity = Rigidbody.velocity;
        velocity.y -= gravityScale;
        Rigidbody.velocity = velocity;
    }

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