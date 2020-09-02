using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    // Start is called before the first frame update
    public bool GamePause=false;

    public GameObject PauseUI;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pause();
    }

    public void pause()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GamePause)
            {
                Resume();
            }else
            {
                Stop();
            }
        }
    }


    public void Resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        GamePause = false;

    }

    public void Stop()
    {
        PauseUI.SetActive(true);
        Time.timeScale = 0f;
        GamePause = true;
    }
}
