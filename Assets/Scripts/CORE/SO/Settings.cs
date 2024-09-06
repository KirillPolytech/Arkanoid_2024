using UnityEngine;

namespace Arkanoid.Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "GameSO/Settings", order = 1)]
    public class Settings : ScriptableObject
    {
        [Header("GameSettings")] 
        public int Health = 3;
        
        [Space(10)]
        [Header("PlatformSettings")]
        public float PlatformSpeed = 1f;
        
        [Space(10)]
        [Header("BallSettings")]
        public float BallStartForce = 5f;
        public float StartRange = 3;
        
        [Space(10)]
        [Header("Buffs")]
        [Range(0,30)]public float buffDuration = 5f;
        [Range(1,10)]public float coefficient = 2;
        [Range(1,10)]public int DropProbability = 5;
        
        [Space(10)]
        [Header("Block")]
        [Range(0,30)]public int HitToDestructRange = 5;
    }
}