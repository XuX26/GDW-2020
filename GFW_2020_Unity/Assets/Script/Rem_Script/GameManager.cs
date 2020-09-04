using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Phase state = Phase.Start;

    public GameObject Spawner;
    public bool Can_Destroy;
    public WallCreator wallCreator;
    public CanvasManager CanvasManager;
    public float timerStart;
    public float timerRound;
    public int lifeLeft;
    public float timeLeft;
    public SoundManager soundManager;
    public GameObject WallWinFx;
    public GameObject FxWin;

    public float speedup;
    public bool isPassed;
    public InstancePose instancePose;

    private int score;
    
    void Start()
    {
        UpdateGameState(Phase.Start);
    }

    // Update is called once per frame
    void Update()
    {
        if (state == Phase.Start)
        {
            timerStart -= Time.deltaTime;
            if (timerStart < 0)
            {
                UpdateGameState(Phase.NewRound);
            }
        }
        else if (state == Phase.PlayMode)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                UpdateGameState(Phase.Check);
                Debug.Log("Time out ! ");
            }
        }

         if (Input.GetKeyDown(KeyCode.Q))
        {
           
        }

    }

    public void UpdateGameState(Phase newState)
    {
        state = newState;
        if (state == Phase.Start)
        {
        }
        else if (state == Phase.NewRound)
        {
            Debug.Log("New Round Called");
            RestartValues();
            Spawn();
            //Spawner.GetComponent<GameManager>().MovePlayer(true);
            UpdateGameState(Phase.PlayMode);
            UpSpeed();

        }
        else if (state == Phase.Check) //Fin du timer ou trigger enter
        {
            DestroyChar();
            CheckResult();
            CheckLife();
        }
        else if (state == Phase.GameOver)
        {
            CanvasManager.DisplayReplayButton();
            AfficheFx();

        }
    }
    
    public void Spawn()
    {
        instancePose.InstanciateCharacter();
        wallCreator.ResetWall();
        Debug.Log("Personnage et Mur spawn");
    }

    public void AfficheFx()
    {
        FxWin.SetActive(true);
    }

    private void DestroyChar()
    {
        Destroy(instancePose.actualChar.gameObject);
    }

    public void UpSpeed()
    {
        GameData.characterSpeed += speedup;
    }

    void RestartValues()
    {
        timeLeft = timerRound;
        isPassed = false;
    }

    public void CheckResult()
    {
        if (isPassed)
        {
           soundManager.PlaySfx("Pass_Mur");
           AddScore();
        }

        else
        {
            LoseOneLife();
        }
    }

    public void AddScore()
    {
        soundManager.PlaySfx("Score");
        score += 150;
        CanvasManager.UpdateScore(score);
        Instantiate(WallWinFx, new Vector3(0, 3, 0), Quaternion.identity);
    }

    void LoseOneLife()
    {
        CanvasManager.DisplayX(lifeLeft);
        lifeLeft--;
    }

    void CheckLife()
    {
        if(lifeLeft == 0)
            UpdateGameState(Phase.GameOver);
        else
        {
            UpdateGameState(Phase.NewRound);
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
public enum Phase
{
    Start,
    NewRound,
    PlayMode,
    Check,
    GameOver,
}