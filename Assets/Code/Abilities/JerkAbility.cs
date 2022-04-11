using System.Collections;
using Code.Components.Interfaces;
using UnityEngine;
using Zenject;

namespace Code.Abilities
{
    public class JerkAbility:MonoBehaviour, IAbility
    {
        private const float Distance = 0.1f;
        [SerializeField] private float _distance;
        [SerializeField] private float _speed;
        [SerializeField] private float _rechargeTime;
        private Vector3 _endPoint;
        private float _time;
        [Inject]
        public void Init([Inject(Id = "jerkDistance")] float distance,
            [Inject(Id = "jerkSpeed")] float speed, 
            [Inject(Id = "jerkRechargeTime")] float time)
        {
            _distance = distance;
            _speed = speed;
            _rechargeTime = time;
        }
        public void Execute()
        {
            if (_time > Time.time)
                return;
            _time = Time.time + _rechargeTime;
            _endPoint = transform.position + transform.forward * _distance;
            StartCoroutine(nameof(Jerk));
        }

        private IEnumerator Jerk()
        {
            while (Vector3.SqrMagnitude(_endPoint - transform.position)>Distance)
            {
                transform.position += transform.forward * _speed * Time.deltaTime;
                yield return null;
            }
        }
    }
}