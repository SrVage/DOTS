using System;
using Code.Components.Character;
using Code.UI;
using Unity.Entities;
using UnityEngine;
using UnityWeld.Binding;
using Object = UnityEngine.Object;

namespace Code.Systems
{
    internal sealed class ShowHealthOnUISystem:ComponentSystem
    {
        private EntityQuery _healthQuery;
        private EntityQuery _healthQueryNetwork;
        private ViewModel _viewModel;
        private ViewModel _viewModelNetwork;
        private float _previousHealth=0;
        private float _previousNetworkHealth=0;
        
        protected override void OnCreate()
        {
            _viewModel = Object.FindObjectOfType<ViewModel>();
            _healthQuery = GetEntityQuery(ComponentType.ReadOnly<HealthData>(),
                ComponentType.ReadOnly<LocalPlayerTag>());
            _healthQueryNetwork = GetEntityQuery(ComponentType.ReadOnly<HealthData>(),
                ComponentType.Exclude<LocalPlayerTag>(),
                ComponentType.ReadOnly<Transform>());
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
            Entities.With(_healthQueryNetwork).ForEach((Entity entity, ref HealthData healthData, Transform transform) =>
            {
                if (_viewModelNetwork != null)
                {
                    _viewModelNetwork.transform.LookAt(Camera.main.transform);
                }
                ref var health = ref healthData.Health;
                ref var maxHealth = ref healthData.MaxHealth;
                if (Math.Abs(health - _previousNetworkHealth) > 0.1f)
                {
                    if (_viewModelNetwork == null)
                    {
                        _viewModelNetwork = transform.GetComponentInChildren<ViewModel>();
                        transform.GetComponentInChildren<OneWayPropertyBinding>().Disconnect();
                        transform.GetComponentInChildren<OneWayPropertyBinding>().Connect();
                    }
                    ShowHealthNetwork(health, maxHealth);
                    _previousNetworkHealth = health;
                }
            });
        }

        private void ShowHealth(float health)
        {
            _viewModel.Health = $"Health: {health}";
        }
        
        private void ShowHealthNetwork(float health, float maxHealth)
        {
            _viewModelNetwork.EnemyHealth = $"{health}/{maxHealth}";
        }
    }
}