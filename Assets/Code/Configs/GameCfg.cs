using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(menuName = "Configs/Game Config")]
    public class GameCfg:ScriptableObject, IGameCfg
    {
        [Header("General")]
        public float playerHealth;
        public float playerSpeed;
        
        [Header("Shoot")] 
        public float shootDelay;

        [Header("Jerk")] 
        public float jerkDistance;
        public float jerkSpeed;
        public float jerkRechargeTime;
        public float PlayerHealth => playerHealth;
        public float PlayerSpeed => playerSpeed;
        public float ShootDelay => shootDelay;
        public float JerkDistance => jerkDistance;
        public float JerkSpeed => jerkSpeed;
        public float JerkRechargeTime => jerkRechargeTime;
    }
}