using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPauseToggle : MonoBehaviour {

    public GameObject pause;

    
	// Use this for initialization
	public void Toggle(bool switcher)
    {
        //Debug.Log(switcher);
        pause.SetActive(!switcher);
        
    }
    
}
