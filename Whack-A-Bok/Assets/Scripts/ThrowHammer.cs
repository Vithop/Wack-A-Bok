using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHammer : MonoBehaviour {

    public Camera playerCam;
    private bool throwHammer;
    public Rigidbody rb;
    private Vector3 hammerTarget;


	// Use this for initialization
	void Start ()
    {
        throwHammer = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        throwHammer = false;

        if (Input.touchCount != 1)
        {
            return;
        }
        var ray = playerCam.ScreenPointToRay(Input.touches[0].position);
        var hitInfo = new RaycastHit();

        if(Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.transform.name != "GameField")
                return;
            throwHammer = true;
            hammerTarget = hitInfo.point;
        }
	}

    void FixedUpdate()
    {
        if (throwHammer)
        {
            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce( hammerTarget * 100); 
        }
        
    }

}
