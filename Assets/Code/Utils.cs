using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Code
{
    public static class Utils
    {
        public static List<Collider> GetAllColliders(this GameObject gameObject)
        {
            return gameObject == null ? null : gameObject.GetComponents<Collider>().ToList();
        }

        public static void ToWorldSpaceBox(this BoxCollider collider, out float3 center,
            out float3 halfExtents, out quaternion orientation)
        {
            var transform = collider.transform;
            orientation = transform.rotation;
            center = transform.TransformPoint(collider.center);
            var lossyScale = transform.lossyScale;
            var scale = Abs(lossyScale);
            halfExtents = Vector3.Scale(scale, collider.size) * 0.5f;
        }

        public static void ToWorldSpaceCapsule(this CapsuleCollider collider, out float3 startPoint,
            out float3 endPoint, out float radius)
        {
            var transform = collider.transform;
            float3 center = transform.TransformPoint(collider.center);
            radius = 0f;
            float height = 0f;
            var lossyScale = Abs(transform.lossyScale);
            float3 dir = float3.zero;
            switch (collider.direction)
            {
                case 0:
                    radius = Mathf.Max(lossyScale.y, lossyScale.z) * collider.radius;
                    height = lossyScale.x * collider.height;
                    dir = transform.TransformDirection(Vector3.right);
                    break; 
                case 1:
                    radius = Mathf.Max(lossyScale.x, lossyScale.z) * collider.radius;
                    height = lossyScale.y * collider.height;
                    dir = transform.TransformDirection(Vector3.up);
                    break;
                case 2:
                    radius = Mathf.Max(lossyScale.x, lossyScale.y) * collider.radius;
                    height = lossyScale.z * collider.height;
                    dir = transform.TransformDirection(Vector3.right);
                    break;
            }

            if (height < radius * 2f) 
                dir = Vector3.zero;
            startPoint = center + dir * (height * 0.5f - radius);
            endPoint = center - dir * (height * 0.5f - radius);
        }

        public static void ToWorldSpaceSphere(this SphereCollider collider, out float3 center, out float radius)
        {
            var transform = collider.transform;
            center = transform.TransformPoint(collider.center);
            radius = collider.radius * Max(Abs(transform.lossyScale));
        }

        private static float3 Abs(float3 v)
        {
            return new float3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        private static float Max(float3 v)
        {
            return Mathf.Max(v.x, Mathf.Max(v.y, v.z));
        }
    }
}