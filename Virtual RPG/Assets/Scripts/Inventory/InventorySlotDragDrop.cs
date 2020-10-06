using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlotDragDrop : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private Canvas canvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private GameObject draggedItem;
    private GameObject itemTooltip;

    private void Awake()
    {
        canvas = GameObject.Find("UI").GetComponent<Canvas>();
        itemTooltip = GameObject.Find("ItemTooltipUI");
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnDestroy()
    {
        if(draggedItem != null)
        {
            Destroy(draggedItem);
        }   
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DeactivateTooltip();
        draggedItem = Instantiate(gameObject, transform.position, transform.rotation, canvas.transform);

        draggedItem.GetComponent<CanvasGroup>().alpha = .6f;
        draggedItem.GetComponent<CanvasGroup>().blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        draggedItem.GetComponent<RectTransform>().anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Destroy(draggedItem);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Invoke("ActivateTooltip", 0.5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("ActivateTooltip");
        DeactivateTooltip();
    }

    void ActivateTooltip()
    {
        itemTooltip.GetComponent<ItemTooltip>().ActivateTooltip();
        itemTooltip.GetComponent<ItemTooltip>().TooltipItemName.text = gameObject.GetComponent<InventorySlotUI>().GetItem().item.itemName;
        if(gameObject.GetComponent<InventorySlotUI>().GetItem().item is Weapon)
        {
            Weapon weapon = gameObject.GetComponent<InventorySlotUI>().GetItem().item as Weapon;
            if(weapon.weaponClass == Weapon.WeaponClass.small)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Small Weapon";
            }
            if (weapon.weaponClass == Weapon.WeaponClass.medium)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Medium Weapon";
            }
            if (weapon.weaponClass == Weapon.WeaponClass.heavy)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Heavy Weapon";
            }

            itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Damage: " + weapon.weaponDamage + "\n";
            itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text += "Range: " + weapon.weaponRange + "\n";
        }
        if (gameObject.GetComponent<InventorySlotUI>().GetItem().item is WeaponProjectile)
        {
            itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Projectile";

            WeaponProjectile weaponProjectile = gameObject.GetComponent<InventorySlotUI>().GetItem().item as WeaponProjectile;
            itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Extra Damage: " + weaponProjectile.weaponProjectileDamage + "\n";
        }
        if (gameObject.GetComponent<InventorySlotUI>().GetItem().item is Clothing)
        {
            Clothing clothing = gameObject.GetComponent<InventorySlotUI>().GetItem().item as Clothing;

            if (clothing.clothingSlot == ClothingSlot.ChestClothing)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Chest Clothing";
            }
            if (clothing.clothingSlot == ClothingSlot.FootClothing)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Foot Clothing";
            }
            if (clothing.clothingSlot == ClothingSlot.HandsClothing)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Hands Clothing";
            }
            if (clothing.clothingSlot == ClothingSlot.HeadClothing)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Head Clothing";
            }
            if (clothing.clothingSlot == ClothingSlot.LegsClothing)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Legs Clothing";
            }

            if (clothing.clothingEffect.statEffect == StatEffect.AgilityModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Agillity +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.Carryweight)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Carryweight +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.CharismaModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Charisma +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.CharismaModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Charisma +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.DamageResistance)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Damage Resistance +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.DexterityModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Dexterity +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.EnduranceModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Endurance +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.Health)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Health +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.IntelligenceModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Intelligence +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.Mana)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Mana +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.MaxHealth)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Max. Health +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.MaxMana)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Max. Mana +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.StrengthModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Strength +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.ToughnessModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Toughness +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.WillpowerModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Willpower +" + clothing.clothingEffect.attributeEffectValue;
            }
            if (clothing.clothingEffect.statEffect == StatEffect.WisdomModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Wisdom +" + clothing.clothingEffect.attributeEffectValue;
            }
        }



        if (gameObject.GetComponent<InventorySlotUI>().GetItem().item is Consumable)
        {
            itemTooltip.GetComponent<ItemTooltip>().TooltipItemType.text = "Consumable";
            Consumable consumable = gameObject.GetComponent<InventorySlotUI>().GetItem().item as Consumable;
            if (consumable.consumableEffect.statEffect == StatEffect.AgilityModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Agillity +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.Carryweight)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Carryweight +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.CharismaModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Charisma +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.CharismaModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Charisma +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.DamageResistance)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Damage Resistance +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.DexterityModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Dexterity +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.EnduranceModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Endurance +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.Health)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Health +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.IntelligenceModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Intelligence +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.Mana)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Mana +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.MaxHealth)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Max. Health +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.MaxMana)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Max. Mana +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.StrengthModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Strength +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.ToughnessModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Toughness +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.WillpowerModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Willpower +" + consumable.consumableEffect.attributeEffectValue;
            }
            if (consumable.consumableEffect.statEffect == StatEffect.WisdomModifier)
            {
                itemTooltip.GetComponent<ItemTooltip>().TooltipItemDescription.text = "Wisdom +" + consumable.consumableEffect.attributeEffectValue;
            }
        }
    }

    void DeactivateTooltip()
    {
        itemTooltip.GetComponent<ItemTooltip>().DeactivateTooltip();
    }
}
