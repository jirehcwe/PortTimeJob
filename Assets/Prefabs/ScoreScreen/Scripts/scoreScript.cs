using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreScript : MonoBehaviour {

    [SerializeField]
    private GameObject timer;
    public int score;
    private int changeScore;

    public bool doneLoading = false;

	// Use this for initialization
	void Start () {
        score = timer.GetComponent<TimerScript>().changingScore;
	}

    private void Update()
    {
        if(changeScore < score)
        {
            GetComponent<Text>().text = "" + changeScore;
            changeScore+=317;
        }
        else
        {
            GetComponent<Text>().text = "" + score;
            doneLoading = true;
        }
    }

}
