using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [field: SerializeField] public string ItemName { get; protected set; }
    [field: SerializeField] public int ID { get; protected set; }
    [field: SerializeField] public Sprite Icon { get; protected set; }

    public int stackSize = 1;

    public abstract void Use();

    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

}