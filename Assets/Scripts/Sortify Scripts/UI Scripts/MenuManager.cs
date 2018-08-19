using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour {


	public GameObject SplashText;
	// Use this for initialization
	void Start () {
		InvokeRepeating ("flashTheText", 0f, 0.4f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)){
			SceneManager.LoadScene ("game");
			}
			
}
	void flashTheText(){
		if (SplashText.activeInHierarchy){
			SplashText.SetActive (false);
		} 
		else {
			SplashText.SetActive (true);
		}
	}
}
		