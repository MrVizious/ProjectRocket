using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UtilityMethods;

public class GravitySource : ExternalForceSource
{
    public float furthestGravity, closestGravity;
    public CircleCollider2D areaTrigger;
    public CircleCollider2D groundCollider;
    protected override void ApplyForceToEntities(float delta)
    {
        foreach (AffectedByExternalForce entity in affectedEntities)
        {
            float entityToSourceSqrDistance = Vector2.Distance(entity.transform.position, transform.position);
            float forceToApply = Math.Remap(value: entityToSourceSqrDistance,
                                            groundCollider.radius * transform.lossyScale.x,
                                            areaTrigger.radius * transform.lossyScale.x,
                                            closestGravity,
                                            furthestGravity,
                                            clamp: true) * delta;
            Vector2 forceVectorToApply = (transform.position - entity.transform.position).normalized * forceToApply;
            entity.ApplyForce(forceVectorToApply);
        }
    }

    private void FixedUpdate()
    {
        ApplyForceToEntities(Time.fixedDeltaTime);
    }
}