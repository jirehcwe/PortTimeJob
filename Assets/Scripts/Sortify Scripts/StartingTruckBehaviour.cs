using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingTruckBehaviour : MonoBehaviour {


	public static bool startingTruckIsOffScreen = false;



	private float maxTruckSpeed = 500;
    private float yLimit = -860f;
//    public bool outOfBounds = false;


	// Use this for initialization
	void Start () {
		startingTruckIsOffScreen = false;
	}
	
	// Update is called once per frame
	void Update () {
		truckMovement ();
        checkBoundaries();

    }

	void truckMovement(){
		this.transform.position += Vector3.down * Time.deltaTime * maxTruckSpeed;
	}

    void checkBoundaries()
    {
        if (this.transform.position.y < yLimit)
        {
            Destroy(this.gameObject);
//            outOfBounds = true;
        }
    }
		
}
