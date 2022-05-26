using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBallController : MonoBehaviour
{
    [SerializeField] List<GameObject> littleBalls;
    [SerializeField] GameObject mainBall;

    public void Fragmentation()
    {
        foreach (GameObject ball in littleBalls)
        {
            ball.transform.parent = null;
            ball.SetActive(true);
            ball.GetComponent<Rigidbody>().useGravity = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER))
        {
            mainBall.SetActive(true);
            mainBall.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
