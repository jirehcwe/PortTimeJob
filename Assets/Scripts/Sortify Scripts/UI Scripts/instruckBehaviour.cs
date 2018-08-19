using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class instruckBehaviour : MonoBehaviour {
    private Color randColor;

    public float colorTimer;
    private int colorloop;

    public Color Red = new Color32(217, 56, 49, 255);
    public Color Blue = new Color32(35, 156, 207, 255);
    public Color Yellow = new Color32(223, 213, 25, 255);
    public Color Green = new Color32(0, 152, 74, 255);
    // Use this for initialization
    void Start () {
        colorTimer = 0;
        colorloop = 0;
    }
	
	// Update is called once per frame
	void Update () {
        colorTimer += UnityEngine.Time.deltaTime;
        if (colorTimer >= 0.8f)
        {
            randColor = randomColor();
            this.gameObject.GetComponent<Image>().color = randColor;
            colorTimer = 0;
            colorloop++;
        }

        if (colorloop == 4)
        {
            colorloop = 0;
        }
        
    }

    public Color randomColor()
    {
        switch (colorloop)
        {
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
