using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private InventorySlot _inventorySlot;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            _inventorySlot.UseItem();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            _inventorySlot.DropItem();
        }
    }
}
