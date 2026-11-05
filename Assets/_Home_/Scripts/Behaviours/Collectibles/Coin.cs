using UnityEngine;

public class Coin : Collectible
{
    public override Collectible Collect()
    {
        base.Collect();
        Destroy(gameObject);
        return this;
    }
}
