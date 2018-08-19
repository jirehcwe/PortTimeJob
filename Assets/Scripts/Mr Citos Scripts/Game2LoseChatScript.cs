using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game2LoseChatScript : MonoBehaviour
{
	public bool isChatButtonClicked = false;
	public static string gender;
	public static int sequence = 1; // starts with strArray chat
	public bool runLoop = true;
	private GameObject chatBoxAfterGame2Lost;
	private GameObject downArrow;
	public Text uiText;
	public bool isPrinting = false;
	public bool donePrinting = false;
	public float charPrintDelay = 0.01f;
	public char[] strArraySplit;
	public char[] textToPrintSplit;
	public int whichString = 0;
    public string[] strArray ={
        "Mr Citos: Come on "+gender+"!",
        "Mr Citos: You can’t even manage a simple sorting operation!",
        "Mr Citos: It can’t be that hard, can it?",
        "Mr Citos: Ah well! Whenever you’re ready, let’s have another go at it, shall we?"
    };

	// these need to be changed to the location pins !!!
	public GameObject button;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;


	void Start()
	{
		if ((PlayerPrefs.GetInt("haveLostGame2") == 1) || (PlayerPrefs.GetInt("haveWonGame2") == 1))
		{
			transform.parent.gameObject.SetActive(false);
		}
		else
		{
			button.SetActive(false);
			button1.SetActive(false);
			button2.SetActive(false);
			button3.SetActive(false);
		}
        Camera.main.gameObject.GetComponent<drag_cam>().enabled = false;
		chatBoxAfterGame2Lost = transform.parent.parent.GetChild(11).gameObject;
		gender = PlayerPrefs.GetString("gender");
		downArrow = transform.parent.GetChild(0).gameObject;
		downArrow.SetActive(true);

		// set reference to UIText
		uiText = GetComponent<Text>();

        strArray[0] = "Mr Citos: Come on " + gender + "!";
        strArray[1] = "Mr Citos: You can’t even manage a simple sorting operation!";
		strArray[2] = "Mr Citos: It can’t be that hard, can it?";
        strArray[3] = "Mr Citos: Ah well! Whenever you’re ready, let’s have another go at it, shall we?";

		// split strArray into a char array and store into strArraySplit
		strArraySplit = strArray[whichString].ToCharArray();

		// loads the char array into textToPrintSplit
		textToPrintSplit = new char[strArraySplit.Length];

		PlayerPrefs.SetInt("haveLostGame2", 1);
		Debug.Log("lost game 2");
		Debug.Log("haveLostGame2= " + PlayerPrefs.GetInt("haveLostGame2"));
	}

	void Update()
	{
		// after strArray is fully printed
		if ((whichString == strArray.Length) && (runLoop))
		{
			// prevent re-entering this loop
			runLoop = false;

			// set sequence to 2
			sequence = 2;
		}

		switch (sequence)
		{
			// printing strArray1
			case 1:
				if (isChatButtonClicked)
				{
					isChatButtonClicked = false;
					if (!isPrinting)
                    {
                        AudioManager.instance.PlayCommonSound("click");
						textToPrintSplit = new char[strArraySplit.Length];
						StartCoroutine(PrintText());
					}
					else
					{
						if (!donePrinting)
                        {
                            AudioManager.instance.PlayCommonSound("click");
							StopCoroutine(PrintText());
							textToPrintSplit = strArraySplit;
							string s = new string(textToPrintSplit);
							uiText.text = s;
							donePrinting = true;
						}
						else
						{
							whichString++;
							strArraySplit = strArray[whichString].ToCharArray();
							textToPrintSplit = new char[strArraySplit.Length];
							isPrinting = false;
						}
					}
				}
				break;

			default:
				if (isChatButtonClicked)
				{
					isChatButtonClicked = false;

					button.SetActive(true);
					button1.SetActive(true);
					button2.SetActive(true);
					button3.SetActive(true);

                    Camera.main.gameObject.GetComponent<drag_cam>().enabled = true;
					chatBoxAfterGame2Lost.SetActive(false);
				}
				break;
		}
	}

	// these are the coroutines
	IEnumerator PrintText()
	{
		isPrinting = true;
		donePrinting = false;

		for (int i = 0; i < strArraySplit.Length; i++)
		{
			textToPrintSplit[i] = strArraySplit[i];
			string s = new string(textToPrintSplit);
			uiText.text = s;
			if (donePrinting)
			{
				yield break;
			}
			yield return new WaitForSeconds(charPrintDelay);
		}
		// needs to break coroutine somehow

		isPrinting = false;
		donePrinting = true;
		whichString++;
		strArraySplit = strArray[whichString].ToCharArray();
		textToPrintSplit = new char[strArraySplit.Length];
	}

	public void nextChat()
	{
		isChatButtonClicked = true;
	}
}