using System;
using Code.Abilities;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Code.Behaviours
{
    public class TrapMoveBehavior:MonoBehaviour
    {
        [SerializeField] private Transform[] _points;
        [SerializeField] private float _moveBackSpeed;
        [SerializeField] private float _attackSpeed;
        private DamageAbility _ability;
        
        private void Start()
        {
            if (TryGetComponent<DamageAbility>(out var ability))
            {
                _ability = ability;
            }
            transform.position = _points[1].position;
            MoveBack();
        }

        private void MoveBack()
        {
            Debug.Log("moveBack");
            DOTween.Sequence().Append(transform.DOMove(_points[0].position, 1 / _moveBackSpeed))
                .AppendInterval(Random.Range(0.2f, 1f)).OnComplete(()=>_ability.ChangeDamage(10))
                .Append(transform.DOMove(_points[1].position, 1 / _attackSpeed))
                .AppendInterval(Random.Range(0.7f, 2f)).OnComplete(()=>
                {
                    _ability.ChangeDamage(0);
                    MoveBack();
                });
        }
    }
}