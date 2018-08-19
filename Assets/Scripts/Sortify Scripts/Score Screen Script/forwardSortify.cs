using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class forwardSortify : MonoBehaviour {
    public GameObject GM;
    // Use this for initialization
    void Start () {
        GetComponent<Button>().onClick.AddListener(() => forward());
        GM = GameObject.FindGameObjectWithTag("GameManager");
    }
	
	// Update is called once per frame
	public void forward() {
        TruckSpawner.trucksActive = 0;
        GameManager.gameLives = 3;
        ButtonScript.truckCrash = false;
        GameManager.startingTruckSequenceFinished = false;
        StartingTruckBehaviour.startingTruckIsOffScreen = false;
        AudioManager.instance.PlayCommonSound("Button Click");
        AudioManager.instance.StopSound("BGM");
        AudioManager.instance.StopSound("BGM1.2");
        AudioManager.instance.StopSound("BGM1.5");
        AudioManager.instance.StopSound("Score Counting");

        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
}
