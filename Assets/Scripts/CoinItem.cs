using UnityEngine;

public class CoinItem : Item
{
    public int value = 10;

    public override void Use()
    {
        Debug.Log($"Added {value} gold to wallet.");
        // no actual use.. 
    }
}