using UnityEditor;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        gameManager.AddScore();
    }

}