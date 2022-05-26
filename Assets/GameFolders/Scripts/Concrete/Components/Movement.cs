using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float _forwardSpeed;
    [SerializeField] float _maxspeed;
    [SerializeField] float boundLimit;
    [SerializeField] float horizontalSpeed;
    [SerializeField] float maxForwardSpeed;


    float moveFactorX;
    float startPosX;
    float horizontal;


    GameController _gameController;

    public Rigidbody _rigidbody;

    #region Encapsulation


    public float ForwardSpeed
    {
        get
        {
            return _forwardSpeed;
        }
        set
        {
            if (_forwardSpeed < maxForwardSpeed)
            {
                _gameController.slider.value = value / maxForwardSpeed;
                _gameController.barText.text = ((int)((value / maxForwardSpeed) * 100)).ToString();
                _forwardSpeed = value;
            }
        }
    }
    #endregion

    private void Awake()
    {
        _gameController = FindObjectOfType<GameController>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && !GameManager.Instance.IsFinish && !GameManager.Instance.IsStart)
            {
                GameManager.Instance.IsStart = true;
            }
        }
        if (_gameController.InControlZoneEnter || _gameController.InFinishLineExit || !GameManager.Instance.IsStart) return;

        _rigidbody.velocity = new Vector3(horizontal * horizontalSpeed, 0, _forwardSpeed);

        #region Input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPosX = Camera.main.ScreenToViewportPoint(touch.position).x;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                moveFactorX = Camera.main.ScreenToViewportPoint(touch.position).x - startPosX;
                startPosX = Camera.main.ScreenToViewportPoint(touch.position).x;
                horizontal = moveFactorX;

                if (transform.position.x > boundLimit && horizontal > 0 || transform.position.x < -boundLimit && horizontal < 0)
                {
                    horizontal = 0;
                }
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                horizontal = 0;
            }
            else if (touch.phase == TouchPhase.Canceled)
            {
                horizontal = 0;
            }
        }
        #endregion
    }
}
