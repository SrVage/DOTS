using System.IO;
using System.Threading.Tasks;
using Code.Components.Interfaces;
using Code.Configs;
using Code.Utils;
using Unity.Entities;
using UnityEngine;
using Zenject;

namespace Code.Components.Character
{
    public class UserInputData:MonoBehaviour, IConvertGameObjectToEntity
    {
        public MonoBehaviour ShootAbility => _shootAbility;
        public MonoBehaviour JerkAbility => _jerkAbility;
        private GameConfig _gameConfig;
        [SerializeField] private float  _speed,
                                        _health;

        [SerializeField] private MonoBehaviour _shootAbility,
                                                _jerkAbility,
                                                _takeDamage,
                                                _changeBullet;

        public Animator Animator;

        [Inject]
        public void Init([Inject(Id = "health")] float health, [Inject(Id = "speed")] float speed)
        {
            _health = health;
            _speed = speed;
        }

        private async Task<GameConfig> LoadConfig()
        {
            var stringData = File.ReadAllText(Application.dataPath + "/PlayerConfig.txt");
            return JsonUtility.FromJson<GameConfig>(stringData);
        }
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            //var task  = LoadConfig();
            //await task;
            //_gameConfig = task.Result;
            dstManager.AddComponentData(entity, new InputData());
            dstManager.AddComponentData(entity, new MoveData()
            {
                Speed = _speed
            });

            if (_health > 0)
            {
                dstManager.AddComponentData(entity, new HealthData()
                {
                    Health = _health,
                    MaxHealth = _health
                });
            }
            
            if (_shootAbility != null && _shootAbility is IAbility ability)
            {
                dstManager.AddComponentData(entity, new ShootData());
            }

            if (_jerkAbility != null && _jerkAbility is IAbility jerkAbility)
            {
                dstManager.AddComponentData(entity, new JerkData());
            }

            if (Animator != null)
            {
                dstManager.AddComponentData(entity, new AnimationData()
                {
                    MoveHash = Animator.StringToHash("Speed"),
                    AttackHash = UnityEngine.Animator.StringToHash("Attack")
                });
            }

            if (_takeDamage != null && _takeDamage is ITakeDamage takeDamage)
            {
                takeDamage.Init(entity);
            }
        }
    }
}