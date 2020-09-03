using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
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

    public float speedup;
    public bool isPassed;
    public InstancePose instancePose;
    
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

        }
    }
    
    public void Spawn()
    {
        instancePose.InstanciateCharacter();
        wallCreator.ResetWall();
        Debug.Log("Personnage et Mur spawn");
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
            AddScore();
        }

        else
        {
            LoseOneLife();
        }
    }

    public void AddScore()
    {

    }

    void LoseOneLife()
    {
        //CanvasManager.DisplayX(lifeLeft);
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
}
public enum Phase
{
    Start,
    NewRound,
    PlayMode,
    Check,
    GameOver,
}