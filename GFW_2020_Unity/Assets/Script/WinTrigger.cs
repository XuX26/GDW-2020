using UnityEditor;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            gameManager.isPassed = true;
            gameManager.UpdateGameState(Phase.Check);
            Debug.Log("pass !");
        }
    }
}