using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToTuas : MonoBehaviour {

    private Vector3 tuasPos;
    private Vector3 ppPos;
    private bool move = false;
    private bool move2 = false;

    // Use this for initialization
    void Start () {
        tuasPos = new Vector3(-639, 316, -143.1f);
        ppPos = new Vector3(-79.245f, 42, -143.1f);
	}
	
	// Update is called once per frame
	void Update () {
        if (move)
        {
            Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, tuasPos, 3f * Time.deltaTime);

        }
        if (move2)
        {
            Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, ppPos, 3f * Time.deltaTime);

        }
    }

    public void moveCam()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        move = false;
        move2 = false;
        StopAllCoroutines();
        Camera.main.gameObject.GetComponent<drag_cam>().enabled = false;
        Camera.main.orthographicSize = 60;
        move = true;
    }

    public void moveCamBack()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        move = false;
        move2 = false;
        StopAllCoroutines();
        move2 = true;
        StartCoroutine(delayDragCam());
    }


    IEnumerator delayDragCam()
    {
        yield return new WaitForSeconds(2);
        Camera.main.gameObject.GetComponent<drag_cam>().enabled = true;
        move = false;
        move2 = false;
    }
}
