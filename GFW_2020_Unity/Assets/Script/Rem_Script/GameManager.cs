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

    public float speed;
    public float speedup;
    public bool collide;
    public bool restart;

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
        else if (state == Phase.PlayMode) //Lorsque le joueur peut detruire
        {
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

    public void ScoreCheck()
    {
        Debug.Log("Score: 465.31");
    }

    public void AddScore()
    {

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