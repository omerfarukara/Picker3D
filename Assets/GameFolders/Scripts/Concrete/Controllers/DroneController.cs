using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DroneController : MonoBehaviour
{
    [SerializeField] Vector3 stepDistance;
    [SerializeField] float rotateDegreeX;
    [SerializeField] float rotateDegreeZ;
    [SerializeField] float stepTime;
    [SerializeField] int requestedLapCount;
    [SerializeField] GameObject collectableObj;

    [SerializeField] float createTime;

    int lapCount;
    bool droneMove = true;

    float currentTime;

    private void Start()
    {
        currentTime = createTime;
    }

    private void Update()
    {
        if (lapCount == requestedLapCount) { droneMove = false; return; }

        currentTime -= Time.deltaTime;
        if (currentTime < 0)
        {
            Instantiate(collectableObj, new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), Quaternion.identity);
            currentTime = createTime;
        }
    }

    void MoveRight()
    {
        transform.DORotate(new Vector3(rotateDegreeX, 0, -rotateDegreeZ), stepTime);
        lapCount++;
        transform.DOMove(new Vector3(stepDistance.x, transform.position.y, transform.position.z + stepDistance.z), stepTime).OnStepComplete(() => MoveLeft());
    }

    public void MoveLeft()
    {
        if (!droneMove) { transform.DOMoveY(10, stepTime); return; }
        transform.DORotate(new Vector3(rotateDegreeX, 0, rotateDegreeZ), stepTime);
        transform.DOMove(new Vector3(-stepDistance.x, transform.position.y, transform.position.z + stepDistance.z), stepTime).OnStepComplete(() => MoveRight());
    }
}
