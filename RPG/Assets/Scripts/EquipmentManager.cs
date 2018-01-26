using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager Instance;

    private Inventory _inventory;
    private Equipment[] _currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged OnEquipmentChangedCallback;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("More than one instance of EquipmentManager found");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        _inventory = Inventory.Instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        _currentEquipment = new Equipment[numSlots];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.EquipSlot;
        Equipment oldItem = Unequip(slotIndex);

        _currentEquipment[slotIndex] = newItem;

        if (OnEquipmentChangedCallback != null)
            OnEquipmentChangedCallback.Invoke(newItem, oldItem);
    }

    public Equipment Unequip(int slotIndex)
    {
        if (_currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = _currentEquipment[slotIndex];
            if (!_inventory.Add(oldItem))
            {
                Debug.Log("Unable to add item to inventory");
                return null;
            }

            _currentEquipment[slotIndex] = null;

            if (OnEquipmentChangedCallback != null)
                OnEquipmentChangedCallback.Invoke(null, oldItem);
            
            return oldItem;
        }

        return null;
    }

    public void UnequipAll()
    {
        Debug.Log("Unequipping all items");
        for (int i = 0; i < _currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
}
