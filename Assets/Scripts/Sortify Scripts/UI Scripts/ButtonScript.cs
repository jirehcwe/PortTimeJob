using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour {

	GameObject containerSprite;
	public Color buttonColor;
	public Color containerColor;
	private bool isCorrectButton;
    public GameObject[] buttons;
    public GameObject wavesText;

    [HideInInspector]
	public static bool truckCrash = false;


	// Use this for initialization
	void Start () {
        buttonColor = gameObject.GetComponent<Image>().color;
        buttons = GameObject.FindGameObjectsWithTag("button");
    }

    // Update is called once per frame
    void Update() {

        if (!GameManager.gamePaused)
        {
            if (truckCrash)
            {
                //print("disabled since truck crashed");
                this.gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                if (GameObject.FindGameObjectWithTag("containerpic") != null)
                {
                    containerSprite = GameObject.FindGameObjectWithTag("containerpic");
                    containerColor = containerSprite.GetComponent<SpriteRenderer>().color;

                    if (GameManager.wavesOnScreen)
                    {
                        //print("disabled since waves on screen");
                        this.gameObject.GetComponent<Button>().interactable = false;
                    }
                    else
                    {
                        this.gameObject.GetComponent<Button>().interactable = true;
                    }
                }
                else
                {
                   // print("disabled since no container found");
                    this.gameObject.GetComponent<Button>().interactable = false;
                }
            }
        } else
        {
            //print("disabled since game paused");
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        
        

        
       
        


        /*if (wavesText.GetComponent<RectTransform>().localPosition.y > 700 && wavesText.GetComponent<RectTransform>().localPosition.y < -900)
        {
            this.gameObject.GetComponent<Button>().interactable = true;
            Debug.Log("button off screen");
        }
        else
        {
            Debug.Log("button on screen");
            this.gameObject.GetComponent<Button>().interactable = false;
        }
        */
        
        
    }

    public void OnClick() {
        if (containerColor == buttonColor) {
//            Debug.Log("this is the right button");
            isCorrectButton = true;
        }
        else if (containerColor != buttonColor) {
//            Debug.Log("this is the wrong button");
            isCorrectButton = false;
           GameManager.gameLives--;
            if (GameManager.gameLives <= 0)
            truckCrash = true;

            AudioManager.instance.PlaySound("sortWrong");

            for (int i = 0; i < GameManager.noOfButtons; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
        }

        if (GameManager.startingTruckSequenceFinished == true && truckCrash == false && isCorrectButton == true) {
            AudioManager.instance.PlaySound("sortSuccess");
            AudioManager.instance.PlaySound("whoosh");
            //            Debug.Log("matching!");
            GameManager.gameScore += 90 + (10 * TruckSpawner.GetLevel());
            Destroy(containerSprite);
            truckCrash = false;
        }
	}


}
