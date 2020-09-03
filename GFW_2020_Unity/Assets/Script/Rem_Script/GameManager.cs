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

    private InstancePose instancePose;
    public float speed;
    public float speedup;
    public bool collide;
    public bool restart;
    void Start()
    {
        TimerStart();
        instancePose = GetComponent<InstancePose>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void UpdateGameState(Phase newState)
    {
        state = newState;
        if (state == Phase.Start)
        {
            state = Phase.NewRound;
            Spawner.GetComponent<GameManager>().Spawn();

        }
        else if (state == Phase.NewRound)
        {
            Spawn();
            Spawner.GetComponent<GameManager>().MovePlayer(true);
            Can_Destroy = true;
        }
        else if (state == Phase.Verif) //Lorsque le joueur passera le mur
        {
            Can_Destroy = false;
            Spawner.GetComponent<GameManager>().ScoreCheck();
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
    public void TimerStart()
    {
        Debug.Log("Timer Start");
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

    public void ScoreCheck()
    {
        Debug.Log("Score: 465.31");
    }
}
public enum Phase
{
    Start,
    NewRound,
    PlayMode,
    Verif,
    Score,
}