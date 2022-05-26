using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] bool leftArm;
    [SerializeField] float rotateSpeed;

    private void Update()
    {
        if (leftArm)
        {
            transform.Rotate(-Vector3.forward * rotateSpeed);
        }
        else
        {
            transform.Rotate(Vector3.forward * rotateSpeed);
        }
    }
}
