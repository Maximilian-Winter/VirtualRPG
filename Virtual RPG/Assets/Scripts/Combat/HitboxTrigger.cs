using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxTrigger : MonoBehaviour
{
    public delegate void HitDelegate();
    public HitDelegate OnHit;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "WeaponProjectile")
        {
            Destroy(col.gameObject);
        }

    }

    public void HitTarget(string attackerName, Weapon weapon, WeaponProjectile weaponProjectile)
    {
        if (OnHit != null)
        {
            OnHit();
        }

        System.Random rnd = new System.Random();
        int target = rnd.Next(1, 21) + GetComponentInParent<AttributesController>().DerivedAttributes.AgilityModifier;

        GameObject Attacker = GameObject.Find(attackerName);
        int attackerMod = System.Math.Max(Attacker.GetComponent<AttributesController>().DerivedAttributes.DexterityModifier, Attacker.GetComponent<AttributesController>().DerivedAttributes.IntelligenceModifier);
        int attacker = rnd.Next(1, 21) + attackerMod;

        if (attacker > target)
        {
            GetComponentInParent<AttributesController>().CurrentHealth -= (int)(weapon.weaponDamage * (float)(1.0f - (GetComponentInParent<AttributesController>().DerivedAttributes.DamageResistance / 100.0f)));
            Debug.Log("WeaponHit Target: " + GetComponentInParent<AttributesController>().DerivedAttributes.DamageResistance);
            if (weaponProjectile != null)
            {
                GetComponentInParent<AttributesController>().CurrentHealth -= weaponProjectile.weaponProjectileDamage;
            }
            //Debug.Log("WeaponHit Target: " + target + " Attacker: " + attacker);
        }
        else
        {
           // Debug.Log("WeaponMiss Target: " + target + " Attacker: " + attacker);
        }
    }
}
