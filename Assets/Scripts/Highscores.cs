using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Highscores : MonoBehaviour {

    public Text symScore;
    public Text sortScore;
    public Text crateFishScore;
    // Use this for initialization
    void Start() {
        symScore.text =  PlayerPrefs.GetInt("Symmetry Highscore")+ "";
        sortScore.text = PlayerPrefs.GetInt("Sortify Highscore") + "";
        crateFishScore.text = PlayerPrefs.GetInt("Catching Items Highscore") + "";
    }

    public void exitHighScore()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        this.gameObject.SetActive(false);
        print("clicked exit");
    }

    public void openHighScore()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        this.gameObject.SetActive(true);
    }

    public void backToMap()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
}
