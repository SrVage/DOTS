using System;
using System.Collections.Generic;
using Code.Abilities;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Code.Systems
{
    public class CollisionSystem:ComponentSystem
    {
        private EntityQuery _collisionQuery;
        private Collider[] _results = new Collider[10];
        private EntityManager _entityManager;

        
        protected override void OnCreate()
        {
            _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(),
                ComponentType.ReadOnly<Transform>());
            _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        protected override void OnUpdate()
        {
            Entities.With(_collisionQuery).ForEach((Entity entity, CollisionAbility ability, ref ActorColliderData data) =>
            {
                var gameObject = ability.gameObject;
                float3 position = gameObject.transform.position;
                Quaternion rotation = gameObject.transform.rotation;
                int size = 0;
                ability.Colliders = new List<Collider>();
                switch (data.ColliderType)
                {
                    case ColliderType.Sphere:
                        size = Physics.OverlapSphereNonAlloc(data.Center + position, data.Radius, _results);
                        break;
                    case ColliderType.Capsule:
                        var center = ((data.CapsuleStart + position) + (data.CapsuleEnd + position)) / 2;
                        var point1 = data.CapsuleStart + position;
                        var point2 = data.CapsuleEnd + position;
                        point1 = (float3)(rotation * (point1 - center)) + center;
                        point2 = (float3)(rotation * (point2 - center)) + center;
                        size = Physics.OverlapCapsuleNonAlloc(point1, point2, data.Radius, _results);
                        break;
                    case ColliderType.Box:
                        size = Physics.OverlapBoxNonAlloc(data.Center + position, data.HalfExtents, _results,
                            data.BoxOrientation * rotation);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                if (size > 0)
                {
                    foreach (var result in _results)
                    {
                        if (result != null)
                            ability.Colliders.Add(result);
                    }
                    ability.Execute();
                }
            });
        }
    }
}