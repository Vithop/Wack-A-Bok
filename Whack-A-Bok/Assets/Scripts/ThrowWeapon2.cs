using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Remove later
using UnityEngine.UI;

public class ThrowWeapon2 : MonoBehaviour
{

    public Camera playerCam;
    public GameObject weapon;
    public Vector3 spawnPoint;
    public Text DebugText; //remove later
    public float angle;

    private GameObject newWeapon;
    private Rigidbody newWeapon_rb;
    private Vector3 weaponTarget;
    private bool toThrow;



    // Use this for initialization
    void Start()
    {
        MakeWeapon();

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

                    toThrow = true;
                }
                break;

            case TouchPhase.Ended:
                break;
        }

    }

    void FixedUpdate()
    {
        if (toThrow)
        {

            newWeapon_rb.constraints = RigidbodyConstraints.None;
            if (weaponTarget == new Vector3(0, 0, 0))
                return;
            //Vector3 ForceVector = ( (weaponTarget-playerWeapon.transform.position) * 100f);
            //rb.AddForce(ForceVector.x,ForceVector.y,ForceVector.z);
            DebugText.text = "Target Points: " + newWeapon_rb.transform.position + " : " + weaponTarget; // remove later
            newWeapon_rb.velocity = BallisticVel(weaponTarget, newWeapon_rb.transform.position, angle);
            Debug.Log("Weapon is thrown");
            MakeWeapon();
        }
    }


    void MakeWeapon()
    {
        Debug.Log("Weapon is being made");

        toThrow = false;
        newWeapon = Instantiate(weapon, spawnPoint, Quaternion.identity);
        newWeapon_rb = newWeapon.GetComponent<Rigidbody>();
        newWeapon_rb.constraints = RigidbodyConstraints.FreezeAll;
        //newWeapon.transform.SetParent(gameObject.transform);

        Debug.Log("Weapon is made");

        
    }

    Vector3 BallisticVel(Vector3 target, Vector3 initial, float angle)
    {
        Vector3 dir = target - initial;  // get target direction
        float h = dir.y;  // get height difference
        dir.y = 0;  // retain only the horizontal direction
        float dist = dir.magnitude;  // get horizontal distance
        float a = angle * Mathf.Deg2Rad;  // convert angle to radians
        dir.y = dist * Mathf.Tan(a);  // set dir to the elevation angle
        dist += h / Mathf.Tan(a);  // correct for small height differences
                                   // calculate the velocity magnitude
        float vel = Mathf.Sqrt(dist * Physics.gravity.magnitude / Mathf.Sin(2 * a));
        return vel * dir.normalized;
    }

}