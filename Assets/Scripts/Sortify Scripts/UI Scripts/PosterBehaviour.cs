using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PosterBehaviour : MonoBehaviour {


    public float yLimit = 650;
    public GameObject canvas;
    public GameObject flashText;
    public Text instructionText;
    public GameObject instructions;
    public bool textFinished;
    public GameObject skipButton;
    public Text skipText;

    public float fadeTime = 0;

    public float flashTime = 0;
    
    public float fadeInterpolator = 0;
  



	// Use this for initialization
	void Start () {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        instructions = transform.GetChild(0).gameObject;
        this.transform.GetChild(0).GetComponent<Button>().interactable = false;
    }
	
	// Update is called once per frame
	void Update () {
//       Debug.Log(this.transform.GetComponentInParent<Transform>().position.y);



        if (this.transform.GetComponentInParent<Transform>().position.y <= yLimit)
          
        {

            //            Debug.Log("decoupled parent");
            this.transform.SetParent(canvas.transform);
            if (flashTime >= 0.6f) {
                flashTheText();
                flashTime = 0;
            }
            flashTime += Time.deltaTime;

            
        }
        if (textFinished == true)
        {
            fadeTime += Time.deltaTime;

            if (fadeTime > 0)
            {
                this.GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(1, 0, fadeInterpolator));
                this.GetComponentInChildren<Text>().color = new Color(0, 0, 0, Mathf.Lerp(1, 0, fadeInterpolator));
                this.transform.GetChild(0).GetComponent<Image>().color = new Color(1, 1, 1, Mathf.Lerp(1, 0, fadeInterpolator));
                fadeInterpolator += 0.5f * Time.deltaTime;
            }




            if (fadeTime > 2)
            {
                GameManager.startingTruckSequenceFinished = true;
                this.gameObject.SetActive(false);
            }


        }

    }
    void flashTheText()
    {
        if (flashText.activeInHierarchy)
        {
            flashText.SetActive(false);
        }
        else
        {
            flashText.SetActive(true);
        }
    }

    public void skipInstructions()
    {

        this.gameObject.SetActive(false);
        AudioManager.instance.PlaySound("introFinished");
        GameManager.startingTruckSequenceFinished = true;
    }
}
