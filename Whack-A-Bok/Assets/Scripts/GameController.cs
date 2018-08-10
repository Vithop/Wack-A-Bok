using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject Mole;
    public float startWait;
    public Vector3[] spawnValues;
    public int numberOfMoles;
    public float spawnWait;



    // Use this for initialization
    void Start () {
        StartCoroutine(SpawnMoles());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator SpawnMoles()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            Vector3 spawnPosition = spawnValues[Random.Range(0,9)];
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(Mole, spawnPosition, spawnRotation);
            yield return new WaitForSeconds(spawnWait);
            
            

            //if (gameOver)
            //{
            //    restartButton.SetActive(true);
            //    //              restartText.text = "Press 'R' for Restart";
            //    //              restart = true;
            //    break;
            //}
        }
    }
}
