using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public GameObject Mole;
    public int numberOfMoles;
    public Vector3[] spawnValues;
    public float startWait;
    public float spawnWait;
    public float forceOfPop;
    public float speedOfPop;

    private bool[] isActive = new bool[9];
    private GameObject[] moles = new GameObject[9];
    private Rigidbody rbMole;

    // Use this for initialization
    void Start () {
        //set all holes as inactive on start
        for (int x = 0; x < 9; x++)
        {
            isActive[x] = false;
        }

       
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
            //choose a hole to pop mole out of
            int holeNum = Random.Range(0, 9);

            if (!isActive[holeNum])
            {
                //create a mole a record where mole is
                moles[holeNum] = Instantiate(Mole, spawnValues[holeNum] - new Vector3(0, 0.5f, 0), Quaternion.identity);
                isActive[holeNum] = true;

                //create rigidbody
                rbMole = moles[holeNum].GetComponent<Rigidbody>();

                //poping animation
                rbMole.detectCollisions = false;
                rbMole.AddForce(transform.up * forceOfPop);
                yield return new WaitForSeconds(speedOfPop);
                rbMole.detectCollisions = true;

                //wait before spawning next mole and removing mole
                yield return new WaitForSeconds(spawnWait);
                Debug.Log(rbMole.position);
                Debug.Log(spawnValues[holeNum]);
                float dis = Vector3.Distance(rbMole.position, spawnValues[holeNum]);
                if(dis < 0.1f)
                {
                    Debug.Log("same position");
                    StartCoroutine(RemoveMole(holeNum));
                }
                

            }
            
            
            

            //if (gameOver)
            //{
            //    restartButton.SetActive(true);
            //    //              restartText.text = "Press 'R' for Restart";
            //    //              restart = true;
            //    break;
            //}
        }
    }
    

    IEnumerator RemoveMole(int holeNum)
    {
        rbMole.detectCollisions = false;
        yield return new WaitForSeconds(speedOfPop*3);
        Destroy(moles[holeNum]);
        isActive[holeNum] = false;


    }
}
