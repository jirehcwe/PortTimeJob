using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingImage : MonoBehaviour {

    private bool blinking = true;
    private bool stopBlinking = false;
    private Color color;

    private void Start()
    {
        color = GetComponent<Image>().color;
        AudioManager.instance.PlayCommonSound("SaltyDitty");
        AudioManager.instance.PlayCommonSound("SymmetryBGM");
    }

    private void Update ()
    {
        if (blinking && !stopBlinking)
        {
            StartCoroutine(blinkingroutine());
            blinking = false;
        }
        if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft || Input.GetKeyDown(KeyCode.Space))
        {
            color = Color.white;
            transform.parent.GetChild(0).GetComponent<RawImage>().color = color;
            transform.parent.GetChild(1).gameObject.SetActive(false);
            color.a = 0;
            GetComponent<Image>().color = color;
            transform.parent.GetChild(3).gameObject.SetActive(true);
            Debug.Log("working");
            stopBlinking = true;
            gameObject.SetActive(false);
        }
	}

    IEnumerator blinkingroutine()
    {
        yield return new WaitForSeconds(.5f);
        color.a = 0;
        GetComponent<Image>().color = color;
        yield return new WaitForSeconds(.3f);
        color.a = 1;
        GetComponent<Image>().color = color;
        blinking = true;
    }
}
