using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerHover : MonoBehaviour {

    public GameObject player;
    private float distanceX;
    private float distanceY;
    private float distanceZ;
    private float distanceBtwen;


	// Use this for initialization
	void Start () {


        distanceBtwen = Vector3.Distance(transform.position, player.transform.position);

        distanceX = player.transform.position.x - transform.position.x;
        distanceY = player.transform.position.y - transform.position.y;
        distanceZ = player.transform.position.z - transform.position.z;
        transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y,transform.position.z),
            new Vector3(transform.position.x + distanceX, transform.position.y, transform.position.z + distanceZ),
            Time.deltaTime);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
