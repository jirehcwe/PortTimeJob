using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;

public class instructionTextBehaviour : MonoBehaviour {
    private Text instructionText;
    public bool isPrinting;
    public bool finishedPrinting = false;
    public bool isPressed = false;
    public int sentence = 0;
    public string printString;
    public GameObject tick;
    public GameObject cross;
    public GameObject leftCon;
    public GameObject rightCon;
    public Color Red = new Color32(217, 56, 49, 255);
    public Color Blue = new Color32(35, 156, 207, 255);
    public Color Yellow = new Color32(223, 213, 25, 255);
    public Color Green = new Color32(0, 152, 74, 255);
    public GameObject primerMover;
    public GameObject anim;
    public GameObject tapForInst;
    public GameObject skipInst;


    bool hasSwapped = false;
    Sprite temp;



    private string[] instructions = {"Hi there! You must be here to help Mr. Citos sort the prime movers going out of the port!",
                                     "PSA sorts 190,000 containers a day, and some of them are moved by prime movers!",
                                     "Since Mr. Citos is dow--I mean, busy, help us to sort the containers by tapping on the color that matches the container on the mover!",
                                     "Good luck sorting those prime movers!"};


    // Use this for initialization
    void Start () {
        instructionText = GetComponent<Text>();

        isPrinting = false;
        finishedPrinting = true;
        sentence = 0;
    }

    // Update is called once per frame
    void Update() {
        instructionText.text = printString;
        if (sentence == 4)
        {
            StartCoroutine(waitThenFinish());
        }

        if ((Input.GetMouseButtonDown(0)))  //NEED TO ADD TOUCH
        {
            if (sentence >= 0 && sentence != 3)
            tapForInst.GetComponent<Text>().text = "NEXT >";

            if (sentence == 1)
            skipInst.GetComponent<Button>().interactable = true;

            if (!isPrinting)
            {
                StartCoroutine(animateText(instructions[sentence]));
                    AudioManager.instance.PlayCommonSound("click");
            }

        }

       /* try
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isPressed = true;
            }
            else
            {
                isPressed = false;
            }
        } catch (Exception e)
        {
            //String s = e.Message;
            print(e.Message);
        }*/
        

        if (sentence == 2)
        {
            leftCon.GetComponent<Image>().color = Yellow;
            rightCon.GetComponent<Image>().color = Red;
            primerMover.transform.GetChild(0).gameObject.GetComponent<Image>().color = Red;
            if (!hasSwapped)
            {
                hasSwapped = true;
                temp = tick.GetComponent<Image>().sprite;
                tick.GetComponent<Image>().sprite = cross.GetComponent<Image>().sprite;
                cross.GetComponent<Image>().sprite = temp;
            }
        }


    }

    IEnumerator animateText(string text)
    {
        isPrinting = true;
        finishedPrinting = false;

        yield return new WaitForSeconds(0.01F);
        int i = 0;
        printString = "";
        while (i < text.Length)
        {
            if (i < text.Length)
            {
                printString += text[i++];
                yield return new WaitForSeconds(0.01F);
            }
        }
        isPrinting = false;
        finishedPrinting = true;
        sentence++;
        
    }

    IEnumerator waitThenFinish()
    {
        yield return new WaitForSeconds(1);
        this.transform.GetComponentInParent<PosterBehaviour>().textFinished = true;
        anim.SetActive(false);
    }

}
