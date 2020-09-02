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

    public float speed;
    public float speedup;
    public bool collide;
    public bool restart;
    void Start()
    {
        TimerStart();

    }

    // Update is called once per frame
    void Update()
    {
    Game();
        
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
            
            Spawner.GetComponent<GameManage>().MovePlayer(true);
            Can_Destroy = true;
        }
        else
        if (etape == Phase.Verif) //Lorsque le joueur passera le mur
        {
            Can_Destroy = false;
            Spawner.GetComponent<GameManage>().ScoreCheck();
            Restart();
        }
    }
    public void Restart()
    {
        Spawner.GetComponent<GameManage>().UpSpeed();
        etape = Phase.Start;
    }
    public void Spawn()
    {
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
    PlayerAndWallSpawn,
    Timer,
    MovePlayer,
    Verif,
    Score,
}