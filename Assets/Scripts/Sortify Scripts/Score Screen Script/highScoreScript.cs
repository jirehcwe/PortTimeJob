using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class highScoreScript : MonoBehaviour
{
    [SerializeField]
    private GameObject GM;
    public string highscore;

    private void Awake()
    {
        //PlayerPrefs.SetInt("Sortify Highscore", 0);
//        Debug.Log(GameManager.gameScore);
//        Debug.Log(PlayerPrefs.GetInt(highscore));
        if (GameManager.gameScore > PlayerPrefs.GetInt(highscore))
        {
            PlayerPrefs.SetInt(highscore, GameManager.gameScore);
            AudioManager.instance.PlayCommonSound("Highscore");

        }

        GetComponent<Text>().text = "" + PlayerPrefs.GetInt(highscore);
    }
}
