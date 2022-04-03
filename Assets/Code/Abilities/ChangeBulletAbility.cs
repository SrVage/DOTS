using Code.Components.Interfaces;
using UnityEngine;

namespace Code.Abilities
{
    public class ChangeBulletAbility:MonoBehaviour, IChangeBullet
    {
        public void GetBullet()
        {
            if (gameObject.TryGetComponent<ShootAbility>(out var shoot))
            {
                shoot.JumpBullet = true;
            }
        }
    }
}