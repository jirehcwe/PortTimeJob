using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStars : MonoBehaviour {

    [SerializeField]
    private Sprite[] stars;
    // can be locked location pin too

    [SerializeField]
    private string starFrom;

    [SerializeField]
    private Button button;

    [SerializeField]
    private string haveWonString_gameBefore;

    private void Start()
    {
        // if have not won previous game
        if (PlayerPrefs.GetInt(haveWonString_gameBefore)==0)
        {
			// Debug.Log(starFrom + ": yellow lock");
			button.interactable = false;    
            GetComponent<SpriteRenderer>().sprite = stars[4];
        }

        // have won previous game
        else
        {
			switch (PlayerPrefs.GetInt(starFrom))
			{
				case 0:
					// Debug.Log(starFrom + ": 0 stars");
					GetComponent<SpriteRenderer>().sprite = stars[0];
					break;
				case 1:
					// Debug.Log(starFrom + ": 1 stars");
					GetComponent<SpriteRenderer>().sprite = stars[1];
					break;
				case 2:
					// Debug.Log(starFrom + ": 2 stars");
					GetComponent<SpriteRenderer>().sprite = stars[2];
					break;
				case 3:
					// Debug.Log(starFrom + ": 3 stars");
					GetComponent<SpriteRenderer>().sprite = stars[3];
					break;
				default:
					// Debug.Log("PlayerPrefs value out of array.");
					break;
			}   
        }
    }
}
