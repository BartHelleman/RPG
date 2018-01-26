using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;
    [SerializeField] private Transform _itemsParent;

    private Inventory _inventory;
    private InventorySlot[] _inventorySlots;

    private void Start()
    {
        _inventory = Inventory.Instance;
        _inventory.OnItemChangedCallback += UpdateUI;

        _inventorySlots = _itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Inventory"))
            _inventoryUI.SetActive(!_inventoryUI.activeSelf);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < _inventory.Items.Count)
                _inventorySlots[i].AddItem(_inventory.Items[i]);
            else
                _inventorySlots[i].ClearSlot();
        }
    }
}
