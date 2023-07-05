using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public ZombieHealth zombieHealth;

    public void OnRaycastHit(RaycastWeapon weapon, Vector3 direction)
    {
        zombieHealth.TakeDamage(weapon.damage, direction);
    }
}
