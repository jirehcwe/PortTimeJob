using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game3WinChatScript : MonoBehaviour
{
	public bool isChatButtonClicked = false;
	public static string gender;
	public static int sequence = 1; // starts with strArray chat
	public bool runLoop = true;
	public bool runLoop2 = true;
	public bool runLoop3 = true;
	public bool skipClick = false;
	public static string response1 = "";
	private GameObject chatBoxAfterGame3Won;
	private GameObject downArrow;
	public GameObject optionsBox7;
	public Text uiText;
	public bool isPrinting = false;
	public bool donePrinting = false;
	public float charPrintDelay = 0.01f;
	public char[] strArraySplit;
	public char[] textToPrintSplit;
	public int whichString = 0;
	public string[] strArray ={
		"Mr Citos: Well congratulations!",
        "Mr Citos: You made it through the day!",
        "Mr Citos: Easy, wasn't it?",
        "Mr Citos: Surely is for me since I've been running this port every day since 1988!",
        "Mr Citos: Anyway...",
		"Mr Citos: You have just gained access to the highest security building in the compound...",
		"Mr Citos: THE DATA CENTRE!!"
    };
	public char[] strArray2Split;
	public char[] textToPrint2Split;
	public int whichString2 = 0;
	public string[] strArray2 ={
		"Mr Citos: Let's go then!",
        "Mr Citos: I'll meet you there!",
        "Mr Citos: Don't take your time though, I'm busy!",
        "Mr Citos: Always am!"
	};

	public GameObject button;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;

	void Start()
	{
		if (PlayerPrefs.GetInt("haveWonGame3") == 1)
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
		chatBoxAfterGame3Won = transform.parent.parent.GetChild(13).gameObject;
		gender = PlayerPrefs.GetString("gender");
		downArrow = transform.parent.GetChild(0).gameObject;
		downArrow.SetActive(true);
		optionsBox7.SetActive(false);

		// set reference to UIText
		uiText = GetComponent<Text>();

		// split strArray into a char array and store into strArraySplit
		strArraySplit = strArray[whichString].ToCharArray();

		// loads the char array into textToPrintSplit
		textToPrintSplit = new char[strArraySplit.Length];

		PlayerPrefs.SetInt("haveWonGame3", 1);
		Debug.Log("won game 3");
		Debug.Log("haveWonGame3= " + PlayerPrefs.GetInt("haveWonGame3"));
	}

	void Update()
	{
		// after strArray is fully printed
		if ((whichString == strArray.Length) && (runLoop))
		{
			// prevent re-entering this loop
			runLoop = false;

			// setup options box
			SetupOptions7();

			// set sequence to 2
			sequence = 2;
		}

		// after option2 is chosen for the first time
		// if the user chooses option A
		if ((response1 == "A") && (runLoop2))
		{
			runLoop2 = false;
			strArray2Split = strArray2[whichString2].ToCharArray();
			textToPrint2Split = new char[strArray2Split.Length];
			whichString2 = 0;
			isPrinting = false;
			SetupChat();
			sequence = 3;
			skipClick = true;
		}

		// after strArray2 is fully printed
		if ((whichString2 == strArray2.Length) && (runLoop3))
		{
			// prevent re-entering this loop
			runLoop3 = false;

			// set sequence to 2
			sequence = 4;

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

			// waiting for Response1 response
			case 2:
				break;

			// printing strArray2
			case 3:
				if ((isChatButtonClicked) || (skipClick))
				{
					isChatButtonClicked = false;
					skipClick = false;
					if (!isPrinting)
                    {
                        AudioManager.instance.PlayCommonSound("click");
						textToPrint2Split = new char[strArray2Split.Length];
						StartCoroutine(PrintText2());
					}
					else
					{
						if (!donePrinting)
                        {
                            AudioManager.instance.PlayCommonSound("click");
							textToPrint2Split = strArray2Split;
							string s = new string(textToPrint2Split);
							uiText.text = s;
							donePrinting = true;
						}
						else
						{
							whichString2++;
							strArray2Split = strArray2[whichString2].ToCharArray();
							textToPrint2Split = new char[strArray2Split.Length];
							isPrinting = false;
						}
					}
				}
				break;

            // change scene to Sortify upon mouseclick
            default:
                if (isChatButtonClicked)
				{
					isChatButtonClicked = false;

                    button.SetActive(true);
                    button1.SetActive(true);
                    button2.SetActive(true);
                    button3.SetActive(true);

                    Camera.main.gameObject.GetComponent<drag_cam>().enabled = true;
                    chatBoxAfterGame3Won.SetActive(false);

                    PlayerPrefs.SetInt("toScene", 5);
                    SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
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

	IEnumerator PrintText2()
	{
		isPrinting = true;
		donePrinting = false;

		for (int i = 0; i < strArray2Split.Length; i++)
		{
			textToPrint2Split[i] = strArray2Split[i];
			string s = new string(textToPrint2Split);
			uiText.text = s;
			if (donePrinting)
			{
				yield break;
			}
			yield return new WaitForSeconds(charPrintDelay);
		}
		isPrinting = false;
		donePrinting = true;
		whichString2++;
		strArray2Split = strArray2[whichString2].ToCharArray();
		textToPrint2Split = new char[strArray2Split.Length];
	}

	void SetupOptions7()
	{
		downArrow.SetActive(false);
		optionsBox7.SetActive(true);
	}

	void SetupChat()
	{
		downArrow.SetActive(true);
		optionsBox7.SetActive(false);
		downArrow.GetComponent<DownArrowScript>().Restart();
	}

	public void SetResponse1(string chosenResponse1)
	{
		response1 = chosenResponse1;
	}

	public void nextChat()
	{
		isChatButtonClicked = true;
	}
}