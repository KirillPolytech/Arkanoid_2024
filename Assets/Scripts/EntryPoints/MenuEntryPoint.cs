using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Button startButton;
    
    [SerializeField] private Button exitButton;
    
    private SceneLoader _sceneLoader;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
        
        startButton.onClick.AddListener(_sceneLoader.LoadLevel);
        
        exitButton.onClick.AddListener(Application.Quit);
    }

    private void OnDisable()
    {
        startButton.onClick.RemoveListener(_sceneLoader.LoadMenu);
        exitButton.onClick.RemoveListener(Application.Quit);
    }
}
