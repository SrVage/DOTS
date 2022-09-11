using Code.Components.Character;
using Code.UI;
using Unity.Entities;
using Object = UnityEngine.Object;

namespace Code.Systems
{
    public class ArmourTimerSystem:ComponentSystem
    {
        private EntityQuery _timerQuery;
        private EntityManager _entityManager;
        private ViewModel _viewModel;

        protected override void OnCreate()
        {
            _viewModel = Object.FindObjectOfType<ViewModel>();
            _timerQuery = GetEntityQuery(ComponentType.ReadOnly<ShieldComponent>());
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        protected override void OnUpdate()
        {
            Entities.With(_timerQuery).ForEach((Entity entity, ShieldComponent timer) =>
            {
                ref var time = ref timer.Timer;
                time-= Time.DeltaTime;
                ShowTime(time);
                if (time <= 0)
                {
                    HideTime();
                    _entityManager.RemoveComponent<ShieldComponent>(entity);
                }
            });
        }
        
        private void ShowTime(float time)
        {
            _viewModel.Time = $"Shield time: {time:0.00}";
        }

        private void HideTime()
        {
            _viewModel.Time = "";
        }
    }
}