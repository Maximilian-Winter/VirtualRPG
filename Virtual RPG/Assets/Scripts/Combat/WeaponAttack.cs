using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
    [SerializeField]
    private string attackerName;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private WeaponProjectile weaponProjectile;

    public string AttackerName { get => attackerName; set => attackerName = value; }
    public Weapon Weapon { get => weapon; set => weapon = value; }
    public WeaponProjectile WeaponProjectile { get => weaponProjectile; set => weaponProjectile = value; }

    public void SetAttackerName(string name)
    {
        AttackerName = name;
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
