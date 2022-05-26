using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] TextMeshProUGUI currentLevelText;
    [SerializeField] TextMeshProUGUI nextLevelText;

    [SerializeField] Button nextLevel;
    [SerializeField] Button restartLevel;
    [SerializeField] Button quit;


    public Text diamondText;


    private void Awake()
    {
        #region Instance
        Instance = this;
        #endregion

        nextLevel.onClick.AddListener(NextLevel);
        restartLevel.onClick.AddListener(RestartLevel);
        quit.onClick.AddListener(Quit);
    }

    void Start()
    {
        currentLevelText.text = GameManager.Instance.LevelText.ToString();
        nextLevelText.text = (GameManager.Instance.LevelText + 1).ToString();
        diamondText.text = GameManager.Instance.Diamond.ToString();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(GameManager.Instance.Level);
        GameManager.Instance.IsFinish = false;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(GameManager.Instance.Level);
        GameManager.Instance.IsFinish = false;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
