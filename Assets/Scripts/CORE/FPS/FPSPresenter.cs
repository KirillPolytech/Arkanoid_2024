using UnityEngine;
using Zenject;

public class FPSPresenter : ITickable, IInitializable
{
    private readonly FPSSO _fpsso;
    private readonly FPSView _fpsView;

    private float _fps;

    [Inject]
    public FPSPresenter(FPSSO fpsso, FPSView fpsView)
    {
        _fpsso = fpsso;
        _fpsView = fpsView;
    }

    public void Initialize()
    {
        Application.targetFrameRate = _fpsso.TargetFPS;
    }

    public void Tick()
    {
#if UNITY_EDITOR
        Application.targetFrameRate = _fpsso.TargetFPS;
#endif

        _fps = (int) Mathf.Clamp(1 / Time.deltaTime, 0, 999);
        _fpsView.UpdateFPSValue(_fps);
    }
}