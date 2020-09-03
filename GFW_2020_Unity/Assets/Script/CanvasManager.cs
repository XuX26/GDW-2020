using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject[] lifeX;
    public TMP_Text textScore;
    public GameObject replayUI;

    public void DisplayX(int life)
    {
        lifeX[life-1].SetActive(true);
    }

    public void UpdateScore(int score)
    {
        textScore.text = score.ToString();
    }

    public void DisplayReplayButton()
    {
        replayUI.gameObject.SetActive(true);
    }
}
