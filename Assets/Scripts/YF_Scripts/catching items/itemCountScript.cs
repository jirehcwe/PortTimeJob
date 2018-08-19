using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCountScript : MonoBehaviour {

	[SerializeField]
	public int count;
	private int changeCount = 0;

	// Use this for initialization
	void Start () {
		if (gameObject.name == "Parcel Count") {
			count = YFGameManager.parcelsCaught;
		} else if (gameObject.name == "Junk Count") {
			count = YFGameManager.junkCaught;
		}
	}

	private void Update()
	{
		if(changeCount < count)
		{
			GetComponent<Text>().text = "" + changeCount;
			changeCount+=3;
		}
		else
		{
			GetComponent<Text>().text = "" + count;
		}
	}
}
