using UnityEditor;
using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!gameManager.isPassed && gameManager.state == Phase.PlayMode)
            {
                gameManager.isPassed = true;
                gameManager.UpdateGameState(Phase.Check);
                Debug.Log("pass ! GO : " + other.gameObject);
            }
        }
    }
}