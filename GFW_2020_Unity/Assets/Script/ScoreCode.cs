using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreCode : MonoBehaviour
{

    public static int scoreValue = 0;
    //Text scoreText;
    private TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + scoreValue; 
    }

    // ScoreCode.scoreVlue += ....;
}
