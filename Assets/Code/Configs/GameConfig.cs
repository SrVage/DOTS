using System;
using System.IO;
using UnityEngine;

namespace Code.Configs
{
    [Serializable]
    public class GameConfig: IGameCfg
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

        /*private void Awake()
        {
            Debug.Log("save");
            string stringData = JsonUtility.ToJson(this);
            File.WriteAllText(Application.dataPath+"/PlayerConfig.txt", stringData);
        }*/
    }
}