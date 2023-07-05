using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadWeapon : MonoBehaviour
{
    public Animator rigController;

    public WeaponAnimatinEwents animationEvents;

    public ActiveWeapon activeWeapon;
    public Transform leftHand;
    public AmmoWidget ammoWidget;
    public bool isReloading;
    private GameObject magazineHand;
    
    // Start is called before the first frame update
    void Start()
    {
        animationEvents.WeaponAnimationEvent.AddListener(OnAnimationEvent);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();

        if (weapon)
        {
            if ((Input.GetButtonDown("Reload") || weapon.ammoCount <= 0) && !activeWeapon.isHolstered)
            {
                isReloading = true;
                rigController.SetTrigger("reload_weapon");
            }

            if (weapon.isFiring)
            {
                ammoWidget.Refresh(weapon.ammoCount);
            }
        }
        
    }

    void OnAnimationEvent(string eventName)
    {
        switch (eventName)
        {
            case "detach_magazine":
                DetachMagazine();
                break;
            case "drop_magazine":
                DropMagazine();
                break;
            case "refill_magazine":
                RefillMagazine();
                break;
            case "attach_magazine":
                AttachMagazine();
                break;
        }
    }

    void DetachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        magazineHand = Instantiate(weapon.magazine, leftHand, true);
        weapon.magazine.SetActive(false);
    }
    void DropMagazine()
    {
        GameObject droppedMagazine =
            Instantiate(magazineHand, magazineHand.transform.position, magazineHand.transform.rotation);
        droppedMagazine.AddComponent<Rigidbody>();
        droppedMagazine.AddComponent<BoxCollider>();
        Destroy(droppedMagazine, 5f);
        magazineHand.SetActive(false);

    }
    void RefillMagazine()
    {
        magazineHand.SetActive(true);
    }
    void AttachMagazine()
    {
        RaycastWeapon weapon = activeWeapon.GetActiveWeapon();
        weapon.magazine.SetActive(true);
        Destroy(magazineHand);
        weapon.ammoCount = weapon.clipSize;
        rigController.ResetTrigger("reload_weapon");
        
        ammoWidget.Refresh(weapon.ammoCount);
        isReloading = false;
    }
}
