using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GameManage : MonoBehaviour
{
    // Start is called before the first frame update
    Phase etape = Phase.Start;

    public GameObject Spawner;
    public bool Can_Destroy;
    public int timer = 600;
    public bool collide;
    public bool restart;
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Game();

       
        if (etape == Phase.Timer)
        {
            timer -= 1;
        }

    }

    public void Game()
    {
        if (etape == Phase.Start)
        {
            etape = Phase.PlayerAndWallSpawn;
            Spawner.GetComponent<GameManage>().Spawn();

        }
        else
        if (etape == Phase.PlayerAndWallSpawn)
        {
            etape = Phase.Timer;
            Spawner.GetComponent<GameManage>().TimerStart();
            Spawner.GetComponent<GameManage>().MovePlayer(true);
            Can_Destroy = true;
        }
        else
        if (etape == Phase.Timer && timer <= 0)
        {
            Can_Destroy = false;
            etape = Phase.Verif;

        }
        else
        if (etape == Phase.Verif)
        {
            Spawner.GetComponent<GameManage>().ScoreCheck();

        }
    }
    public void Restart()
    {
        timer = 600;
        restart = false;
        etape = Phase.Start;
    }
    public void Spawn()
    {
        Debug.Log("Personnage et Mur spawn");
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
    PlayerAndWallSpawn,
    Timer,
    MovePlayer,
    Verif,
    Score,
}