using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;
    
    [Space(10)]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;
    
    private SceneLoader _sceneLoader;
    private VolumeSettings _volumeSettings;

    [Inject]
    public void Construct(SceneLoader sceneLoader, VolumeSettings volumeSettings)
    {
        _sceneLoader = sceneLoader;
        _volumeSettings = volumeSettings;
        
        startButton.onClick.AddListener(_sceneLoader.LoadLevel);
        exitButton.onClick.AddListener(Application.Quit);
        
        musicSlider.onValueChanged.AddListener(_volumeSettings.ChangeMusicVol);
        soundSlider.onValueChanged.AddListener(_volumeSettings.ChangeSoundVol);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(_sceneLoader.LoadMenu);
        exitButton.onClick.RemoveListener(Application.Quit);
        
        musicSlider.onValueChanged.RemoveListener(_volumeSettings.ChangeMusicVol);
        soundSlider.onValueChanged.RemoveListener(_volumeSettings.ChangeSoundVol);
    }
}
