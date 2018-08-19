using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour {

    public void SortifyScene()
    {
        PlayerPrefs.SetInt("toScene", 3);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }

    public void SymmetryScene()
    {
        PlayerPrefs.SetInt("toScene", 4);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }

    public void CrateFishingScene()
    {
        PlayerPrefs.SetInt("toScene", 1);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
    public void MenuScene()
    {
        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
    public void DataScene()
    {
        PlayerPrefs.SetInt("toScene",5);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }

}
