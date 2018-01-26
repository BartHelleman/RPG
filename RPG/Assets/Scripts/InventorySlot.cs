using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private GameObject _button;
    [SerializeField] private GameObject _basePickupable;

    private Item _item;

    public void AddItem(Item newItem)
    {
        _item = newItem;
        _button.SetActive(true);
        _icon.sprite = _item.Icon;
        _icon.enabled = true;
    }

    public void ClearSlot()
    {
        _item = null;
        _button.SetActive(false);
        _icon.sprite = null;
        _icon.enabled = false;
    }

    public void UseItem()
    {
        Debug.Log("Using item");
        if (_item != null)
        {
            _item.Use();
        }
    }

    public void DropItem()
    {
        Debug.Log("Dropping " + _item.Name);
        if (_item != null)
        {
            Transform player = PlayerManager.Instance.Player.transform;
            Vector3 dropPosition = player.position + (player.forward * 2);
            GameObject droppedItem = Instantiate(_basePickupable, dropPosition, player.rotation);
            droppedItem.transform.name = _item.Name;
            Pickupable pickupable = droppedItem.GetComponent<Pickupable>();
            pickupable.Item = _item;

            _item.RemoveFromInventory();
        }
    }
}
