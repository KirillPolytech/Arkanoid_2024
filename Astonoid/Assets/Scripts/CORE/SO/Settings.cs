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
        
        [Space(10)]
        [Header("Buffs")]
        [Range(0,30)]public float BuffDuration = 5f;
        [Range(1,10)]public float BuffCoeff = 2;
        [Range(4,100)]public int DropProbability = 5;
        [Range(1,25)]public int DropVelocity = 5;
        
        [Space(10)]
        [Header("Block")]
        [Range(1,30)]public int HitToDestructRange = 5;
        [Range(1,100)]public int DamageBoost = 100;
        
        [Space(10)]
        [Header("Sound")]
        [Range(0,30)]public float HitSoundDelay = 0.05f;
    }
}