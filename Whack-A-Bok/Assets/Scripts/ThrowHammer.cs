using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHammer : MonoBehaviour
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
        var hitInfo = new RaycastHit();

        switch (touch.phase)
        {
            case TouchPhase.Began:

                if (Physics.Raycast(ray, out hitInfo))
                {
                    if (hitInfo.transform.name != "GameField")
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
            rb.AddForce((weaponTarget - playerWeapon.transform.position) * 100);
            Debug.Log("Weapon is thrown");
            throwWeapon = false;
        }
    }


    GameObject MakeWeapon()
    {
        Debug.Log("Weapon is being made");

        var weaponPos = playerWeapon.transform.position + new Vector3(0f,0f,0.5f);
        var weaponRot = playerWeapon.transform.rotation;

        GameObject newWeapon = Instantiate(weapon, weaponPos, weaponRot);
        var rb = newWeapon.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        newWeapon.transform.SetParent(gameObject.transform);

        Debug.Log("Weapon is made");

        return newWeapon;
    }

}
