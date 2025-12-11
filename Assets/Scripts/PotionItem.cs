using UnityEngine;

public class PotionItem : Item
{
    public int healAmount = 50;

   
    public override void Use()
    {
        Debug.Log($"Drank {ItemName}. Healed {healAmount} HP!");
        // no actual logic.. duh 
    }
}