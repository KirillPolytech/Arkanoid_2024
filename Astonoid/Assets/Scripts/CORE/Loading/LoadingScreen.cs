using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Slider loadingSlider;

    private SceneLoader _sceneLoader;
    private Action<float> _cachedProgressAction;
    private Action _cachedBeingLoading;
    private Action<string> _cachedEndLoading;

    [Inject]
    public void Construct(SceneLoader sceneLoader)
    {
        canvas.enabled = false;
        
        _sceneLoader = sceneLoader;

        _cachedProgressAction = x => loadingSlider.value = x;
        _cachedBeingLoading += () => canvas.enabled = true;
        _cachedEndLoading += (_) => canvas.enabled = false;

        _sceneLoader.OnBeginLoading += _cachedBeingLoading.Invoke;
        _sceneLoader.OnEndLoading += _cachedEndLoading.Invoke;
        _sceneLoader.OnLoadProgress += _cachedProgressAction.Invoke;
    }

    private void OnDisable()
    {
        _sceneLoader.OnBeginLoading -= _cachedBeingLoading.Invoke;
        _sceneLoader.OnEndLoading -= _cachedEndLoading.Invoke;
        _sceneLoader.OnLoadProgress -= _cachedProgressAction.Invoke;
    }
}