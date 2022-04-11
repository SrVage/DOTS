using UnityEngine;

namespace Code.Behaviours
{
    public abstract class Behaviour:MonoBehaviour
    {
        public virtual float Evaluate()
        {
            return 0;
        }

        public virtual void Behave()
        {
            
        }
    }
}