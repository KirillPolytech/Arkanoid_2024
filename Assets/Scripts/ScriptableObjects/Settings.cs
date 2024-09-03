using UnityEngine;

namespace Arkanoid.Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "GameSO/Settings", order = 1)]
    public class Settings : ScriptableObject
    {
        [Header("PlatformSettings")]
        public float PlatformSpeed = 1f;
        
        [Space(10)]
        [Header("BallSettings")]
        public float BallStartForce = 5f;
        public float StartRange = 3;
    }
}