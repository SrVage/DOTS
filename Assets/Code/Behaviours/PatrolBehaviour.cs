using UnityEngine;

namespace Code.Behaviours
{
    public class PatrolBehaviour:Behaviour
    {
        public override float Evaluate()
        {
            return 100;
        }

        public override void Behave()
        {
            Debug.Log("Patrol");
        }
    }
}