using System;
using System.Collections;
using UnityEngine;

namespace Code.Behaviours
{
    public class FreezeBehaviour:Behaviour
    {
        private const int FreezeTime = 1;
        private static readonly int TimeParameter = Shader.PropertyToID("_TimeParameter");
        public bool IsFreeze = false;
        public bool IsStart = false;
        [SerializeField] private MeshRenderer _meshRenderer;

        public override float Evaluate()
        {
            if (IsFreeze)
                return 0;
            return float.MaxValue;
        }

        public override void Behave()
        {
            if (!IsStart)
            {
                StartCoroutine(nameof(StartFreeze));
                IsStart = true;
            }
        }

        private IEnumerator StartFreeze()
        {
            float time = 0;
            while (time<1)
            {
                _meshRenderer.material.SetFloat(TimeParameter, time);
                time += 0.01f;
                yield return null;
            }
            yield return new WaitForSeconds(FreezeTime);
            while (time>0)
            {
                _meshRenderer.material.SetFloat(TimeParameter, time);
                time -= 0.01f;
                yield return null;
            }
            IsFreeze = false;
            yield return null;
            IsStart = false;
        }
    }
}