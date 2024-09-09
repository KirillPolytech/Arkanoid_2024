using System;
using System.Collections.Generic;
using System.Linq;
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

    private SceneLoader _sceneLoader;
    private VolumeSettings _volumeSettings;
    private MouseSensivity _mouseSensivity;


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