using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public enum Enum
    {
        None,
        Slider,
        Toggle
    }
    public enum TextInsert
    {
        None,
        Text,
        TMPro
    }


    float _volume = 1;
    [HideInInspector] public float _volumeMaster = 1;
    public Sound[] musics;

    [HideInInspector] public Enum _toolControlsSound;
    [HideInInspector] public TextInsert _textShowSliderValue;

    [HideInInspector] public Toggle _toggleWhichChanges;
    [HideInInspector] public Slider _sliderSound;


    [HideInInspector] public Text textValueSlider;
    [HideInInspector] public TextMeshProUGUI tmproValueSlider;

    [HideInInspector] public string textValueToggleOn, textValueToggleOff;


    [HideInInspector] public bool _hideInspector;


    public static MusicManager Instance { get; private set; }

    int _hasAlreadyChangedMusicValue;

    Music editor;

    [HideInInspector] public AudioManager audioManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            ChangeToggle();
        }

        foreach (Sound s in Instance.musics)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
            s.source.volume = s.volume;
        }

        Load();
        switch (_toolControlsSound)
        {
            case Enum.None:
                _volume = 1;
                break;
            case Enum.Slider:
                ChangeSlider();
                break;
            case Enum.Toggle:
                ChangeToggle();
                break;
            default:
                break;
        }
    }
    void Start()
    {
        if (_toolControlsSound.ToString() == "Slider")
        {
            if (_sliderSound != null)
            {
                _sliderSound.onValueChanged.AddListener(delegate { ChangeSlider(); }); ;
            }
        }
        else if (_toolControlsSound.ToString() == "Toggle")
        {
            if (_toggleWhichChanges != null)
            {
                _toggleWhichChanges.onValueChanged.AddListener(delegate { ChangeToggle(); }); ;
            }
        }
        SetVolume();
        PlayOnAwake();
    }
    void Load()
    {
        _hasAlreadyChangedMusicValue = PlayerPrefs.GetInt("Has already changed Sound values");

        if (_hasAlreadyChangedMusicValue == 1)
        {
            _volume = PlayerPrefs.GetFloat("VolumeSFX");

            if (_toolControlsSound.ToString() == "Slider")
            {
                if (_sliderSound != null)
                {
                    _sliderSound.value = _volume;
                }
            }
            else if (_toolControlsSound.ToString() == "Toggle")
            {
                if (_toggleWhichChanges != null)
                {
                    if (_volume == 0)
                    {
                        _toggleWhichChanges.isOn = false;
                    }
                    else
                    {
                        _toggleWhichChanges.isOn = true;
                    }
                }
            }
        }
    }

    void Save()
    {
        PlayerPrefs.SetFloat("Volume", _volume);
        _hasAlreadyChangedMusicValue = 1;
        PlayerPrefs.SetInt("Has already changed Music values", _hasAlreadyChangedMusicValue);
    }


    public void ChangeSlider()
    {
        if (_sliderSound != null)
        {
            _volume = _sliderSound.value;
        }
        AttributionSlider();
        SetVolume();
    }
    public void ChangeToggle()
    {
        if (_toggleWhichChanges != null)
        {
            if (_toggleWhichChanges.isOn)
            {
                _volume = 1;
            }
            else
            {
                _volume = 0;
            }
        }
        AttributionToggle();
        SetVolume();


        Debug.Log("Foreach");
    }


    void AttributionToggle()
    {
        if (_textShowSliderValue.ToString() == "Text")
        {
            if (textValueSlider != null)
            {
                if (_toggleWhichChanges.isOn)
                    textValueSlider.text = textValueToggleOn;
                else
                    textValueSlider.text = textValueToggleOff;
            }
        }
        else if (_textShowSliderValue.ToString() == "TMPro")
        {
            if (tmproValueSlider != null)
            {
                if (_toggleWhichChanges.isOn)
                    tmproValueSlider.text = textValueToggleOn;
                else
                    tmproValueSlider.text = textValueToggleOff;
            }
        }
    }

    void AttributionSlider()
    {
        if (_toolControlsSound.ToString() == "Slider")
        {
            if (_textShowSliderValue.ToString() == "Text")
            {
                if (textValueSlider != null)
                {
                    textValueSlider.text = Mathf.RoundToInt((_volume * 100)).ToString();
                }
            }
            else if (_textShowSliderValue.ToString() == "TMPro")
            {
                if (tmproValueSlider != null)
                {
                    tmproValueSlider.text = Mathf.RoundToInt((_volume * 100)).ToString();
                }
            }
        }
    }


    void PlayOnAwake()
    {
        for (int i = 0; i < Instance.musics.Length; i++)
        {
            if (Instance.musics[i].playOnAwake)
            {
               Instance.Play(musics[i].name);
            }
        }
    }

    public void Play(string name)
    {
            Sound s = Array.Find(Instance.musics, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Play();
    }


    public void Stop(string name)
    {
            Sound s = Array.Find(Instance.musics, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Stop();
    }
    public void Pause(string name)
    {
            Sound s = Array.Find(Instance.musics, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Pause();
    }
    public void UnPause(string name)
    {
            Sound s = Array.Find(Instance.musics, sound => sound.name == name);
            if (s == null)
                return;
            s.source.UnPause();
    }

    private void Update()
    {
        Debug.Log(_volumeMaster);
    }
    public void SetVolume()
    {
        foreach (Sound s in musics)
        {
            if (_volume == 0)
            {

                s.source.volume = 0;
            }
            else
            {
                for (int i = 0; i < musics.Length; i++)
                {
                    if (musics[i].clip == s.source.clip)
                    {
                        s.source.volume = musics[i].volume * _volume * _volumeMaster;
                    }
                }
            }
        }

        Save();
    }
}

[CustomEditor(typeof(MusicManager))]

public class Music : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        MusicManager script = (MusicManager)target;

        EditorGUILayout.Space(10);

        script._hideInspector = EditorGUILayout.Toggle("Hide but keep inspector values", script._hideInspector);


        if (!script._hideInspector)
        {
            EditorGUILayout.Space(10);

            script._toolControlsSound = (MusicManager.Enum)EditorGUILayout.EnumPopup("Tool Controls Sound", script._toolControlsSound);

            if (script._toolControlsSound.ToString() == "Toggle")
            {
                #region UsingToggle
                script._toggleWhichChanges = EditorGUILayout.ObjectField("Toggle Controls Sound", script._toggleWhichChanges, typeof(Toggle), true) as Toggle;

                EditorGUILayout.Space(10);

                script._textShowSliderValue = (MusicManager.TextInsert)EditorGUILayout.EnumPopup("Is there a text shows slider value ?", script._textShowSliderValue);

                if (script._textShowSliderValue.ToString() == "Text")
                {
                    script.textValueSlider = EditorGUILayout.ObjectField("Insert Text Game Object", script.textValueSlider, typeof(Text), true) as Text;
                }
                else if (script._textShowSliderValue.ToString() == "TMPro")
                {
                    script.tmproValueSlider = EditorGUILayout.ObjectField("Insert TMPro Game Object", script.tmproValueSlider, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
                }

                if (script._textShowSliderValue.ToString() != "None")
                {
                    EditorGUILayout.Space(10);
                    script.textValueToggleOn = EditorGUILayout.TextField("The text when the Toggle is ON", script.textValueToggleOn);
                    script.textValueToggleOff = EditorGUILayout.TextField("The text when the Toggle is OFF", script.textValueToggleOff);
                }

                #endregion
            }


            else if (script._toolControlsSound.ToString() == "Slider")
            {
                #region UsingSlider
                script._sliderSound = EditorGUILayout.ObjectField("Slider Control Sound", script._sliderSound, typeof(Slider), true) as Slider;

                EditorGUILayout.Space(10);

                script._textShowSliderValue = (MusicManager.TextInsert)EditorGUILayout.EnumPopup("Is there a text shows slider value ?", script._textShowSliderValue);

                if (script._textShowSliderValue.ToString() == "Text")
                {
                    script.textValueSlider = EditorGUILayout.ObjectField("Insert Text Game Object", script.textValueSlider, typeof(Text), true) as Text;
                }
                else if (script._textShowSliderValue.ToString() == "TMPro")
                {
                    script.tmproValueSlider = EditorGUILayout.ObjectField("Insert TMPro Game Object", script.tmproValueSlider, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
                }

                #endregion
            }
        }
    }
}