using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon", menuName = "VRPG/Items/Weapon", order = 51)]
public class Weapon : BaseItem
{
   public enum WeaponClass
   {
        small,
        medium,
        heavy
   }

    public int weaponDamage;
    public bool weaponIsRanged;
    public float weaponRange;
    public WeaponClass weaponClass;
}
