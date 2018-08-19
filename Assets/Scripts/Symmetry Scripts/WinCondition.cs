using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCondition : MonoBehaviour {
    public GameObject[] leftButtons;
    public GameObject[] rightButtons;

    private void Start()
    {
        getButtonsLeft();
        getButtonsRight();
    }

    public void getButtonsLeft()
    {
        leftButtons = new GameObject[20];
        leftButtons = GameObject.FindGameObjectsWithTag("leftButtons");
    }

    public void getButtonsRight()
    {
        rightButtons = new GameObject[20];
        rightButtons = GameObject.FindGameObjectsWithTag("rightButtons");
    }


    void Update ()
    {
		
	}
}
