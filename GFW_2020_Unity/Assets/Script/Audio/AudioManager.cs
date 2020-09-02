using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using TMPro;
using System.Collections;

public class AudioManager : MonoBehaviour
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
    public enum MasterVolume
    {
        Slider,
        Toggle
    }
    public enum TextInsertMaster
    {
        None,
        Text,
        TMPro
    }
    [HideInInspector] public Enum _toolControlsSound;
    [HideInInspector] public TextInsert _textShowSliderValue;
    [HideInInspector] public TextInsertMaster _textShowSliderValueMaster;
    [HideInInspector] public MasterVolume _toolControlsMasterVolume;
    AudioManagerEditor editor;
    public Sound[] sounds;
    float _volumeSFX = 1;
    float _volumeMaster = 1;
    int _hasAlreadyChangedSoundValue = 0;
    public static AudioManager Instance { get; private set; }

    [HideInInspector] public Toggle _toggleWhichChanges;
    [HideInInspector] public Slider _sliderSound;

    [HideInInspector] public Toggle _toggleWhichChangesMaster;
    [HideInInspector] public Slider _sliderMaster;

    [HideInInspector] public Text textValueSlider;
    [HideInInspector] public TextMeshProUGUI tmproValueSlider;


    [HideInInspector] public Text textValueMaster;
    [HideInInspector] public TextMeshProUGUI tmproValueMaster;

    [HideInInspector] public string textValueToggleOn, textValueToggleOff;
    [HideInInspector] public string textValueToggleOnMaster, textValueToggleOffMaster;


    [HideInInspector] public bool _hideInspector;
    [HideInInspector] public bool _usingMasterVolume;

    [HideInInspector] public MusicManager music;

    void Awake()
    {

        music = FindObjectOfType<MusicManager>();
        music.audioManager = GetComponent<AudioManager>();

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.playOnAwake = s.playOnAwake;
        }

        Load();
        switch (_toolControlsSound)
        {
            case Enum.None:
                _volumeSFX = 1;
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

        if (_usingMasterVolume)
        {
            switch (_toolControlsMasterVolume)
            {
                case MasterVolume.Slider:
                    ChangeMasterSlider();
                    break;
                case MasterVolume.Toggle:
                    ChangeMasterToggle();
                    break;
                default:
                    break;
            }
        }
        SetAudio();
        PlayOnAwake();
    }

    void Start()
    {
        if(_toolControlsSound.ToString() == "Slider")
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

        if (_usingMasterVolume)
        {
            if (_toolControlsMasterVolume.ToString() == "Slider")
            {
                if (_sliderMaster != null)
                {
                    _sliderMaster.onValueChanged.AddListener(delegate { ChangeMasterSlider(); }); ;
                }
            }
            else if (_toolControlsMasterVolume.ToString() == "Toggle")
            {
                if (_toggleWhichChangesMaster != null)
                {
                    _toggleWhichChangesMaster.onValueChanged.AddListener(delegate { ChangeMasterToggle(); }); ;
                }
            }
        }


    }


    void PlayOnAwake()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].playOnAwake)
            {
                Play(sounds[i].name);
            }
        }
    }

    public void ChangeMasterSlider()
    {

        if (_sliderMaster != null)
        {
            _volumeMaster = _sliderMaster.value;
        }
        AttributionMasterSlider();
        SetAudio();

    }

    public void ChangeMasterToggle()
    {
        if (_toggleWhichChangesMaster != null)
        {
            if (_toggleWhichChangesMaster.isOn)
            {
                _volumeMaster = 1;
            }
            else
            {
                _volumeMaster = 0;
            }
        }
        AttributionMasterToggle();
        SetAudio();

    }

    void AttributionMasterToggle()
    {
        if (_textShowSliderValueMaster.ToString() == "Text")
        {
            if (textValueMaster != null)
            {
                if (_toggleWhichChangesMaster.isOn)
                    textValueMaster.text = textValueToggleOnMaster;
                else
                    textValueMaster.text = textValueToggleOffMaster;
            }
        }
        else if (_textShowSliderValueMaster.ToString() == "TMPro")
        {
            if (tmproValueSlider != null)
            {
                if (_toggleWhichChangesMaster.isOn)
                    tmproValueMaster.text = textValueToggleOnMaster;
                else
                    tmproValueMaster.text = textValueToggleOffMaster;
            }
        }
    }

    void AttributionMasterSlider()
    {
        if (_toolControlsMasterVolume.ToString() == "Slider")
        {
            if (_textShowSliderValueMaster.ToString() == "Text")
            {
                if (textValueMaster != null)
                {
                    textValueMaster.text = Mathf.RoundToInt((_volumeMaster * 100)).ToString();
                }
            }
            else if (_textShowSliderValueMaster.ToString() == "TMPro")
            {
                if (tmproValueMaster != null)
                {
                    tmproValueMaster.text = Mathf.RoundToInt((_volumeMaster * 100)).ToString();
                }
            }
        }
    }

    public void ChangeSlider()
    {
        if (_sliderSound != null)
        {
            _volumeSFX = _sliderSound.value;
        }
        AttributionSlider();
        SetAudio();
    }
    public void ChangeToggle()
    {
        if(_toggleWhichChanges != null)
        {
            if (_toggleWhichChanges.isOn)
            {
                _volumeSFX = 1;
            }
            else
            {
                _volumeSFX = 0;
            }
        }
        AttributionToggle();
        SetAudio();
    }

    public void SetAudio()
    {
        foreach (Sound s in sounds)
        {
            if (_volumeSFX == 0)
            {
                s.source.volume = 0;
            }
            else
            {
                for (int i = 0; i < sounds.Length; i++)
                {
                    if (sounds[i].clip == s.source.clip)
                    {
                        s.source.volume = sounds[i].volume * _volumeSFX * _volumeMaster;
                    }
                }
            }
        }

        music._volumeMaster = _volumeMaster;
        StartCoroutine(Wait());
        Save();
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        music.SetVolume();

    }

    void Load()
    {
        _hasAlreadyChangedSoundValue = PlayerPrefs.GetInt("Has already changed Sound values");

        if (_hasAlreadyChangedSoundValue == 1)
        {
            _volumeSFX = PlayerPrefs.GetFloat("VolumeSFX");

            if(_toolControlsSound.ToString() == "Slider")
            {
                if(_sliderSound != null)
                {
                    _sliderSound.value = _volumeSFX;
                }
            }
            else if(_toolControlsSound.ToString() == "Toggle")
            {
                if (_toggleWhichChanges != null)
                {
                    if(_volumeSFX == 0)
                    {
                        _toggleWhichChanges.isOn = false;
                    }
                    else
                    {
                        _toggleWhichChanges.isOn = true;
                    }
                }
            }
            _volumeMaster = PlayerPrefs.GetFloat("VolumeMaster");

            if (_usingMasterVolume)
            {
                if (_toolControlsMasterVolume.ToString() == "Slider")
                {
                    if (_sliderMaster != null)
                    {
                        _sliderMaster.value = _volumeMaster;
                    }
                }
                else if (_toolControlsMasterVolume.ToString() == "Toggle")
                {
                    if (_toggleWhichChangesMaster != null)
                    {
                        if (_volumeMaster == 0)
                        {
                            _toggleWhichChangesMaster.isOn = false;
                        }
                        else
                        {
                            _toggleWhichChangesMaster.isOn = true;
                        }
                    }
                }
            }
        }
    }

    void Save()
    {
        PlayerPrefs.SetFloat("VolumeSFX", _volumeSFX);
        PlayerPrefs.SetFloat("VolumeMaster", _volumeMaster);
        _hasAlreadyChangedSoundValue = 1;
        PlayerPrefs.SetInt("Has already changed Sound values", _hasAlreadyChangedSoundValue);
    }

    void AttributionToggle()
    {
        if(_textShowSliderValue.ToString() == "Text")
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
                    textValueSlider.text = Mathf.RoundToInt((_volumeSFX * 100)).ToString();
                }
            }
            else if (_textShowSliderValue.ToString() == "TMPro")
            {
                if (tmproValueSlider != null)
                {
                    tmproValueSlider.text = Mathf.RoundToInt((_volumeSFX * 100)).ToString();
                }
            }
        }
    }
    public void Play(string name)
    {
        if(_volumeSFX >0 && _volumeMaster >0)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Play();
        }
    }
    public void Stop(string name)
    {
        if (_volumeSFX > 0 && _volumeMaster > 0)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Stop();
        }
    }
    public void Pause(string name)
    {
        if (_volumeSFX > 0 && _volumeMaster > 0)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;
            s.source.Pause();
        }
    }
    public void UnPause(string name)
    {
        if (_volumeSFX > 0 && _volumeMaster > 0)
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            if (s == null)
                return;
            s.source.UnPause();
        }
    }
}
[CustomEditor(typeof(AudioManager))]
public class AudioManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        AudioManager script = (AudioManager)target;

        EditorGUILayout.Space(10);


        script._hideInspector = EditorGUILayout.Toggle("Hide but keep inspector values", script._hideInspector);


        if (!script._hideInspector)
        {
            EditorGUILayout.Space(10);

            script._toolControlsSound = (AudioManager.Enum)EditorGUILayout.EnumPopup("Tool Controls Sound", script._toolControlsSound);

            if (script._toolControlsSound.ToString() == "Toggle")
            {
                #region UsingToggle
                script._toggleWhichChanges = EditorGUILayout.ObjectField("Toggle Controls Sound", script._toggleWhichChanges, typeof(Toggle), true) as Toggle;

                EditorGUILayout.Space(10);

                script._textShowSliderValue = (AudioManager.TextInsert)EditorGUILayout.EnumPopup("Is there a text shows slider value ?", script._textShowSliderValue);

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

                script._textShowSliderValue = (AudioManager.TextInsert)EditorGUILayout.EnumPopup("Is there a text shows slider value ?", script._textShowSliderValue);

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

            EditorGUILayout.Space(20);

            script._usingMasterVolume = EditorGUILayout.Toggle("Do you use Master Volume ?", script._usingMasterVolume);

            #region MasterVolume

            if (script._usingMasterVolume)
            {
                script._toolControlsMasterVolume = (AudioManager.MasterVolume)EditorGUILayout.EnumPopup("Tool Controls Master Volume", script._toolControlsMasterVolume);

                if(script._toolControlsMasterVolume.ToString() == "Slider")
                {
                    #region UsingSliderMaster
                    script._sliderMaster = EditorGUILayout.ObjectField("Slider Control Master", script._sliderMaster, typeof(Slider), true) as Slider;

                    EditorGUILayout.Space(10);

                    script._textShowSliderValueMaster = (AudioManager.TextInsertMaster)EditorGUILayout.EnumPopup("Is there a text shows slider value ?", script._textShowSliderValueMaster);

                    if (script._textShowSliderValueMaster.ToString() == "Text")
                    {
                        script.textValueMaster = EditorGUILayout.ObjectField("Insert Text Game Object", script.textValueMaster, typeof(Text), true) as Text;
                    }
                    else if (script._textShowSliderValueMaster.ToString() == "TMPro")
                    {
                        script.tmproValueMaster = EditorGUILayout.ObjectField("Insert TMPro Game Object", script.tmproValueMaster, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
                    }

                    #endregion
                }
                if (script._toolControlsMasterVolume.ToString() == "Toggle")
                {
                    #region UsingToggleMaster
                    script._toggleWhichChangesMaster = EditorGUILayout.ObjectField("Toggle Controls Master Volume", script._toggleWhichChangesMaster, typeof(Toggle), true) as Toggle;

                    EditorGUILayout.Space(10);

                    script._textShowSliderValueMaster = (AudioManager.TextInsertMaster)EditorGUILayout.EnumPopup("Is there a text shows slider value ?", script._textShowSliderValueMaster);

                    if (script._textShowSliderValueMaster.ToString() == "Text")
                    {
                        script.textValueMaster = EditorGUILayout.ObjectField("Insert Text Game Object", script.textValueMaster, typeof(Text), true) as Text;
                    }
                    else if (script._textShowSliderValueMaster.ToString() == "TMPro")
                    {
                        script.tmproValueMaster = EditorGUILayout.ObjectField("Insert TMPro Game Object", script.tmproValueMaster, typeof(TextMeshProUGUI), true) as TextMeshProUGUI;
                    }

                    if (script._textShowSliderValueMaster.ToString() != "None")
                    {
                        EditorGUILayout.Space(10);
                        script.textValueToggleOnMaster = EditorGUILayout.TextField("The text when the Toggle is ON", script.textValueToggleOnMaster);
                        script.textValueToggleOffMaster = EditorGUILayout.TextField("The text when the Toggle is OFF", script.textValueToggleOffMaster);
                    }

                    #endregion
                }

            }
            #endregion

        }
    }
}
