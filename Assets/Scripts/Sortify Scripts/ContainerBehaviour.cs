using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContainerBehaviour : MonoBehaviour {

	Color randColor;
	public GameObject truckspawner;
    public Color Red = new Color32(217, 56, 49, 255);
    public Color Blue = new Color32(35, 156, 207, 255);
    public Color Yellow = new Color32(223, 213, 25, 255);
    public Color Green = new Color32(0, 152, 74, 255);

	// Use this for initialization
	void Start () {
		randColor = randomColor ();
		this.gameObject.GetComponent<SpriteRenderer>().color = randColor;
    }


	public Color randomColor(){
		int random = Random.Range (0, TruckSpawner.categories);
		switch (random) {
		case 0:
                randColor = Red;
			break;
		case 1:
                randColor = Blue;
			break;
		case 2:
                randColor = Yellow;
			break;
		case 3:
                randColor = Green;
			break;
		}
		return randColor;
	}
}
