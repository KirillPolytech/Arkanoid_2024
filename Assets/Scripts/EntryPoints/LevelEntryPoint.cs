using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LevelEntryPoint : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button exitButton;

    private SceneLoader _sceneLoader;
    private LevelStateMachine _levelStateMachine;
    
    [Inject]
    public void Construct(SceneLoader sceneLoader, LevelStateMachine levelStateMachine)
    {
        _sceneLoader = sceneLoader;
        _levelStateMachine = levelStateMachine;
        
        continueButton.onClick.AddListener(_levelStateMachine.SetState<PlayState>);
        
        exitButton.onClick.AddListener(_levelStateMachine.SetState<PlayState>);
        exitButton.onClick.AddListener(_sceneLoader.LoadMenu);
        
        levelStateMachine.SetState<PlayState>();
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(_levelStateMachine.SetState<PlayState>);
        
        exitButton.onClick.RemoveListener(_levelStateMachine.SetState<PlayState>);
        exitButton.onClick.RemoveListener(_sceneLoader.LoadMenu);
    }
}
