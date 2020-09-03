using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resume : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject PauseUI;
    public GameObject PauseManage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resume()
    {
        PauseUI.SetActive(false);
        Time.timeScale = 1f;
        PauseManage.GetComponent<Pause>().GamePause = false;
    }
}
