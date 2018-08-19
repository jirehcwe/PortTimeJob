using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownArrowScript : MonoBehaviour {

    private float translateDistance = 10f;
    public static bool enterIf = true;
    public static bool toggle = false;
    private float downArrowDelayTime = 0.2f;
    private Vector3 startposition;

    private void Start()
    {
        enterIf = true;
        startposition = transform.position;
    }

    public void Restart()
    {
        transform.position = startposition;
        downArrowDelayTime = 0.2f;
		enterIf = true;
		toggle = false;
    }

    void FixedUpdate () {
        StartCoroutine(MoveDownArrow());
    }

    IEnumerator MoveDownArrow()
    {
		if (enterIf)
		{
			enterIf = false;
            if (toggle==true)
            {
                transform.Translate(0, translateDistance, 0);
                toggle = false;
			}
            else
            {
				transform.Translate(0, -translateDistance, 0);
                toggle = true;
			}
            yield return new WaitForSecondsRealtime(downArrowDelayTime);
			enterIf = true;
		}
	}
}
