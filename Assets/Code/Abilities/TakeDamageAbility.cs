using Code.Components.Character;
using Code.Components.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace Code.Abilities
{
    public class TakeDamageAbility:MonoBehaviour,ITakeDamage
    {
        private const float RechargeTime=0.1f;
        private Entity _entity;
        private EntityManager _entityManager;
        private float _time;

        public void Init(Entity entity)
        {
            _entity = entity;
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }
        
        public void Damage(float damage)
        {
            if (_time > Time.time)
                return;
            _time = Time.time + RechargeTime;
            if (damage < 0)
            {
                if (_entityManager.GetComponentData<HealthData>(_entity).Health + Mathf.Abs(damage) >
                    _entityManager.GetComponentData<HealthData>(_entity).MaxHealth)
                {
                    damage = _entityManager.GetComponentData<HealthData>(_entity).Health -
                             _entityManager.GetComponentData<HealthData>(_entity).MaxHealth;
                }
            }
            _entityManager.AddComponentData(_entity, new Damage()
            {
                Value = damage
            });
        }
    }
}