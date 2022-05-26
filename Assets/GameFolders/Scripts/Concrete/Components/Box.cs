using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Box : MonoBehaviour
{
    public bool IsBonusBox;
    public TextMeshProUGUI controlText;
    public Boxes box;
    public List<GameObject> gates;

    ParticleSystem confetti;

    void Start()
    {
        if (!IsBonusBox)
        {
            controlText.text = 0 + " / " + box.RequiredCollectedCount;
        }
        confetti = GetComponentInChildren<ParticleSystem>();
    }

    public void ParticlePlay()
    {
        confetti.Play();
    }
}
