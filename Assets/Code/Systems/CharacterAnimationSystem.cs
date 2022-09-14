using Code.Components.Character;
using Code.Network;
using Unity.Entities;
using UnityEngine;

namespace Code.Systems
{
    public class CharacterAnimationSystem:ComponentSystem
    {
        private const int SpeedCoeff = 50;
        private const int Capacity = 10;
        private EntityQuery _animationQuerry;
        private Vector3 _lastPosition = Vector3.zero;
        private float[] _speed = new float[Capacity];

        protected override void OnCreate()
        {
            _animationQuerry = GetEntityQuery(ComponentType.ReadOnly<UserInputData>(),
                ComponentType.ReadOnly<AnimationData>(),
                ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<LocalPlayerTag>());
        }
        protected override void OnUpdate()
        {
            Entities.With(_animationQuerry).ForEach((Entity entity, UserInputData userInputData, 
                ref AnimationData animatorData, ref InputData inputData) =>
            {
                Vector3 currentSpeed = (userInputData.transform.position - _lastPosition);
                _speed[Capacity-1] = currentSpeed.magnitude;
                float speed = 0;
                for (int i = 0; i < _speed.Length; i++)
                {
                    speed += _speed[i];
                    if (i<_speed.Length-1)
                        _speed[i] = _speed[i + 1];
                }
                float smoothSpeed = (speed / Capacity)*SpeedCoeff;
                _lastPosition = userInputData.transform.position;
                    userInputData.Animator.SetFloat(animatorData.MoveHash,smoothSpeed);
                    userInputData.Animator.speed = smoothSpeed<0.5f?1:smoothSpeed*2;
                    if (inputData.Shoot > 0)
                    {
                        userInputData.Animator.SetTrigger(animatorData.AttackHash);
                        userInputData.SynchronizedParameters.Attack();
                    }
            });
        }
    }
}