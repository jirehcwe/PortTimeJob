using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowScore_YF : MonoBehaviour {

	public int oneStar;
	public int twoStar;
	public int threeStar;

	[SerializeField]
	private GameObject ScoreTimer;

	[SerializeField]
	private GameObject doneLoading;

	[SerializeField]
	private string starFrom;

	void Start () {
		if (YFGameManager.score > oneStar || YFGameManager.parcelsCaught >= 35) {
			StartCoroutine(display());
            print("won game 1");
		} else {
			StartCoroutine(gameOverDisplay());
            print("lost game 1");
		}
	}

	IEnumerator display()
	{
		transform.GetChild(2).gameObject.SetActive(true);	//activate Stage Cleared
		AudioManager.instance.PlayCommonSound("Stage Cleared");
		yield return new WaitForSecondsRealtime(0.3f);
		transform.GetChild(3).gameObject.SetActive(true);	//activate ScoreNum
		AudioManager.instance.PlayCommonSound("Score Counting");
		yield return new WaitUntil(() => doneLoading.GetComponent<ScoreScript_YF>().doneLoading); // wait for score to finish loading
		AudioManager.instance.StopCommonSound("Score Counting");
		if (YFGameManager.score > oneStar || YFGameManager.parcelsCaught >= 35)
		{
			AudioManager.instance.PlayCommonSound("Star 1");
			if(PlayerPrefs.GetInt(starFrom) < 1)
			{
				PlayerPrefs.SetInt(starFrom, 1);
			}
			transform.GetChild(0).GetChild(0).gameObject.SetActive(true);	//activate first star
		}
		if (YFGameManager.score > twoStar)
		{
			if (PlayerPrefs.GetInt(starFrom) < 2)
			{
				PlayerPrefs.SetInt(starFrom, 2);
			}
			yield return new WaitForSecondsRealtime(0.6f);
			AudioManager.instance.PlayCommonSound("Star 2");
			transform.GetChild(0).GetChild(1).gameObject.SetActive(true);	//activate second star
		}
		if (YFGameManager.score > threeStar)
		{
			if (PlayerPrefs.GetInt(starFrom) < 3)
			{
				PlayerPrefs.SetInt(starFrom, 3);
			}
			yield return new WaitForSecondsRealtime(0.6f);
			AudioManager.instance.PlayCommonSound("Star 3");
			transform.GetChild(0).GetChild(2).gameObject.SetActive(true);	//activate third star
		}
		yield return new WaitForSecondsRealtime(1.2f);
		transform.GetChild(4).gameObject.SetActive(true);	//activate HighscoreNum
		yield return new WaitForSecondsRealtime(1);
		transform.GetChild (7).gameObject.SetActive (true);	//activate items caught
		yield return new WaitForSecondsRealtime(0.3f);
		transform.GetChild(7).GetChild(0).gameObject.SetActive(true);	//activate parcel
		yield return new WaitForSecondsRealtime(1.1f);
		transform.GetChild(7).GetChild(0).GetChild(0).gameObject.SetActive(true);	//activate parcel score
		yield return new WaitForSecondsRealtime(0.5f);
		transform.GetChild(7).GetChild(1).gameObject.SetActive(true);	//activate junk
		yield return new WaitForSecondsRealtime(1.1f);
		transform.GetChild(7).GetChild(1).GetChild(0).gameObject.SetActive(true);	//activate junk score

		if (PlayerPrefs.GetInt("haveWonGame1") == 0)
		{
			yield return new WaitForSecondsRealtime(1);
			Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " win");
            Time.timeScale = 1;
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);

			PlayerPrefs.SetInt("chatScene", 1);
		}else{
			yield return new WaitForSecondsRealtime(1.2f);
			transform.GetChild(5).gameObject.SetActive(true);	//activate Buttons
			transform.GetChild(5).gameObject.transform.GetChild(1).gameObject.SetActive(true);	//activate Forward button
		}
	}

	IEnumerator gameOverDisplay()
	{
		transform.GetChild(6).gameObject.SetActive(true);	//activate Stage Failed
		AudioManager.instance.PlayCommonSound("Stage Failed");
		yield return new WaitForSecondsRealtime(0.3f);
		transform.GetChild(3).gameObject.SetActive(true);	//activate ScoreNum
		AudioManager.instance.PlayCommonSound("Score Counting");
		yield return new WaitUntil(() => doneLoading.GetComponent<ScoreScript_YF>().doneLoading); // wait for score to finish loading
		AudioManager.instance.StopCommonSound("Score Counting");
		transform.GetChild(4).gameObject.SetActive(true);	//activate HighscoreNum
		yield return new WaitForSecondsRealtime(1);
		transform.GetChild (7).gameObject.SetActive (true);	//activate items caught
		yield return new WaitForSecondsRealtime(0.3f);
		transform.GetChild(7).GetChild(0).gameObject.SetActive(true);	//activate parcel
		yield return new WaitForSecondsRealtime(1.1f);
		transform.GetChild(7).GetChild(0).GetChild(0).gameObject.SetActive(true);	//activate parcel score
		yield return new WaitForSecondsRealtime(0.5f);
		transform.GetChild(7).GetChild(1).gameObject.SetActive(true);	//activate junk
		yield return new WaitForSecondsRealtime(1.1f);
		transform.GetChild(7).GetChild(1).GetChild(0).gameObject.SetActive(true);	//activate junk score

        //print(PlayerPrefs.GetInt("haveWonGame1"));
		//print(PlayerPrefs.GetInt("haveLostGame1"));

		if ((PlayerPrefs.GetInt("haveLostGame1") == 0) && (PlayerPrefs.GetInt("haveWonGame1") == 0))
		{
			Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " loss");
            yield return new WaitForSecondsRealtime(1f);
            Time.timeScale = 1;
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);

			PlayerPrefs.SetInt("chatScene", 2);
		}else{
			Debug.Log("showScore script detected " + SceneManager.GetActiveScene().name + " loss");
			yield return new WaitForSecondsRealtime(1.2f);
			transform.GetChild(5).gameObject.SetActive(true);	//activate Buttons
		}
	}
}