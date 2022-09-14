using Code.Components.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Code.Network
{
    public class SynchronizedParameters:MonoBehaviourPunCallbacks, IPunObservable
    {
        [SerializeField] private float _damage;
        [SerializeField] private bool _attack;
        [SerializeField] private Animator _animator;
        [SerializeField] private MonoBehaviour _takeDamageMonoBehaviour;
        private ITakeDamage _takeDamage;
        
        private void Awake()
        {
            if (_takeDamageMonoBehaviour is ITakeDamage takeDamage) 
                _takeDamage = takeDamage;
        }

        public void SetDamage(float damage) => 
            _damage = damage;

        public void Attack() => 
            _attack = true;

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_damage);
                stream.SendNext(_attack);
                _damage = 0;
                _attack = false;
            }
            else
            {
                _damage = (float)stream.ReceiveNext();
                _attack = (bool) stream.ReceiveNext();
                SetEntityHealth();
                SetAnimatorAttack();
            }
        }

        private void SetEntityHealth() => 
            _takeDamage.Damage(_damage);

        private void SetAnimatorAttack()
        {
            if (_attack) 
                _animator.SetTrigger("Attack");
        }
    }
}