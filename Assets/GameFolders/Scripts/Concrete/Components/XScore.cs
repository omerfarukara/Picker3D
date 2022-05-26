using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class XScore : MonoBehaviour
{
    public XScores xScore;
    [SerializeField] TextMeshProUGUI score;

    private void Start()
    {
        score.text = xScore.Score.ToString();
    }
}
