using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class GameController : MonoBehaviour
{
    [Header("----Diamond----")]
    [SerializeField] int diamondCoefficient;
    [SerializeField] GameObject diamond;
    [SerializeField] Transform diamondCanvas;
    [SerializeField] float diamondCanvasMoveTime;


    [Header("----UI----")]
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject losePanel;
    public GameObject StartPanel;


    [Header("----Stage Variables----")]
    [SerializeField] float gateDegree;
    [SerializeField] float gateOpeningTime;
    [SerializeField] float boxTargetY;


    [Header("----List----")]
    [SerializeField] List<Box> boxes;
    [SerializeField] List<GameObject> bridges;

    public TextMeshProUGUI barText;

    int collectableCount;
    int index = 0;
    int _xScore;

    bool _inControlZoneEnter;
    bool _inFinishLineExit;

    public List<GameObject> collectedObjects = new List<GameObject>(); // player'ýn alanýna girdiði zaman listeye eklenen player'ýn alanýndan çýktýðý zaman listeden silinen objelerin listesi
    public List<GameObject> refCollectedObjects = new List<GameObject>(); // Control alanýndan sonra silinmeleri için tutulan obje listesi

    public Slider slider;

    PickerController _picker;

    public int XScore
    {
        get
        {
            return _xScore;
        }
        set
        {
            _xScore = value;

        }
    }


    public bool InControlZoneEnter
    {
        get
        {
            return _inControlZoneEnter;
        }
        set
        {
            _inControlZoneEnter = value;
        }
    }

    public bool InFinishLineExit
    {
        get
        {
            return _inFinishLineExit;
        }
        set
        {
            _inFinishLineExit = value;
        }
    }

    private void Awake()
    {
        _picker = FindObjectOfType<PickerController>();
    }

    void LevelSuccess()
    {
        if (!GameManager.Instance.IsFinish)
        {
            FindObjectOfType<PickerController>().transform.GetComponentInChildren<ParticleSystem>().Play();
            winPanel.SetActive(true);
            StartCoroutine(DiamondCanvasMove());
            GameManager.Instance.Diamond = XScore * diamondCoefficient;
            GameManager.Instance.Level++;
            GameManager.Instance.IsFinish = true;
        }
    }

    public void CheckCollectableCount(TextMeshProUGUI ControlText, int requiredCollectedCount)
    {
        collectableCount++;
        ControlText.text = collectableCount + " / " + requiredCollectedCount;
    }
    public void CheckBonusCount(TextMeshProUGUI ControlText)
    {
        collectableCount++;
        ControlText.text = collectableCount.ToString();
    }

    public void Control()
    {
        if (boxes[index].IsBonusBox)
        {
            ControlSucces();
        }
        else
        {
            if (collectableCount >= boxes[index].box.RequiredCollectedCount)
            {
                ControlSucces();
            }
            else
            {
                LevelFailed();
            }
        }
    }

    private void LevelFailed()
    {
        losePanel.SetActive(true);
        GameManager.Instance.IsFinish = true;
    }

    void ControlSucces()
    {
        boxes[index].ParticlePlay();
        bridges[index].transform.DOLocalMoveY(boxTargetY, 1f).OnComplete(() =>
        {
            InControlZoneEnter = false;
            boxes[index].gates[0].transform.DOLocalRotate(Vector3.up * gateDegree, gateOpeningTime);
            boxes[index].gates[1].transform.DOLocalRotate(Vector3.up * -gateDegree, gateOpeningTime).OnComplete(() => index++);
            for (int i = 0; i < 4; i++)
            {
                boxes[index].transform.GetChild(i).GetComponent<BoxCollider>().isTrigger = true;
            }
            collectableCount = 0;
            _picker.GetComponent<Rigidbody>().velocity = Vector3.zero;
        });
        foreach (GameObject obj in refCollectedObjects)
        {
            Destroy(obj);// Yerdeki objeleri yok et.
        }
    }

    void DiamondLerp()
    {
        GameObject newDiamond = Instantiate(diamond, new Vector3(_picker.transform.position.x, _picker.transform.position.y + 1, _picker.transform.position.z), diamond.transform.rotation);
        newDiamond.transform.DOMove(diamondCanvas.position, diamondCanvasMoveTime);
        newDiamond.transform.DOScale(Vector3.zero, diamondCanvasMoveTime).OnComplete(() => newDiamond.SetActive(false));
    }

    IEnumerator DiamondCanvasMove()
    {
        if (XScore > 90)
        {
            for (int i = 0; i < XScore / 10; i++)
            {
                yield return new WaitForSeconds(0.2f);
                DiamondLerp();
            }
        }
        else
        {
            for (int i = 0; i < XScore / 100; i++)
            {
                yield return new WaitForSeconds(0.2f);
                DiamondLerp();
            }
        }

    }
}
