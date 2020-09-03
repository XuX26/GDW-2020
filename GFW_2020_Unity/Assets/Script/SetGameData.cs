using UnityEditor;
using UnityEngine;

public class SetGameData : MonoBehaviour
{
    public float characterSpeed;

    void Update()
    {
        GameData.characterSpeed = characterSpeed;
    }
}