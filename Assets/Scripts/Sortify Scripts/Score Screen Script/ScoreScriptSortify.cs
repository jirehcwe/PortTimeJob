using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScriptSortify : MonoBehaviour {


    [SerializeField]
    private GameObject GM;
    public int score;
    private int changeScore;
    private float add;
    public bool doneLoading = false;

    // Use this for initialization
    void Start()
    {
        score = GameManager.gameScore;
    }

    private void Update()
    {
        if (changeScore < score)
        {
            add = score / 13;
            changeScore += (int)add;
            GetComponent<Text>().text = "" + changeScore;
            AudioManager.instance.PlayCommonSound("Score Counting");
        }
        else
        {
            doneLoading = true;
            GetComponent<Text>().text = "" + score;
            AudioManager.instance.StopCommonSound("Score Counting");
        }
    }

}
