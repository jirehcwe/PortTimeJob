using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Forward : MonoBehaviour {
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => forward());
    }
    public void forward()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        AudioManager.instance.StopCommonSound("SaltyDitty");
        AudioManager.instance.StopCommonSound("SymmetryBGM");
        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
}
