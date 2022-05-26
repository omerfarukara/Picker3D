using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PickerController : MonoBehaviour
{
    [Header("----Speed Variables-----")]
    [SerializeField] float rampIncreaseSpeed;
    [SerializeField] float rampStartSpeed;
    [SerializeField] float rampDecreaseSpeed;

    [Header("----Jump Variables-----")]
    [SerializeField] float jumpForceWithTouch;
    [SerializeField] float jumpForceWithoutTouch;

    [Header("----Finish & Picker Variables-----")]
    [SerializeField] Vector3 finishLockRotation;
    [SerializeField] float finishLockRotAndPosTime;

    [Header("-----Collectable Variables-----")]
    [SerializeField] float throwSpeed;

    [Header("----Other Variables-----")]
    [SerializeField] GameObject arms;

    Movement _movement;
    GameController _gameController;
    DroneController _droneController;
    Rigidbody _rb;

    bool finishTap;

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _gameController = FindObjectOfType<GameController>();
        if (FindObjectOfType<DroneController>() != null)
        {
            _droneController = FindObjectOfType<DroneController>();
            _droneController.gameObject.SetActive(false);
        }
        _rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CollectableController collectable))
        {
            other.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        }
        if (other.CompareTag(Constants.Tags.ARM))
        {
            other.gameObject.SetActive(false);
            arms.SetActive(true);
            _droneController.gameObject.SetActive(true);
            _droneController.MoveLeft();
        }
        if (other.CompareTag(Constants.Tags.CONTROL_ZONE_ENTER))
        {
            StartCoroutine(ControlZoneEnter(other)); // Zýplatma ve sonrasýnda yeterli mi diye control sistemi.
            if (arms != null) { arms.SetActive(false); }
        }
        if (other.CompareTag(Constants.Tags.FINISH_LINE))
        {
            FinishLineEnter();
        }
        if (other.CompareTag(Constants.Tags.FINISH_EXIT))
        {
            FinishExit();
        }
        if (other.CompareTag(Constants.Tags.XSCORE))
        {
            other.tag = Constants.Tags.UNTAGGED;
            _gameController.XScore = other.GetComponent<XScore>().xScore.Score;
            _gameController.Invoke("LevelSuccess", 1);
        }
    }
    private void Update()
    {
        if (!finishTap) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _movement.ForwardSpeed += rampIncreaseSpeed;
            }
        }
    }

    void FinishLineEnter()
    {
        _gameController.slider.transform.parent.gameObject.SetActive(true);
        _rb.useGravity = true;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX;
        finishTap = true;
        _movement.ForwardSpeed = rampStartSpeed;
        StartCoroutine(FinishLine());
    }
    void FinishExit()
    {
        finishTap = false;
        _gameController.slider.transform.parent.gameObject.SetActive(false);
        _gameController.InFinishLineExit = true;
        transform.DOMoveX(Vector3.zero.x, finishLockRotAndPosTime);
        transform.DORotate(finishLockRotation, finishLockRotAndPosTime).OnComplete(() => _rb.constraints = RigidbodyConstraints.FreezeRotation);

        if (_movement.ForwardSpeed > 5)
        {
            _rb.AddForce(Vector3.forward * _movement.ForwardSpeed * jumpForceWithTouch);
        }
        _rb.AddForce(Vector3.forward * jumpForceWithoutTouch);
    }

    IEnumerator FinishLine()
    {
        while (finishTap && _movement.ForwardSpeed > 2)
        {
            _movement.ForwardSpeed -= rampDecreaseSpeed;
            yield return null;
        }
    }

    IEnumerator ControlZoneEnter(Collider other)
    {
        other.tag = Constants.Tags.UNTAGGED;
        _movement._rigidbody.velocity = Vector3.zero;
        _gameController.InControlZoneEnter = true;
        for (int i = 0; i < _gameController.collectedObjects.Count; i++)
        {
            _gameController.refCollectedObjects.Add(_gameController.collectedObjects[i]);
            _gameController.collectedObjects[i].GetComponent<Rigidbody>().AddForce(Vector3.forward * throwSpeed);
        }
        yield return new WaitForSeconds(3f);
        _gameController.Control();
    }
}
