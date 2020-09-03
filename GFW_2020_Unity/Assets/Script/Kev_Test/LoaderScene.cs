using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoaderScene : MonoBehaviour
{
    public bool isMainScene;
    public void LoadingScene(int number)
    {
        SceneManager.LoadScene(number);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
