using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    Phase state = Phase.Start;

    public GameObject Spawner;
    public bool Can_Destroy;
    public WallCreator wallCreator;
    public float timerStart;
    public int life;

    public float speed;
    public float speedup;
    public bool collide;
    public bool restart;
    public bool isPassed;

    private InstancePose instancePose;
    
    void Start()
    {
        instancePose = GetComponent<InstancePose>();
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
    }

    public void UpdateGameState(Phase newState)
    {
        state = newState;
        if (state == Phase.Start)
        {
        }
        else if (state == Phase.NewRound)
        {
            Spawn();
            //Spawner.GetComponent<GameManager>().MovePlayer(true);
        }
        else if (state == Phase.Check) //Fin du timer ou trigger enter
        {
            CheckResult();
        }
    }
    public void NextRound()
    {
        UpdateGameState(Phase.NewRound);
        Spawner.GetComponent<GameManager>().UpSpeed();
    }
    public void Spawn()
    {
        //instancePose.instance();
        wallCreator.ResetWall();
        Debug.Log("Personnage et Mur spawn");
    }

    public void UpSpeed()
    {
        speed += speedup;
    }

    public void MovePlayer(bool Move)
    {
        if(Move==true)
        {
            Debug.Log("Le Perso avance");
        }else
        {
            Debug.Log("Le Perso se stop");
        }
    }

    public void CheckResult()
    {
        if (isPassed)
        {
            AddScore();
            UpdateGameState(Phase.NewRound);
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

    }

    void CheckLife()
    {
        if(life == 0)
            UpdateGameState(Phase.GameOver);
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