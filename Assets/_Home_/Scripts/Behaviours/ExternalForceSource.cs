using System.Collections.Generic;
using ExtensionMethods;
using UnityEngine;

public abstract class ExternalForceSource : MonoBehaviour
{
    protected HashSet<AffectedByExternalForce> affectedEntities = new();
    protected abstract void ApplyForceToEntities(float delta);
    protected void OnTriggerEnter2D(Collider2D other)
    {
        AffectedByExternalForce entity = other.GetComponent<AffectedByExternalForce>();
        if (entity == null) return;
        affectedEntities.Add(entity);
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        AffectedByExternalForce entity = other.GetComponent<AffectedByExternalForce>();
        if (entity == null) return;
        affectedEntities.Remove(entity);
    }
}
