using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTopTruckScript : MonoBehaviour {


    public GameObject mainCamera;
    public GameObject truckCamera;
    public float zoomSpeed;
    public float moveSpeed;
    public AudioSource truckSound;

    // Use this for initialization
    void Start () {
        truckSound = AudioManager.instance.GetSource("truckMoveLoop");
    }
	
	// Update is called once per frame
	void Update () {

            if (mainCamera.GetComponentInChildren<Camera>().fieldOfView > 60)
                mainCamera.GetComponentInChildren<Camera>().fieldOfView -= Time.deltaTime * zoomSpeed;

        if (mainCamera.GetComponentInChildren<Camera>().fieldOfView <= 32 || this.transform.position.y > -15)
        {
            this.transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        if (this.transform.position.y <= 0)
        {
            //truckCamera.GetComponentInChildren<Camera>().gameObject.SetActive(false);
            if (mainCamera.transform.parent != null)
               AudioManager.instance.PlaySound("introFinished");
            mainCamera.transform.SetParent(null, true);
            truckSound.volume -= 0.085f*Time.deltaTime;
        }

        if(transform.position.y <-13)
            GameManager.introSceneFinished = true;
        if (transform.position.y < -14)
        {

            this.gameObject.SetActive(false);
        }

        
    }
}
