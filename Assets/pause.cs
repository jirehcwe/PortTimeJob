using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour {
    private AudioSource[] allAudioSources;

    private void StopAllAudio()
    {
        allAudioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        foreach (AudioSource audioS in allAudioSources)
        {
            audioS.Stop();
        }
    }

    public void pauseGame()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void resumeGame()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void goHome()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        StopAllAudio();
        Time.timeScale = 1;
        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }

    public void restartGame()
    {
        Time.timeScale = 1;
        AudioManager.instance.PlayCommonSound("Button Click");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }
}
