using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAiming : MonoBehaviour
{
    public float turnSpeed = 15f;
    public Transform cameraLookAt;
    public Cinemachine.AxisState xAxis;
    public Cinemachine.AxisState yAxis;
    public bool isAiming;
    
    
    private Camera mainCamera;
    private Animator animator;
    private ActiveWeapon activeWeapon;
    private int isAimingParam = Animator.StringToHash("isAiming");

    // Start is called before the first frame update
    

    void Start()
    {
        mainCamera = Camera.main;
        // weapon = GetComponent<RaycastWeapon>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        animator = GetComponent<Animator>();
        activeWeapon = GetComponent<ActiveWeapon>();

    }

    private void Update()
    {
        isAiming = Input.GetButton("Fire2");
        animator.SetBool(isAimingParam, isAiming);

        var weapon = activeWeapon.GetActiveWeapon();
        if (weapon)
        {
            weapon.recoil.recoilModifier = isAiming ? 0.3f : 1.0f;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        xAxis.Update(Time.fixedDeltaTime);
        yAxis.Update(Time.fixedDeltaTime);
        
        cameraLookAt.eulerAngles = new Vector3(yAxis.Value, xAxis.Value, 0);
        float yawCamera = mainCamera.transform.rotation.eulerAngles.y;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,yawCamera, 0), turnSpeed * Time.fixedDeltaTime );
        
    }
}
