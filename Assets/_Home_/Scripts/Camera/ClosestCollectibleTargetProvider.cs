using System.Collections.Generic;
using UnityEngine;

public class ClosestCollectibleTargetProvider : TargetProvider
{
    public List<Collectible> collectibles = new();

    private void Start()
    {
        RemoveDuplicatedCollectibles();
    }

    private void RemoveDuplicatedCollectibles()
    {
        collectibles = new List<Collectible>(new HashSet<Collectible>(collectibles));
    }

    public override Transform GetTarget(Transform origin)
    {
        if (collectibles.Count <= 0) return null;
        collectibles.RemoveAll(c => c == null);
        float closestSqrDistance = Mathf.Infinity;
        Collectible closestCollectible = null;

        foreach (Collectible collectible in collectibles)
        {
            float sqrDistaceToOrigin = (origin.position - collectible.transform.position).sqrMagnitude;
            if (sqrDistaceToOrigin <= closestSqrDistance)
            {
                closestCollectible = collectible;
                closestSqrDistance = sqrDistaceToOrigin;
            }
        }
        return closestCollectible.transform;
    }
}
