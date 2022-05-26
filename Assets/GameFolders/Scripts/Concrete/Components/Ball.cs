using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    BonusBallController _bonusBallController;

    private void Awake()
    {
        _bonusBallController = gameObject.transform.parent.GetComponent<BonusBallController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == Constants.Tags.WAY)
        {
            gameObject.SetActive(false);
            _bonusBallController.Fragmentation();
        }
    }
}
