using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Weapon Projectile", menuName = "VRPG/Items/WeaponProjectile", order = 51)]
public class WeaponProjectile : BaseItem
{
    public int weaponProjectileDamage;
    public GameObject projectilePrefab;

    public Weapon weapon;
}
