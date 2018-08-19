using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {

    [SerializeField]
    private GameObject waveBar;

    [SerializeField]
    private GameObject shipIcon;

    [SerializeField]
    private GameObject left;

    [SerializeField]
    private GameObject scoreScreen;

    [SerializeField]
    private Text scoreText;

    public int changingScore = 0;
    public int score = 0;

    private bool gameOverActive = false;

    private float timeLimit = 30f;
    private float elapsedTime = 0f;

    private bool addingScore = false;

    private Image wave;

    private Vector3 travelDistance;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    private void Start()
    {
        //StartCoroutine(test());
        travelDistance = new Vector3(415, 0, 0);
        //travelDistance = new Vector3(450, 0, 0);
        startPosition = shipIcon.transform.localPosition;
        targetPosition = startPosition - travelDistance;
        wave = waveBar.GetComponent<Image>();
    }
    
    private void Update()
    {
        if (left.GetComponent<AddButtonsL>().timeStart)
        {
            if (timeLimit - elapsedTime > 0 && !transform.GetChild(6).gameObject.activeSelf)
            {
                elapsedTime += Time.deltaTime;
            }

            float percentDistance = elapsedTime / timeLimit;
            shipIcon.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, percentDistance);


            if (left.GetComponent<AddButtonsL>().addScore && !addingScore)
            {
                addingScore = true;
                changingScore += (int)(timeLimit - elapsedTime) * 13320 / (int)Mathf.Pow(timeLimit,2) + 30000/(int)timeLimit;
                left.GetComponent<AddButtonsL>().addScore = false;
            }
            else if (!left.GetComponent<AddButtonsL>().addScore && addingScore)
            {
                addingScore = false;
                restart();
            }

            if (changingScore > score)
            {
                score +=123;
            }
            else
            {
                score = changingScore;
            }

            scoreText.text = "Score: " + score;
        }
        else
        {
            wave.fillAmount = 1;
            shipIcon.transform.localPosition = startPosition;
            if (changingScore > score)
            {
                score+=123;
            }

            scoreText.text = "Score: " + score;
        }

        // GAMEOVER
        if(timeLimit - elapsedTime <= 0 && !gameOverActive)
        {
            StartCoroutine(activateGameOver());
            transform.GetChild(0).gameObject.SetActive(true);
            shipIcon.GetComponent<Animation>().Stop();
            shipIcon.transform.rotation = Quaternion.Slerp(shipIcon.transform.rotation, Quaternion.Euler(0, 0, -85), Time.deltaTime * 0.5f);
            left.GetComponent<AddButtonsL>().makeUninteractable();
            gameOverActive = true;
        }
    }

    public void restart()
    {
        shipIcon.transform.localPosition = startPosition;
        elapsedTime = 0;
        timeLimit -= 5;
    }

    IEnumerator activateGameOver()
    {
        AudioManager.instance.PlaySound("TimesUp");
        //transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        scoreScreen.SetActive(true);
    }

    IEnumerator test()
    {
        Resolution[] resolutions = Screen.resolutions;
        foreach (Resolution res in resolutions)
        {
            print(res.width + "x" + res.height);
        }
        Debug.Log(Screen.currentResolution);
        Debug.Log(Screen.resolutions[0].width);
        Debug.Log(Screen.resolutions[0].height);
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
        Debug.Log("set reso");
        Screen.SetResolution(1280, 800, true);
        yield return new WaitForEndOfFrame();
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
    }

}
