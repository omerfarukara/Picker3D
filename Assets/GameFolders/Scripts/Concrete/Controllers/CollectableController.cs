using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableController : MonoBehaviour
{
    GameController _gameController;

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER))
        {
            _gameController.collectedObjects.Add(gameObject); // Player'ýn içine girdiði için listeye eklendi.
        }
        if (other.CompareTag(Constants.Tags.CONTROL_ZONE))
        {
            FindObjectOfType<GameController>().CheckCollectableCount(other.GetComponent<Box>().controlText, other.GetComponent<Box>().box.RequiredCollectedCount); // Alana girdiði için sayý artýmý yapýlýyor.
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Constants.Tags.PLAYER))
        {
            _gameController.collectedObjects.Remove(gameObject); // Player'ýn içinden dýþarý çýktý.
        }
    }
}
