using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArrowScript : MonoBehaviour
{

	private float translateDistance = 10f;
	public static bool enterIf = true;
	public static bool toggle = false;
	private float rightArrowDelayTime = 0.2f;
	private Vector3 startposition;

	private void Start()
	{
		startposition = transform.position;
        enterIf = true;
	}

	public void Restart()
	{
		transform.position = startposition;
		enterIf = true;
		toggle = false;
	}

	void Update()
	{
		StartCoroutine(MoveRightArrow());
	}

	IEnumerator MoveRightArrow()
	{
		if (enterIf)
		{
            enterIf = false;
			if (toggle == true)
			{
				transform.Translate(translateDistance, 0, 0);
				toggle = false;
			}
			else
			{
				transform.Translate(-translateDistance, 0, 0);
				toggle = true;
			}
			yield return new WaitForSeconds(rightArrowDelayTime);
			enterIf = true;
		}
	}
}
