using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]

[System.Serializable]
public class Sound
{ 
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 1f;

    [Range(0f,3f)]
    public float pitch = 1f;

    public bool loop;

    public bool playOnAwake;


    [HideInInspector]
    public AudioSource source;

}
[System.Serializable]
[CustomEditor(typeof(Sound))]
public class SoundEditor : Editor
{
    [HideInInspector] public GameObject _objectWhichPlaysSound;
    [HideInInspector] public bool _is3DSound;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        _is3DSound = EditorGUILayout.Toggle("Is a 3D Sound", _is3DSound);

        if (_is3DSound)
        {
            _objectWhichPlaysSound = EditorGUILayout.ObjectField("Object Plays Sound", _objectWhichPlaysSound, typeof(GameObject), true) as GameObject;
        }
    }
}
