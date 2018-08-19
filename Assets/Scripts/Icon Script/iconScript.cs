using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class iconScript : MonoBehaviour {

    private bool fadeOut = false;

    private string printString;
    private string alrightMate = "Alright Mate ";


    private void Start()
    {
        PlayerPrefs.SetInt("toScene", 0);
        print(transform.GetChild(1).GetComponent<Image>().color);
        StartCoroutine(animatateText(alrightMate));
        StartCoroutine(fadeIn());
    }

    private void Update()
    {
        if(printString != alrightMate)
        {
            transform.GetChild(0).GetComponent<Text>().text = printString;
        }

        if (fadeOut)
        {
            Color temp = transform.GetChild(1).GetComponent<Image>().color;
            temp.a += 0.05f;
            if (transform.GetChild(1).GetComponent<Image>().color != Color.white)
            {
                transform.GetChild(1).GetComponent<Image>().color = temp;
            }
            else
            {
                StartCoroutine(nextScene());
            }
        }
    }

    IEnumerator fadeIn()
    {
        yield return new WaitForSeconds(2);
        AudioManager.instance.PlayCommonSound("Yoshi");
        fadeOut = true;
    }

    IEnumerator animatateText(string text)
    {
        yield return new WaitForSeconds(0.1F);
        int i = 0;
        printString = "";
        while(i < text.Length)
        {
            if(i < text.Length)
            {
                printString += text[i++];
                yield return new WaitForSeconds(0.1F);
            }
        }
    }

    IEnumerator nextScene()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.StopCommonSound("Yoshi");
        SceneManager.LoadScene("MainMainMenu", LoadSceneMode.Single);
    }
}

