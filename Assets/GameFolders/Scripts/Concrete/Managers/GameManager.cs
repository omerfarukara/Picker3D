using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] int levelCount;
    [SerializeField] int randomLevelLowerLimit;

    bool isStart;
    bool isFinish;

    public bool IsStart
    {
        get
        {
            return isStart;
        }
        set
        {
            if (value)
            {
                FindObjectOfType<GameController>().StartPanel.SetActive(false);
                IsFinish = false;
            }
            isStart = value;
        }
    }

    public bool IsFinish
    {
        get
        {
            return isFinish;
        }
        set
        {
            isFinish = value;
            if (value) { isStart = false; }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public int Level
    {
        get
        {
            if (PlayerPrefs.GetInt(Constants.Prefs.LEVEL) == 0)
            {
                PlayerPrefs.SetInt(Constants.Prefs.LEVEL, 1);
                return PlayerPrefs.GetInt(Constants.Prefs.LEVEL);
            }
            else if (PlayerPrefs.GetInt(Constants.Prefs.LEVEL) > levelCount)
            {
                return Random.Range(randomLevelLowerLimit, levelCount);
            }
            else
            {
                return PlayerPrefs.GetInt(Constants.Prefs.LEVEL);
            }
        }
        set
        {
            PlayerPrefs.SetInt(Constants.Prefs.LEVEL, value);
            LevelText++;
        }
    }

    public int LevelText
    {
        get
        {
            if (PlayerPrefs.GetInt(Constants.Prefs.LEVEL_TEXT) == 0)
            {
                PlayerPrefs.SetInt(Constants.Prefs.LEVEL_TEXT, 1);
            }
            return PlayerPrefs.GetInt(Constants.Prefs.LEVEL_TEXT);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.Prefs.LEVEL_TEXT, value);
        }
    }

    public int Diamond
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.Prefs.DIAMOND);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.Prefs.DIAMOND, value);
            UIManager.Instance.diamondText.text = value.ToString();
        }
    }


    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            Invoke("StartGame", 1f);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(Level);
    }

}
