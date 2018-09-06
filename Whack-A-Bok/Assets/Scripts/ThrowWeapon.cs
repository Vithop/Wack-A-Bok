﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{

    public Camera playerCam;
    public GameObject weapon;
    public GameObject playerWeapon;

    private bool throwWeapon;
    private Vector3 weaponTarget;


    // Use this for initialization
    void Start()
    {
        throwWeapon = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount != 1)
        {
            return;
        }
        Debug.Log("Touch detected");

        var ray = playerCam.ScreenPointToRay(Input.touches[0].position);
        Touch touch = Input.GetTouch(0);
        RaycastHit hitInfo;

        switch (touch.phase)
        {
            case TouchPhase.Began:

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.name != "GameField"/* && !hitInfo.transform.name.Contains("PillarMole")*/)
                        return;                    
                    weaponTarget = hitInfo.point;
                    
                    throwWeapon = true;
                }
                break;

            case TouchPhase.Ended:
                break;
        }

    }

    void FixedUpdate()
    {
        if (throwWeapon)
        {
            var rb = MakeWeapon().GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            if (weaponTarget == new Vector3(0, 0, 0))
                return;
            Vector3 ForceVector = ( (weaponTarget-playerWeapon.transform.position) * 100f);
            rb.AddForce(ForceVector.x,ForceVector.y,ForceVector.z);
            Debug.Log("Weapon is thrown");
            throwWeapon = false;
        }
    }


    GameObject MakeWeapon()
    {
        Debug.Log("Weapon is being made");

        var weaponPos = playerWeapon.transform.position + (1f * new Vector3(0f, 0.04f, 0.57f));
        var weaponRot = playerWeapon.transform.rotation;

        GameObject newWeapon = Instantiate(weapon, weaponPos, weaponRot);
        var rb = newWeapon.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        newWeapon.transform.SetParent(gameObject.transform);

        Debug.Log("Weapon is made");

        return newWeapon;
    }

}