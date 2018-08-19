using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restartSortify : MonoBehaviour {

    public GameObject GM;
	// Use this for initialization
	void Start () {
        GetComponent<Button>().onClick.AddListener(() => restart());
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }
	
	// Update is called once per frame
	public void restart () {
        TruckSpawner.trucksActive = 0;
        GameManager.gameLives = 3;
        ButtonScript.truckCrash = false;
        GameManager.startingTruckSequenceFinished = false;
        StartingTruckBehaviour.startingTruckIsOffScreen = true;
        AudioManager.instance.PlayCommonSound("Button Click");
        AudioManager.instance.StopSound("BGM");
        AudioManager.instance.StopSound("BGM1.2");
        AudioManager.instance.StopSound("BGM1.5");
        AudioManager.instance.StopSound("Score Counting");

        SceneManager.LoadScene("Sortify", LoadSceneMode.Single);
    }
}
