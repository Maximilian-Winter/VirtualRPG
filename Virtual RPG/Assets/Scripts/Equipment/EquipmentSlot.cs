using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum EquimentSlotType
{
    Weapon,
    Projectile,
    HeadClothing,
    ChestClothing,
    HandsClothing,
    LegsClothing,
    FootClothing
}

public delegate void OnEquippedEquipmentSlot();
public delegate void OnRemovedEquipmentSlot();

public class EquipmentSlot : MonoBehaviour, IDropHandler
{
    [SerializeField]
    private Image equipmentSlotImage;

    private InventoryItem item;

    [SerializeField]
    private EquimentSlotType equimentSlotType = EquimentSlotType.Weapon;

    public OnEquippedEquipmentSlot onEquipedSlot;
    public OnRemovedEquipmentSlot onRemovedEquipmentSlot;

    void Start()
    {
        if(GetItem() != null)
        {
            equipmentSlotImage.sprite = GetItem().item.itemSprite;
            equipmentSlotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        }
        else
        {
            equipmentSlotImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (GetItem() != null)
        {
            if (item.count == 0)
            {
                RemoveItem();
            }
        }
    }

    public InventoryItem GetItem()
    {
        return item;
    }

    public void SetItem(InventoryItem value)
    {
        item = value;
        equipmentSlotImage.sprite = GetItem().item.itemSprite;
        equipmentSlotImage.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
        if (onEquipedSlot != null)
        {
            onEquipedSlot();
        }
    }

    public void RemoveItem()
    {
        item = null;
        equipmentSlotImage.sprite = null;
        equipmentSlotImage.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
        if (onRemovedEquipmentSlot != null)
        {
            onRemovedEquipmentSlot();
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            if (eventData.pointerDrag.GetComponent<InventorySlotUI>() != null)
            {
                if (equimentSlotType == EquimentSlotType.Weapon)
                {
                    if (eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item is Weapon)
                    {
                        SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
                    }
                }
                else if (equimentSlotType == EquimentSlotType.Projectile)
                {
                    if (eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item is WeaponProjectile)
                    {
                        SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
                    }
                }
                else if (equimentSlotType == EquimentSlotType.HeadClothing)
                {
                    if (eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item is Clothing)
                    {
                        Clothing temp = eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item as Clothing;
                        if(temp.clothingSlot == ClothingSlot.HeadClothing)
                        {
                            SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
                        }
                    }
                }
                else if (equimentSlotType == EquimentSlotType.ChestClothing)
                {
                    if (eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item is Clothing)
                    {
                        Clothing temp = eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item as Clothing;
                        if (temp.clothingSlot == ClothingSlot.ChestClothing)
                        {
                            SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
                        }
                    }
                }
                else if (equimentSlotType == EquimentSlotType.HandsClothing)
                {
                    if (eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item is Clothing)
                    {
                        Clothing temp = eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item as Clothing;
                        if (temp.clothingSlot == ClothingSlot.HandsClothing)
                        {
                            SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
                        }
                    }
                }
                else if (equimentSlotType == EquimentSlotType.LegsClothing)
                {
                    if (eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item is Clothing)
                    {
                        Clothing temp = eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item as Clothing;
                        if (temp.clothingSlot == ClothingSlot.LegsClothing)
                        {
                            SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
                        }
                    }
                }
                else if (equimentSlotType == EquimentSlotType.FootClothing)
                {
                    if (eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item is Clothing)
                    {
                        Clothing temp = eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem().item as Clothing;
                        if (temp.clothingSlot == ClothingSlot.FootClothing)
                        {
                            SetItem(eventData.pointerDrag.GetComponent<InventorySlotUI>().GetItem());
                        }
                    }
                }
            }

            if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>() != null)
            {
                if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem != null)
                {
                    if (equimentSlotType == EquimentSlotType.Weapon)
                    {
                        if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item is Weapon)
                        {
                            SetItem(eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem());
                        }
                    }
                    else if (equimentSlotType == EquimentSlotType.Projectile)
                    {
                        if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item is WeaponProjectile)
                        {
                            SetItem(eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem());
                        }
                    }
                    else if (equimentSlotType == EquimentSlotType.HeadClothing)
                    {
                        if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item is Clothing)
                        {
                            Clothing temp = eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item as Clothing;
                            if (temp.clothingSlot == ClothingSlot.FootClothing)
                            {
                                SetItem(eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem());
                            }

                        }
                    }
                    else if (equimentSlotType == EquimentSlotType.ChestClothing)
                    {
                        if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item is Clothing)
                        {
                            Clothing temp = eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item as Clothing;
                            if (temp.clothingSlot == ClothingSlot.ChestClothing)
                            {
                                SetItem(eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem());
                            }

                        }
                    }
                    else if (equimentSlotType == EquimentSlotType.HandsClothing)
                    {
                        if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item is Clothing)
                        {
                            Clothing temp = eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item as Clothing;
                            if (temp.clothingSlot == ClothingSlot.HandsClothing)
                            {
                                SetItem(eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem());
                            }

                        }
                    }
                    else if (equimentSlotType == EquimentSlotType.LegsClothing)
                    {
                        if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item is Clothing)
                        {
                            Clothing temp = eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item as Clothing;
                            if (temp.clothingSlot == ClothingSlot.LegsClothing)
                            {
                                SetItem(eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem());
                            }

                        }
                    }
                    else if (equimentSlotType == EquimentSlotType.FootClothing)
                    {
                        if (eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item is Clothing)
                        {
                            Clothing temp = eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem().item as Clothing;
                            if (temp.clothingSlot == ClothingSlot.FootClothing)
                            {
                                SetItem(eventData.pointerDrag.GetComponent<EquipmentSlotDragDrop>().DraggedItem.GetComponent<EquipmentSlot>().GetItem());
                            }

                        }
                    }
                }
            }
        }
    }
}
