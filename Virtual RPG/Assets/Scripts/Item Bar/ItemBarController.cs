using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarController : MonoBehaviour
{
    [SerializeField]
    private List<ItemBarSlot> itemBarSlots;

    [SerializeField]
    private AttributesEffectsController attributesEffectsController;

    [SerializeField]
    private InventoryController inventoryController;

    
    [SerializeField]
    private EquipmentSlot weaponEquipmentSlot;

    [SerializeField]
    private EquipmentSlot weaponProjectileEquipmentSlot;

    [SerializeField]
    private ItemBarSlot activeItemSlot;

    [SerializeField]
    private int itemBarSlotIndex;

    // Start is called before the first frame update
    void Start()
    {
        itemBarSlotIndex = 0;
        activeItemSlot = itemBarSlots[itemBarSlotIndex];
    }
    private void Update()
    {
        foreach(ItemBarSlot slot in itemBarSlots)
        {
            if (slot.GetItem() != null)
            {
                if (slot.GetItem().count == 0)
                {
                    slot.RemoveItem();
                }
            }
        }
    }

    public void NextItemSlot()
    {
        if(itemBarSlotIndex < (itemBarSlots.Count - 1))
        {
            activeItemSlot.DeactivateSelector();
            itemBarSlotIndex++;
            activeItemSlot = itemBarSlots[itemBarSlotIndex];
            activeItemSlot.ActivateSelector();
        }
    }

    public void LastItemSlot()
    {
        if (itemBarSlotIndex > 0)
        {
            activeItemSlot.DeactivateSelector();
            itemBarSlotIndex--;
            activeItemSlot = itemBarSlots[itemBarSlotIndex];
            activeItemSlot.ActivateSelector();
        }
    }

    public void UseCurrentItem()
    {
        if(activeItemSlot.GetItem() != null)
        {
            if (activeItemSlot.GetItem().item is Weapon)
            {
                weaponEquipmentSlot.SetItem(activeItemSlot.GetItem());
            }
            else if (activeItemSlot.GetItem().item is WeaponProjectile)
            {
                weaponProjectileEquipmentSlot.SetItem(activeItemSlot.GetItem());
            }
            else if (activeItemSlot.GetItem().item is Consumable)
            {
                Consumable item = activeItemSlot.GetItem().item as Consumable;
                if (inventoryController.GetItemCount(item) > 0)
                {
                    attributesEffectsController.AddAttributeEffect(item.consumableEffect);
                    inventoryController.RemoveInventoryItem(activeItemSlot.GetItem(), 1);
                    if (inventoryController.GetItemCount(item) == 0)
                    {
                        activeItemSlot.RemoveItem();
                    }
                }
                else
                {
                    activeItemSlot.RemoveItem();
                }
            }
            else
            {
                Debug.Log("UseCurrentItem Is Nothing from Interest");
            }
        }
       
    }
}
