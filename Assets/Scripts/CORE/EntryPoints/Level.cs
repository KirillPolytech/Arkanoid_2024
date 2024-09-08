using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Level : MonoBehaviour
{
    [SerializeField] private Button nextButton;
    [SerializeField] private Button continueButton;
    [SerializeField] private Button[] exitButton;

    [SerializeField] private Button restartButton;

    private SceneLoader _sceneLoader;
    private LevelStateMachine _levelStateMachine;
    private Action _cached;

    [Inject]
    public void Construct(
        SceneLoader sceneLoader,
        LevelStateMachine levelStateMachine,
        LoseTrigger loseTrigger)
    {
        _sceneLoader = sceneLoader;
        _levelStateMachine = levelStateMachine;

        _cached = () => sceneLoader.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);

        nextButton.onClick.AddListener(_cached.Invoke);
        continueButton.onClick.AddListener(_levelStateMachine.SetState<ContinueState>);
        restartButton.onClick.AddListener(_levelStateMachine.SetState<InitialState>);

        foreach (var button in exitButton)
        {
            button.onClick.AddListener(_levelStateMachine.SetState<BeginState>);
            button.onClick.AddListener(_sceneLoader.LoadMenu);
        }
    }

    private void OnDisable()
    {
        nextButton.onClick.RemoveListener(_cached.Invoke);
        continueButton.onClick.RemoveListener(_levelStateMachine.SetState<BeginState>);
        restartButton.onClick.RemoveListener(_levelStateMachine.SetState<InitialState>);

        foreach (var button in exitButton)
        {
            button.onClick.RemoveListener(_levelStateMachine.SetState<BeginState>);
            button.onClick.RemoveListener(_sceneLoader.LoadMenu);
        }
    }
}