using System.Threading.Tasks;
using Code.Components.Character;
using Code.Utils;
using Unity.Entities;
using UnityEngine;

namespace Code.Systems
{
    public class LoadPlayerData:ComponentSystem
    {
        private EntityQuery _playerDataQuery;

        protected override void OnCreate()
        {
            _playerDataQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>()
                ,ComponentType.ReadOnly<HealthData>());
        }

        protected override async void OnStartRunning()
        {
            Debug.Log("StartLoading");
            await LoadData();
            Debug.Log("LoadComplete");
        }

        async Task LoadData()
        {
            Entities.With(_playerDataQuery).ForEach(
                (Transform transform, ref InputData inputData, ref HealthData healthData) =>
                {
                    var playerData = SavedData.Load();
                    if (playerData != null)
                    {
                        transform.position = playerData.Position;
                        ref var hp = ref healthData.Health;
                        hp = playerData.Health;
                    }
                });
            await Task.Delay(3000);
        }

        protected override void OnUpdate()
        {
            Entities.With(_playerDataQuery).ForEach((Transform transform, ref InputData inputData, ref HealthData healthData) =>
                {
                    if (inputData.Save > 0)
                    {
                        PlayerData playerData = new PlayerData()
                        {
                            Position = transform.position,
                            Health = healthData.Health
                        };
                        SavedData.Save(playerData);
                        Debug.Log(playerData);
                    }

                    if (inputData.Load > 0)
                    {
                        var playerData = SavedData.Load();
                        if (playerData != null)
                        {
                            transform.position = playerData.Position;
                            ref var hp = ref healthData.Health;
                            hp = playerData.Health;
                        }
                    }
                });
            }
        }
    }