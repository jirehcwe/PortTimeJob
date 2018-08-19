using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMainMenuButton : MonoBehaviour {

    private bool hasSavedFile = false;

    private void Start()
    {
		AudioManager.instance.PlaySound ("MMMenu BGM");

        if(PlayerPrefs.GetString("gender") == "")
        {
            transform.GetChild(1).GetComponent<Animator>().speed = 0; 
            transform.GetChild(1).GetComponent<Button>().interactable = false;
        }
        else
        {
            hasSavedFile = true;
        }
    }

    public void newGame()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        if (hasSavedFile)
        {
            transform.parent.GetChild(2).gameObject.SetActive(true);
        }
        else // No saved files
        {
            nextScene();
        }

    }

    public void nextScene()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        // Story playerPrefs
        PlayerPrefs.SetInt("chatScene", 0);
		PlayerPrefs.SetInt("haveWonGame0", 0);
        PlayerPrefs.SetInt("haveWonGame1", 0);
        PlayerPrefs.SetInt("haveLostGame1", 0);
        PlayerPrefs.SetInt("haveWonGame2", 0);
        PlayerPrefs.SetInt("haveLostGame2", 0);
        PlayerPrefs.SetInt("haveWonGame3", 0);
        PlayerPrefs.SetInt("haveLostGame3", 0);
        PlayerPrefs.SetInt("haveFinishedGame", 0);
        PlayerPrefs.SetString("gender", "");

        // Score playerPrefs
        PlayerPrefs.SetInt("Symmetry Highscore", 0);
        PlayerPrefs.SetInt("Catching Items Highscore", 0);
        PlayerPrefs.SetInt("Sortify Highscore", 0);

        // Stars playerPrefs
        PlayerPrefs.SetInt("Symmetry Stars", 0);
        PlayerPrefs.SetInt("Sortify Stars", 0);
        PlayerPrefs.SetInt("CatchingItems Stars", 0);

        // Scene Change
        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);

        Debug.Log("reset");
    }

    public void resumeGame()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        // Scene Change
        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }

    public void backtoScene()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        transform.parent.GetChild(2).gameObject.SetActive(false);
    }
}
