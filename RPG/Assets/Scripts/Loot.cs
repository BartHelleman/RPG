using UnityEngine;

public class Loot : Interactable
{
    //[SerializeField] private GameObject _itemSlot;
    //[SerializeField] private GameObject _panel;
    //[SerializeField] private Text _itemNameText;

    public Item[] ItemsToLoot;

    private Inventory _inventory;

    private void Start()
    {
        _inventory = Inventory.Instance;
    }

    public override void Interact()
    {
        base.Interact();
        //OpenLootPanel();
        AddAllToInventory();
        Debug.Log("Interacting with lootbag");
    }

    //private void OpenLootPanel()
    //{
    //    if (_itemSlot == null || _panel == null || _itemNameText == null)
    //    {
    //        Debug.LogError("Missing references in the loot panel");
    //        return;
    //    }

    //    _panel.SetActive(true);
    //}

    //private void CloseLootPanel()
    //{
    //    _panel.SetActive(false);
    //}

    // Temp method
    private void AddAllToInventory()
    {
        foreach (Item item in ItemsToLoot)
        {
            if (!_inventory.Add(item))
            {
                Debug.Log("Unable to add item to inventory");
                return;
            }

            Debug.Log("Added " + item.Name + " to inventory");
        }

        Destroy(gameObject);
    }
}
