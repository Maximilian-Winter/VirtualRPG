using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimObjectChanger : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private CombatController combatController;

    [SerializeField]
    private Sprite defaultAimObjectSprite;

    private Sprite weaponAimObjectSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(combatController.IsInCombat)
        {
            if(combatController.Weapon != null)
            {
                if (combatController.Weapon.itemSprite != weaponAimObjectSprite)
                {
                    weaponAimObjectSprite = combatController.Weapon.itemSprite;
                }
               
            }
            else 
            {
                if (defaultAimObjectSprite != weaponAimObjectSprite)
                {
                    weaponAimObjectSprite = defaultAimObjectSprite;
                }
            }
            if(spriteRenderer.sprite != weaponAimObjectSprite)
            {
                spriteRenderer.sprite = weaponAimObjectSprite;
            }

        }
    }
}
