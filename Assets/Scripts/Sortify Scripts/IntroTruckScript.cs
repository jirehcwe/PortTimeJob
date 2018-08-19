using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTruckScript : MonoBehaviour {


    public GameObject topViewTrucks;
    public GameObject mainCamera;
    public GameObject introCamera;
    public GameObject map;
    private Vector3 dirDownRight = new Vector3(100.8f, -58f, 0);
    public float speed;
    public float timeElapsed;
    public float reachedShed;
    public float endSceneTime;
    public float zoomSpeed;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        
        timeElapsed += Time.deltaTime;
        if (timeElapsed < reachedShed)
        {
            this.transform.position += dirDownRight * speed * Time.deltaTime;
        }
        else 
        {
            introCamera.GetComponent<Camera>().fieldOfView -= zoomSpeed*Time.deltaTime;
        }

        if (timeElapsed > endSceneTime)
        {
            introCamera.SetActive(false);
            topViewTrucks.SetActive(true);
            mainCamera.SetActive(true);    
            map.SetActive(false);
            
        }


    }
}
