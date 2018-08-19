using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class showScoreSort : MonoBehaviour {


    public int oneStar;
    public int twoStar;
    public int threeStar;
    public string highscore;

    [SerializeField]
    private string starFrom;

    [SerializeField]
    private GameObject doneLoading;

    void Start () {

        

        if (GameManager.gameScore >= oneStar)
        {
            StartCoroutine(display());
            AudioManager.instance.PlayCommonSound("Stage Cleared");
 //           Debug.Log("score enough for one star");
            
        }

        else if(PlayerPrefs.GetInt(highscore) >= oneStar)
        {
//            Debug.Log("highscore enough for one star");
            StartCoroutine(display());
            AudioManager.instance.PlayCommonSound("Stage Failed");

        }
        else
        {
            //            Debug.Log("not enough for one star");
            AudioManager.instance.PlayCommonSound("Stage Failed");
            Debug.Log(PlayerPrefs.GetInt(highscore));
            StartCoroutine(gameOverDisplay());
        }
    }

    IEnumerator display()
    {
        transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitUntil(() => doneLoading.GetComponent<ScoreScriptSortify>().doneLoading);
        if (transform.GetChild(3).GetComponent<ScoreScriptSortify>().score > oneStar)
        {
            if (PlayerPrefs.GetInt(starFrom) < 1) // And this 
            {
                PlayerPrefs.SetInt(starFrom, 1);
            }
            transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            AudioManager.instance.PlayCommonSound("Star 1");
        }
        if (transform.GetChild(3).GetComponent<ScoreScriptSortify>().score > twoStar)
        {
            if (PlayerPrefs.GetInt(starFrom) < 2) // And this 
            {
                PlayerPrefs.SetInt(starFrom, 2);
            }
            yield return new WaitForSeconds(0.3f);
            transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
            AudioManager.instance.PlayCommonSound("Star 2");
        }
        if (transform.GetChild(3).GetComponent<ScoreScriptSortify>().score > threeStar)
        {
            if (PlayerPrefs.GetInt(starFrom) < 3) // And this 
            {
                PlayerPrefs.SetInt(starFrom, 3);
            }
            yield return new WaitForSeconds(0.3f);
            transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
            AudioManager.instance.PlayCommonSound("Star 3");
        }
        yield return new WaitForSeconds(1.2f);
        transform.GetChild(4).gameObject.SetActive(true);

        if (PlayerPrefs.GetInt("haveWonGame2") == 0)
        {
            print("entered if loop");
            yield return new WaitForSeconds(1);
//            Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " win");
            AudioManager.instance.StopSound("BGM");
            AudioManager.instance.StopSound("BGM1.2");
            AudioManager.instance.StopSound("BGM1.5");
            AudioManager.instance.StopSound("Score Counting");
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            PlayerPrefs.SetInt("chatScene", 3);
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
        yield return new WaitForSeconds(0.3f);
        transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        yield return new WaitForSeconds(1.2f);
        transform.GetChild(4).gameObject.SetActive(true); // highscore

		if ((PlayerPrefs.GetInt("haveLostGame2") == 0) && (PlayerPrefs.GetInt("haveWonGame2") == 0))
		{
			Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " lose");
			yield return new WaitForSecondsRealtime(1f);
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            AudioManager.instance.StopSound("BGM");
            AudioManager.instance.StopSound("BGM1.2");
            AudioManager.instance.StopSound("BGM1.5");
            AudioManager.instance.StopSound("Score Counting");
            PlayerPrefs.SetInt("chatScene", 4);
		}
		else
		{
            Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " win");
			yield return new WaitForSecondsRealtime(1.2f);
			transform.GetChild(5).gameObject.SetActive(true);   //activate Buttons
			transform.GetChild(5).gameObject.transform.GetChild(1).gameObject.SetActive(true);
		}
    }
}
