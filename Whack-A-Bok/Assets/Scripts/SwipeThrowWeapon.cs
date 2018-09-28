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
        originalWeaponPos = playerWeapon.transform.localPosition;
        originalWeaponRot = playerWeapon.transform.localRotation;
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
        float touchMagnitude = 0f;

        switch (touch.phase)
        {
            case TouchPhase.Began:
                DebugText.text = "Touch Begin";


                break;

                case TouchPhase.Moved:
                DebugText.text = "Touch Moved";
                deltaTouchPos = touch.deltaPosition;
                touchMagnitude = deltaTouchPos.sqrMagnitude;
                DebugText.text = "Mag & Pos:  " + touchMagnitude + ", " + deltaTouchPos;
                playerWeapon.transform.Translate(deltaTouchPos*0.0001f, gameObject.transform);
                break;

            case TouchPhase.Ended:
                DebugText.text = "Touch Ended";
                if (touchMagnitude > 0.0001)
                {
                    weaponVector = new Vector3 (deltaTouchPos.x,touchMagnitude , deltaTouchPos.y) + gameObject.transform.forward;
                    throwWeapon = true;
                    var dT = playerWeapon.GetComponent<DestroyByTime>();
                    dT.enabled = true;
                    DebugText.text = "Touch Trow, WeaponV: " + weaponVector;

                }
                else
                {
                    throwWeapon = true;
                    playerWeapon.transform.localPosition = originalWeaponPos;
                    DebugText.text = "Touch End, WeaponV: " + Vector3.zero;

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
            rb.AddForce(weaponVector * 50);
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
