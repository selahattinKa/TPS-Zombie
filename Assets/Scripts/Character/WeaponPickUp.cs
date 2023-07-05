using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public RaycastWeapon wweaponPF;

    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.gameObject.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            RaycastWeapon newWeapon = Instantiate(wweaponPF);
            activeWeapon.Equip(newWeapon);
        }
    }
}
