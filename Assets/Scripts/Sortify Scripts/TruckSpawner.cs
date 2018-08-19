using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TruckSpawner : MonoBehaviour {
	public GameObject truckPrefab;
	public Vector3 spawnPoint;
	public int numberOfTrucks;
	public static int categories = 4;
	public float spawnRateCap = 0.4f;
    public GameObject wavesText;
    public GameObject GM;
    public AudioSource waveSound;

	[SerializeField]
	private static int Level;
	[SerializeField]
	private float spawnRate;

    AudioSource zoom;

    public static int trucksActive;



	// Use this for initialization
	void Start () {
		spawnPoint = new Vector3 (0, 25f, 0);
		Level = 0;
		spawnRate = 1;
		TruckBehaviour.maxTruckSpeed = 2;
        TruckSpawner.trucksActive = 0;
        waveSound = AudioManager.instance.GetSource("waveFinished");
        zoom = AudioManager.instance.GetSource("carZoom");
    }
	
	// Update is called once per frame
	void Update (){
//       Debug.Log("truckcrash bool" + ButtonScript.truckCrash);
//       Debug.Log("trucks active" + TruckSpawner.trucksActive);


        if (ButtonScript.truckCrash == false && GameManager.startingTruckSequenceFinished == true) { 
			if (TruckSpawner.trucksActive == 0) {
                
                if (!GameManager.wavesOnScreen) {
                    
                    Invoke("activateWaves",0);

                    Level++;
                    
                    InvokeRepeating("spawnTruck", 0, spawnRate);
                    Invoke("CancelInvoke", (Level - 1) * spawnRate + 0.01f);


                    TruckBehaviour.maxTruckSpeed += 1;
                    if (spawnRate < spawnRateCap)
                    {
                        spawnRate = spawnRateCap;
                    }
                    else spawnRate -= 0.1f;
                }
			}
		}
	}

	void spawnTruck(){
        if (trucksActive == 0)
        {
            AudioManager.instance.PlaySound("truckMoveLoop");
        }
        TruckSpawner.trucksActive++;
        GameObject truck;
        truck = Instantiate(truckPrefab.transform, spawnPoint, new Quaternion(180, 0, 0, 0)).gameObject;
	}
   
	public static int GetLevel(){
		return Level;
	}
    
    void activateWaves()
    {
        waveSound.pitch = 1 + Level * 0.1f;
        zoom.pitch = 0.95f + TruckSpawner.GetLevel() * 0.05f;
        AudioManager.instance.PlaySound("clothFlap");
        AudioManager.instance.PlaySound("carZoom");
        //AudioManager.instance.PlaySound("waveFinished");
        if (Level == 4)
        {
            AudioManager.instance.StopSound("BGM");
            AudioManager.instance.PlaySound("BGM1.2");
        }

        if (Level == 8)
        {
            AudioManager.instance.StopSound("BGM1.2");
            AudioManager.instance.PlaySound("BGM1.5");
        }
        wavesText.SetActive(true);
        GameManager.wavesOnScreen = true;
 //       Debug.Log("waves activated");
    }

    public void setWavesFinished()
    {
        GameManager.wavesOnScreen = true;
    }


}
