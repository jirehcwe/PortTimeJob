using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class loadingTextScript : MonoBehaviour {

    private string[] loadingTexts = { "Loading Crates", "Fixing Cranes", "Docking Vessels", "Programming AGVs", "Managing Databases" , "Calibrating PortNet", "Scanning Trucks", "Balancing Ships", "Unlatching Cargo", "Cooling Reefer Units"};
    private string currentLoadingText;
    private bool dotting = false;
    private int stringNum;
    private int dotCount = 0;

	void Start () {
        stringNum = Random.Range(0, loadingTexts.Length);
        currentLoadingText = loadingTexts[stringNum];
	}

	void Update () {
        GetComponent<Text>().text = currentLoadingText;
        if (!dotting)
        {
            if (dotCount < 3)
            {
                StartCoroutine(dotText());
                dotCount++;
            }
            else
            {
                StartCoroutine(clearDot());
                dotCount = 0;
            }
        }
	}

    IEnumerator dotText()
    {
        dotting = true;
        yield return new WaitForSeconds(0.3f);
        currentLoadingText += ".";
        dotting = false;
    }

    IEnumerator clearDot()
    {
        dotting = true;
        yield return new WaitForSeconds(0.3f);
        currentLoadingText = loadingTexts[stringNum];
        dotting = false;
    }
}
