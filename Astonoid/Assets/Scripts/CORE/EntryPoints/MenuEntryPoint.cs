using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuEntryPoint : MonoBehaviour
{
    private readonly List<Action> _cachedActions = new List<Action>();

    [SerializeField] private Button[] startButton;
    [SerializeField] private Button exitButton;

    [Space(10)] 
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    
    [Space(10)] 
    [SerializeField] private Slider mouseSens;
    
    [Space(10)] 
    [SerializeField] private Image[] levels;
    [SerializeField] private TextMeshProUGUI[] times;
    
    [Space(10)] 
    [SerializeField] private Button controlDeviceButton;
    [SerializeField] private Image controlDeviceImage;
    [SerializeField] private Sprite keyboardImage;
    [SerializeField] private Sprite mouseImage;

    private SceneLoader _sceneLoader;
    private VolumeSettings _volumeSettings;
    private MouseSensivity _mouseSensivity;
    private UserData _userData;


    [Inject]
    public void Construct(
        SceneLoader sceneLoader, 
        VolumeSettings volumeSettings,
        MouseSensivity mouseSensivity,
        UserData userData)
    {
        _sceneLoader = sceneLoader;
        _volumeSettings = volumeSettings;
        _mouseSensivity = mouseSensivity;
        _userData = userData;

        var temp = userData.GetLevelsData;

        for (int i = 0; i < levels.Length; i++)
        {
            if (temp.ElementAt(i).Equals(default(LevelData))) 
                continue;
            
            levels[i].color = Color.yellow;
            times[i].text = $"{temp[i].TimeData.Min:00}:{temp[i].TimeData.Sec:00}:{temp[i].TimeData.MilSec:000}";
        }
        
        for (int i = 0; i < startButton.Length; i++)
        {
            int ind = i;
            Action cached = () => _sceneLoader.LoadLevel(ind);
            startButton[i].onClick.AddListener(cached.Invoke);

            _cachedActions.Add(cached);
        }

        exitButton.onClick.AddListener(Application.Quit);

        musicSlider.onValueChanged.AddListener(_volumeSettings.ChangeMusicVol);
        soundSlider.onValueChanged.AddListener(_volumeSettings.ChangeSoundVol);
        
        mouseSens.onValueChanged.AddListener(x => _mouseSensivity.MouseSensivityValue = x);

        mouseSens.value = _mouseSensivity.MouseSensivityValue;
        musicSlider.value = volumeSettings.CurrentMusicVolume;
        soundSlider.value = volumeSettings.CurrentSoundVolume;
        
        controlDeviceButton.onClick.AddListener(ChangeControlDevice);
    }

    private void ChangeControlDevice()
    {
        switch (_userData.ControlType)
        {
            case ControlType.keyboard:
                _userData.ControlType = ControlType.mouse;
                controlDeviceImage.sprite = mouseImage;
                break;
            case ControlType.mouse:
                _userData.ControlType = ControlType.keyboard;
                controlDeviceImage.sprite = keyboardImage;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < startButton.Length; i++)
        {
            startButton[i].onClick.RemoveListener(_cachedActions.ElementAt(i).Invoke);
        }

        exitButton.onClick.RemoveListener(Application.Quit);

        musicSlider.onValueChanged.RemoveListener(_volumeSettings.ChangeMusicVol);
        soundSlider.onValueChanged.RemoveListener(_volumeSettings.ChangeSoundVol);
        
        mouseSens.onValueChanged.AddListener(x => _mouseSensivity.MouseSensivityValue = x);
    }
}