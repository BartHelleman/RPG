using UnityEngine;

public class Pickupable : Interactable
{
    public Item Item;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    private void PickUp()
    {
        Debug.Log("Picking up " + Item.Name);

        if (Inventory.Instance.Add(Item))
            Destroy(gameObject);
    }
}
