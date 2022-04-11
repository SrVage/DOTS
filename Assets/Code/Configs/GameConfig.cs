using UnityEngine;

namespace Code.Configs
{
    public class GameConfig:MonoBehaviour, IGameCfg
    {
        [Header("General")]
        public float playerHealth = 100;
        public float playerSpeed = 10;
        
        [Header("Shoot")] 
        public float shootDelay = 2;

        [Header("Jerk")] 
        public float jerkDistance = 4;
        public float jerkSpeed = 30;
        public float jerkRechargeTime = 5;
        
        public float PlayerHealth => playerHealth;
        public float PlayerSpeed => playerSpeed;
        public float ShootDelay => shootDelay;
        public float JerkDistance => jerkDistance;
        public float JerkSpeed => jerkSpeed;
        public float JerkRechargeTime => jerkRechargeTime;
    }
}