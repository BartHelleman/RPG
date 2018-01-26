using UnityEngine;

public enum EquipmentSlot { MainHand, OffHand, Head, Chest, Hands, Legs, Feet }

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot EquipSlot;

    // All stat modifiers
    public int HpModifier;
    public int DefenceModifier;
    public int AttackPowerModifier;
    public int CritRateModifier;
    public int CritDamageModifier;

    public override void Use()
    {
        base.Use();
        RemoveFromInventory();
    }
}
