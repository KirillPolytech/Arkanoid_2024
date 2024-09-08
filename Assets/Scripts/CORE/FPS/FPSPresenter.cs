using UnityEngine;
using Zenject;

public class FPSPresenter : ITickable, IInitializable
{
    private readonly FPSSO _fpsso;
    private readonly FPSView _fpsView;
    
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
#if UNITY_EDITOR
    public void Tick()
    {
        Application.targetFrameRate = _fpsso.TargetFPS;
    }
    #endif
}
