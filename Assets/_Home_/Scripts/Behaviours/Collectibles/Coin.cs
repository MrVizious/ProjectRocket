using UnityEngine;

public class Coin : Collectible<Coin>
{
    public override Coin Collect()
    {
        Destroy(gameObject);
        return this;
    }
}
