using System;
using Code.Components.Character;
using Code.UI;
using Unity.Entities;
using Object = UnityEngine.Object;

namespace Code.Systems
{
    internal sealed class ShowHealthOnUISystem:ComponentSystem
    {
        private EntityQuery _healthQuery;
        private ViewModel _viewModel;
        private float _previousHealth;
        
        protected override void OnCreate()
        {
            _viewModel = Object.FindObjectOfType<ViewModel>();
            _healthQuery = GetEntityQuery(ComponentType.ReadOnly<HealthData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_healthQuery).ForEach((Entity entity, ref HealthData healthData) =>
            {
                ref var health = ref healthData.Health;
                if (Math.Abs(health - _previousHealth) > 0.1f)
                {
                    ShowHealth(health);
                    _previousHealth = health;
                }
            });
        }

        private void ShowHealth(float health)
        {
            _viewModel.Health = $"Health: {health}";
        }
    }
}