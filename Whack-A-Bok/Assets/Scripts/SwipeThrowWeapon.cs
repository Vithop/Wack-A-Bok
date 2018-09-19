using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SwipeThrowWeapon : MonoBehaviour {


    public Camera playerCam;
    public GameObject weapon;
    public GameObject playerWeapon;
    public Text DebugText;

    private bool throwWeapon;
    private Vector3 weaponVector;
    private Vector3 originalWeaponPos;
    private Quaternion originalWeaponRot;

    // Use this for initialization
    void Start () {
        throwWeapon = false;
        originalWeaponPos = playerWeapon.transform.position;
        originalWeaponRot = playerWeapon.transform.rotation;
        playerWeapon.AddComponent<Rigidbody>();
        var rb = playerWeapon.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;


    }
	
	// Update is called once per frame
	void Update () {

        if (Input.touchCount != 1)
            return;
        DebugText.text = "Touch Detected"; 

        Touch touch = Input.GetTouch(0);
        Vector2 deltaTouchPos = Vector2.zero;

        switch (touch.phase)
        {
            case TouchPhase.Began:

         
                break;

            case TouchPhase.Moved:
                deltaTouchPos = touch.deltaPosition;
                DebugText.text = "deltaTouchPos" + deltaTouchPos;
                playerWeapon.transform.Translate(deltaTouchPos*0.1f, gameObject.transform);
                break;

            case TouchPhase.Ended:
                if(deltaTouchPos.magnitude > 1)
                {
                    weaponVector = new Vector3 (deltaTouchPos.x, deltaTouchPos.y, deltaTouchPos.magnitude);
                    throwWeapon = true;
                    var dT = playerWeapon.GetComponent<DestroyByTime>();
                    dT.enabled = true;
                }
                else
                {
                    throwWeapon = false;
                    playerWeapon.transform.position = originalWeaponPos;

                }
                
                break;
        }

    }



    void FixedUpdate()
    {
        if (throwWeapon)
        {
            var rb = playerWeapon.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            if (weaponVector == new Vector3(0, 0, 0))
                return;
            rb.AddForce(weaponVector * 500);
            DebugText.text = "Weapon is thrown";
            throwWeapon = false;
            var thrownWeapon = playerWeapon;
            playerWeapon = MakeWeapon();  

        }
    }


    GameObject MakeWeapon()
    {
        DebugText.text = "Weapon is being made";


        GameObject newWeapon = Instantiate(weapon, originalWeaponPos, originalWeaponRot);
        var rb = newWeapon.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        newWeapon.transform.SetParent(gameObject.transform);

        DebugText.text = "Weapon is made";

        return newWeapon;
    }


}
