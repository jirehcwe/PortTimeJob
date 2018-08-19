using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour {
    
    public GameObject truckSpawnerObj;
    public AudioSource sortFailSound;

    void Start()
    {
        sortFailSound = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        checkBoundaries(other);

        if (TruckSpawner.trucksActive == 0 && GameManager.introSceneFinished == true)
        {
            AudioManager.instance.StopSound("truckMoveLoop");

        }
    }


    void checkBoundaries(Collider2D other){
        if (other.gameObject.tag == "Truck") {
            if (other.gameObject.transform.GetChild(0).childCount > 0)
            {

                AudioManager.instance.PlaySound("sortMiss");
                if (GameManager.gameLives>0)
                GameManager.gameLives--;
                else {
                    GameManager.gameLives = 0;
                    ButtonScript.truckCrash = true;
                }

            }

            Destroy (other.gameObject);

            
            

           // truckSpawnerObj.SetActive(false);
			if (TruckSpawner.trucksActive < 0){
				TruckSpawner.trucksActive = 0;
			}
			else {TruckSpawner.trucksActive--;}

		}

		if (other.gameObject.tag == "StartingTruck"){
			Destroy (other.gameObject);

		}
	}
}
