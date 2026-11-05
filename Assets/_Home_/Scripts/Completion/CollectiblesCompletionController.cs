using System.Collections.Generic;
using Sirenix.Serialization;
using UltEvents;
using UnityEngine;

public class CollectiblesCompletionController : CompletionController
{
    public List<Collectible> collectiblesToGet = new();
    private HashSet<Collectible> _collectiblesToGet;

    private void Start()
    {
        CacheCollectiblesList();
        foreach (Collectible collectible in _collectiblesToGet)
        {
            collectible.collected += OnCollectibleCollected;
        }
    }

    private void CacheCollectiblesList()
    {
        collectiblesToGet.RemoveAll(c => c == null);
        _collectiblesToGet = new(collectiblesToGet);
    }

    private void OnCollectibleCollected(Collectible collectedCollectible)
    {
        Debug.Log($"Collected collectible: {collectedCollectible}", this);
        collectedCollectible.collected -= OnCollectibleCollected;
        _collectiblesToGet.Remove(collectedCollectible);
        if (_collectiblesToGet.Count <= 0) Complete();
    }

}
