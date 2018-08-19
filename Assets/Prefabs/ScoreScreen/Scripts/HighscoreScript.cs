using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighscoreScript : MonoBehaviour
{

    [SerializeField]
    public string highscore;
    [SerializeField]
    private GameObject scoreLocation;

    private void Awake()
    {
        //PlayerPrefs.SetInt("Symmetry Highscore", 0);
		if (scoreLocation.GetComponent<scoreScript>().score > PlayerPrefs.GetInt(highscore))
        {
            PlayerPrefs.SetInt(highscore, scoreLocation.GetComponent<scoreScript>().score);
            AudioManager.instance.PlayCommonSound("Highscore");
        }

        GetComponent<Text>().text = "" + PlayerPrefs.GetInt(highscore);
    }
}
