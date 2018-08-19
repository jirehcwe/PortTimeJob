using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class tapInstructionScript : MonoBehaviour {

    private GameObject containerSprite;
    public GameObject blueCon;
    public GameObject redCon;
    public GameObject yellowCon;
    public GameObject greenCon;

    public GameObject blueCont;
    public GameObject redCont;
    public GameObject yellowCont;
    public GameObject greenCont;
    public Color containerColor;

    // Use this for initialization
    void Start () {
        blueCon.SetActive(false);
        redCon.SetActive(false);
        yellowCon.SetActive(false);
        greenCon.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {


        if (GameObject.FindGameObjectWithTag("containerpic") != null) {
            containerSprite = GameObject.FindGameObjectWithTag("containerpic");
            containerColor = containerSprite.GetComponent<SpriteRenderer>().color;

            if (containerSprite.transform.parent.transform.parent.transform.position.y < 4.8)
            {

                if (containerColor == redCont.GetComponent<Image>().color)
                {
                    blueCon.SetActive(false);
                    redCon.SetActive(true);
                    yellowCon.SetActive(false);
                    greenCon.SetActive(false);
                }
                else if (containerColor == blueCont.GetComponent<Image>().color)
                {
                    blueCon.SetActive(true);
                    redCon.SetActive(false);
                    yellowCon.SetActive(false);
                    greenCon.SetActive(false);
                }
                else if (containerColor == yellowCont.GetComponent<Image>().color)
                {
                    blueCon.SetActive(false);
                    redCon.SetActive(false);
                    yellowCon.SetActive(true);
                    greenCon.SetActive(false);
                }
                else if (containerColor == greenCont.GetComponent<Image>().color)
                {
                    blueCon.SetActive(false);
                    redCon.SetActive(false);
                    yellowCon.SetActive(false);
                    greenCon.SetActive(true);
                }
                else
                {
                    blueCon.SetActive(false);
                    redCon.SetActive(false);
                    yellowCon.SetActive(false);
                    greenCon.SetActive(false);
                }
            }

        } else
        {
            blueCon.SetActive(false);
            redCon.SetActive(false);
            yellowCon.SetActive(false);
            greenCon.SetActive(false);
        }
            

        

        
    }
}
