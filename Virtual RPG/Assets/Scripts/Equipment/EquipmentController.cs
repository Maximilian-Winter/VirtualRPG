using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentController : MonoBehaviour
{
    [SerializeField]
    private EquipmentSlot weaponEquipmentSlot;

    [SerializeField]
    private EquipmentSlot weaponProjectileEquipmentSlot;

    [SerializeField]
    private EquipmentSlot headEquipmentSlot;

    [SerializeField]
    private EquipmentSlot chestEquipmentSlot;

    [SerializeField]
    private EquipmentSlot handsEquipmentSlot;

    [SerializeField]
    private EquipmentSlot legsEquipmentSlot;

    [SerializeField]
    private EquipmentSlot footEquipmentSlot;

    [SerializeField]
    private AttributesEffectsController attributesEffectsController;

    [SerializeField]
    private CombatController combatController;


    private AttributeEffect headEffectTemp;
    private AttributeEffect chestEffectTemp;
    private AttributeEffect handsEffectTemp;
    private AttributeEffect legsEffectTemp;
    private AttributeEffect footEffectTemp;

    private void OnEnable()
    {
        weaponEquipmentSlot.onEquipedSlot += OnWeaponEquipmentChange;
        weaponProjectileEquipmentSlot.onEquipedSlot += OnWeaponProjectileEquipmentChange;
        headEquipmentSlot.onEquipedSlot += OnHeadEquipmentChange;
        chestEquipmentSlot.onEquipedSlot += OnChestEquipmentChange;
        handsEquipmentSlot.onEquipedSlot += OnHandsEquipmentChange;
        legsEquipmentSlot.onEquipedSlot += OnLegsEquipmentChange;
        footEquipmentSlot.onEquipedSlot += OnFootEquipmentChange;

        weaponEquipmentSlot.onRemovedEquipmentSlot += OnWeaponEquipmentRemoved;
        weaponProjectileEquipmentSlot.onRemovedEquipmentSlot += OnWeaponProjectileEquipmentRemoved;
        headEquipmentSlot.onRemovedEquipmentSlot += OnHeadEquipmentRemoved;
        chestEquipmentSlot.onRemovedEquipmentSlot += OnChestEquipmentRemoved;
        handsEquipmentSlot.onRemovedEquipmentSlot += OnHandsEquipmentRemoved;
        legsEquipmentSlot.onRemovedEquipmentSlot += OnLegsEquipmentRemoved;
        footEquipmentSlot.onRemovedEquipmentSlot += OnFootEquipmentRemoved;

    }

    private void OnDisable()
    {
        weaponEquipmentSlot.onEquipedSlot -= OnWeaponEquipmentChange;
        weaponProjectileEquipmentSlot.onEquipedSlot -= OnWeaponProjectileEquipmentChange;
        headEquipmentSlot.onEquipedSlot -= OnHeadEquipmentChange;
        chestEquipmentSlot.onEquipedSlot -= OnChestEquipmentChange;
        handsEquipmentSlot.onEquipedSlot -= OnHandsEquipmentChange;
        legsEquipmentSlot.onEquipedSlot -= OnLegsEquipmentChange;
        footEquipmentSlot.onEquipedSlot -= OnFootEquipmentChange;

        weaponEquipmentSlot.onRemovedEquipmentSlot -= OnWeaponEquipmentRemoved;
        weaponProjectileEquipmentSlot.onRemovedEquipmentSlot -= OnWeaponProjectileEquipmentRemoved;
        headEquipmentSlot.onRemovedEquipmentSlot -= OnHeadEquipmentRemoved;
        chestEquipmentSlot.onRemovedEquipmentSlot -= OnChestEquipmentRemoved;
        handsEquipmentSlot.onRemovedEquipmentSlot -= OnHandsEquipmentRemoved;
        legsEquipmentSlot.onRemovedEquipmentSlot -= OnLegsEquipmentRemoved;
        footEquipmentSlot.onRemovedEquipmentSlot -= OnFootEquipmentRemoved;
    }

    private void OnWeaponEquipmentChange()
    {
        combatController.SetWeapon(weaponEquipmentSlot.GetItem().item as Weapon);
    }

    private void OnWeaponProjectileEquipmentChange()
    {
        combatController.SetWeaponProjectile(weaponProjectileEquipmentSlot.GetItem().item as WeaponProjectile);
    }

    private void OnHeadEquipmentChange()
    {
        Clothing temp = headEquipmentSlot.GetItem().item as Clothing;
        attributesEffectsController.AddAttributeEffect(temp.clothingEffect);
        headEffectTemp = temp.clothingEffect;
    }

    private void OnChestEquipmentChange()
    {
        Clothing temp = chestEquipmentSlot.GetItem().item as Clothing;
        attributesEffectsController.AddAttributeEffect(temp.clothingEffect);
        chestEffectTemp = temp.clothingEffect;
    }

    private void OnHandsEquipmentChange()
    {
        Clothing temp = handsEquipmentSlot.GetItem().item as Clothing;
        attributesEffectsController.AddAttributeEffect(temp.clothingEffect);
        handsEffectTemp = temp.clothingEffect;
    }

    private void OnLegsEquipmentChange()
    {
        Clothing temp = legsEquipmentSlot.GetItem().item as Clothing;
        attributesEffectsController.AddAttributeEffect(temp.clothingEffect);
        legsEffectTemp = temp.clothingEffect;
    }

    private void OnFootEquipmentChange()
    {
        Clothing temp = footEquipmentSlot.GetItem().item as Clothing;
        attributesEffectsController.AddAttributeEffect(temp.clothingEffect);
        footEffectTemp = temp.clothingEffect;
    }

    private void OnWeaponEquipmentRemoved()
    {
        combatController.SetWeapon(null);
    }

    private void OnWeaponProjectileEquipmentRemoved()
    {
        combatController.SetWeaponProjectile(null);
    }

    private void OnHeadEquipmentRemoved()
    {
        attributesEffectsController.RemoveAttributeEffect(headEffectTemp);
    }

    private void OnChestEquipmentRemoved()
    {
        attributesEffectsController.RemoveAttributeEffect(chestEffectTemp);
    }

    private void OnHandsEquipmentRemoved()
    {
        attributesEffectsController.RemoveAttributeEffect(handsEffectTemp);
    }

    private void OnLegsEquipmentRemoved()
    {
        attributesEffectsController.RemoveAttributeEffect(legsEffectTemp);
    }

    private void OnFootEquipmentRemoved()
    {
        attributesEffectsController.RemoveAttributeEffect(footEffectTemp);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
