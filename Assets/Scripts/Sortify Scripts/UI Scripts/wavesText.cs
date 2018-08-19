using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class wavesText : MonoBehaviour {

	public Text waveText;
    public Vector3 spawnPoint;
    

    void Start()
    {
    }


	
	// Update is called once per frame
	void Update () {
        
        waveText.text = ("" + TruckSpawner.GetLevel());

        this.transform.Translate(Vector3.left * TruckBehaviour.maxTruckSpeed *Time.deltaTime * 100);
//        print(this.transform.position);
        if (this.transform.localPosition.y <= -900)
        {
            
            this.transform.SetPositionAndRotation(spawnPoint, Quaternion.Euler(0, 0, 90));
            GameManager.wavesOnScreen = false;
            AudioManager.instance.StopSound("clothFlap");
            AudioManager.instance.StopSound("carZoom");
            this.gameObject.SetActive(false);
        }
		/*currentLevel = TruckSpawner.GetLevel ();

		if (currentLevel == 0){
			wave.SetActive (true);
			currentLevel++;
			for (int i = 0; i < 3; i++) {
				wave.SetActive (true);
				waveText.text = "Starting Level...";
				StartCoroutine (waitThenDisable (0.5f, wave));
			}

		} else if (currentLevel < TruckSpawner.GetLevel()){
			currentLevel++;
			wave.SetActive (true);
			waveText.text = "Wave" + TruckSpawner.GetLevel () + "Incoming!";
			StartCoroutine (waitThenDisable (0.5f, wave));

		}
        */
	}



	public static IEnumerator waitThenDisable(float time, GameObject obj){
		yield return new WaitForSeconds (time);
		obj.SetActive (false);
	}

}
