using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowHammer : MonoBehaviour
{

    public Camera playerCam;
    private bool throwHammer;
    public GameObject ballHammer;
    public Rigidbody rb;
    private Vector3 hammerTarget;
    private float startTapped;
    private float endTapped;

    // Use this for initialization
    void Start()
    {
        throwHammer = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (throwHammer)
        {
            endTapped = Time.time;
            Debug.Log(endTapped - startTapped);
            if (Input.touchCount == 1 && (endTapped - startTapped) > 1)
                MakeHammer();
        }*/

        if (Input.touchCount != 1)
        {
            return;
        }
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
                    hammerTarget = hitInfo.point;
                    throwHammer = true;
                }
                break;

            case TouchPhase.Ended:
                ballHammer = MakeHammer();
                break;
        }






    }

    void FixedUpdate()
    {
        if (throwHammer)
        {

            rb.constraints = RigidbodyConstraints.None;
            rb.AddForce(hammerTarget * 100);


        }
    }


    GameObject MakeHammer()
    {
        
        GameObject newBallHammer = Instantiate(ballHammer /*, hammerPos, hammerRot*/);
        rb = newBallHammer.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
        newBallHammer.transform.SetParent(playerCam.transform);

        var hammerPos = playerCam.transform.position + new Vector3(0.27f,-1.45f,2.37f) ;
        var hammerRot = playerCam.transform.rotation;

        newBallHammer.transform.position = hammerPos;
        newBallHammer.transform.rotation = hammerRot;
        throwHammer = false;
        return newBallHammer;
    }

}
