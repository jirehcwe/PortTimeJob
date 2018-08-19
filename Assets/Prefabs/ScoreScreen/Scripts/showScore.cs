using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class showScore : MonoBehaviour {


    public int oneStar;
    public int twoStar;
    public int threeStar;

    [SerializeField]
    private GameObject ScoreTimer;

    [SerializeField]
    private GameObject doneLoading;

    [SerializeField]
    private string starFrom;   //latest update

    void Start () {
        if (ScoreTimer.GetComponent<TimerScript>().changingScore > oneStar)
        {
            StartCoroutine(display());
        }
        else
        {
            StartCoroutine(gameOverDisplay());
        }
    }

    IEnumerator display()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        AudioManager.instance.PlayCommonSound("Stage Cleared");
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(3).gameObject.SetActive(true);
        AudioManager.instance.PlayCommonSound("Score Counting");
        yield return new WaitUntil(() => doneLoading.GetComponent<scoreScript>().doneLoading); // wait for score to finish loading
        AudioManager.instance.StopCommonSound("Score Counting");

        if (transform.GetChild(3).GetComponent<scoreScript>().score > oneStar)
        {
            AudioManager.instance.PlayCommonSound("Star 1");
            if (PlayerPrefs.GetInt(starFrom) < 1) // And this 
            {
                PlayerPrefs.SetInt(starFrom, 1);
            }
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        }
        if (transform.GetChild(3).GetComponent<scoreScript>().score > twoStar)
        {
            if (PlayerPrefs.GetInt(starFrom) < 2)// And this 
            {
                PlayerPrefs.SetInt(starFrom, 2);
            }
            yield return new WaitForSeconds(0.6f);
            AudioManager.instance.PlayCommonSound("Star 2");
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        }
        if (transform.GetChild(3).GetComponent<scoreScript>().score > threeStar)
        {
            
            if (PlayerPrefs.GetInt(starFrom) < 3)// And this 
            {
                PlayerPrefs.SetInt(starFrom, 3);
            }
            yield return new WaitForSeconds(0.6f);
            AudioManager.instance.PlayCommonSound("Star 3");
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        }

		yield return new WaitForSeconds(1.2f);
		transform.GetChild(4).gameObject.SetActive(true);

		if (PlayerPrefs.GetInt("haveWonGame3") == 0)
        {
            AudioManager.instance.StopCommonSound("SaltyDitty");
            AudioManager.instance.StopCommonSound("SymmetryBGM");
            yield return new WaitForSeconds(1);
			Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " win");
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            AudioManager.instance.StopSound("BGM");
            AudioManager.instance.StopSound("BGM1.2");
            AudioManager.instance.StopSound("BGM1.5");
            AudioManager.instance.StopSound("Score Counting"); 

			PlayerPrefs.SetInt("chatScene", 5);
		}
		else
        {
            yield return new WaitForSeconds(1);
			transform.GetChild(5).gameObject.SetActive(true);
			transform.GetChild(5).gameObject.transform.GetChild(1).gameObject.SetActive(true);
		}

    }

    IEnumerator gameOverDisplay()
    {
        transform.GetChild(6).gameObject.SetActive(true);
        AudioManager.instance.PlayCommonSound("Stage Failed");
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitUntil(() => doneLoading.GetComponent<scoreScript>().doneLoading);
        yield return new WaitForSeconds(1.2f);
        transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(1);

		if ((PlayerPrefs.GetInt("haveLostGame3") == 0) && (PlayerPrefs.GetInt("haveWonGame3") == 0))
		{
            AudioManager.instance.StopCommonSound("SaltyDitty");
            AudioManager.instance.StopCommonSound("SymmetryBGM");
            yield return new WaitForSeconds(1);
    		Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " lose");
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            PlayerPrefs.SetInt("chatScene", 6);
		}
		else
        {
            // restart button appear
            transform.GetChild(5).gameObject.SetActive(true);
		}
    }
}
