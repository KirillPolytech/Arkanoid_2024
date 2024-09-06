using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Level : MonoBehaviour
{
    [SerializeField] private Button continueButton;
    [SerializeField] private Button[] exitButton;
    
    [SerializeField] private Button restartButton;

    private SceneLoader _sceneLoader;
    private LevelStateMachine _levelStateMachine;
    
    [Inject]
    public void Construct(
        SceneLoader sceneLoader, 
        LevelStateMachine levelStateMachine,
        LoseTrigger loseTrigger)
    {
        _sceneLoader = sceneLoader;
        _levelStateMachine = levelStateMachine;
        
        continueButton.onClick.AddListener(_levelStateMachine.SetState<BeginState>);
        restartButton.onClick.AddListener(_levelStateMachine.SetState<InitialState>);

        foreach (var button in exitButton)
        {
            button.onClick.AddListener(_levelStateMachine.SetState<BeginState>);
            button.onClick.AddListener(_sceneLoader.LoadMenu);
        }
    }

    private void OnDisable()
    {
        continueButton.onClick.RemoveListener(_levelStateMachine.SetState<BeginState>);
        foreach (var button in exitButton)
        {
            button.onClick.RemoveListener(_levelStateMachine.SetState<BeginState>);
            button.onClick.RemoveListener(_sceneLoader.LoadMenu);
        }
    }
}
