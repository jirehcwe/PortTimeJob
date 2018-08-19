using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game1WinChatScript : MonoBehaviour
{
	public bool isChatButtonClicked = false;
	public static string gender;
	public static int sequence = 1; // starts with strArray chat
	public bool runLoop = true;
	public bool runLoop2 = true;
    public bool runLoop3 = true;
    public bool playUnlockSound = true;
    public bool enterCase5 = false;
	public bool skipClick = false;
	public static string response1 = "";
	private GameObject chatBoxAfterGame1Won;
	private GameObject downArrow;
	public GameObject optionsBox3;
	public Text uiText;
	public bool isPrinting = false;
	public bool donePrinting = false;
	public float charPrintDelay = 0.01f;
	public char[] strArraySplit;
	public char[] textToPrintSplit;
	public int whichString = 0;
	public string[] strArray ={
		"Mr Citos: Damn, "+gender+"! Impressive!",
		"Mr Citos: In fact, almost as amazing as I am!",
		"Mr Citos: Your stunning performance has earned you access to the flow-through gates!",
        "Mr Citos: You've also unlocked the Automated Guided Vehicle (AGV) testing bay!",
        "Mr Citos: Be sure to check out these info points soon!",
        "Mr Citos: Oh? I hear you asking where the info points are!",
        "Mr Citos: Just look out for the jumping pink 'i's! They'll take you there right away!"};
	public char[] strArray2Split;
	public char[] textToPrint2Split;
	public int whichString2 = 0;
	public string[] strArray2 ={
		"Mr Citos: Go ahead then! The wharf is open to you anytime!",
		"Mr Citos: Replay crate fishing as many times as you want!",
		"Mr Citos: That'll let you collect all those sweet sweet stars!",
		"Mr Citos: And then, off you go to the Sorting Bay!"
	};
	public char[] strArray3Split;
	public char[] textToPrint3Split;
	public int whichString3 = 0;
	public string[] strArray3 ={
		"Mr Citos: No way! The day has just started.",
		"Mr Citos: Okay then! On your way to the Sorting Bay!",
		"Mr Citos: When you get a wink of free time, remember to replay crate fishing!",
		"Mr Citos: That'll let you collect all those sweet sweet stars!"
	};

    // these need to be changed to the location pins !!!
	public GameObject button;
	public GameObject button1;
	public GameObject button2;
	public GameObject button3;

    Vector3 displacement;
    public Vector3 startPoint = new Vector3(8, 2, -143.1f);
    public Vector3 sortifyPoint = new Vector3(120.94f, 0, -143.1f);
    public float cameraPanSpeed = 5;

    void Start()
    {
		if (PlayerPrefs.GetInt("haveWonGame1") == 1)
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
		chatBoxAfterGame1Won = transform.parent.parent.GetChild(4).gameObject;
		gender = PlayerPrefs.GetString("gender");
		downArrow = transform.parent.GetChild(0).gameObject;
		downArrow.SetActive(true);
		optionsBox3.SetActive(false);

		// set reference to UIText
		uiText = GetComponent<Text>();

		strArray[0] = string.Concat("Mr Citos: Damn, ", gender, "! Impressive!");
		strArray[1] = "Mr Citos: In fact, almost as amazing as I am!";
		strArray[2] = "Mr Citos: Your stunning performance has earned you access to the flow-through gates!";

		// split strArray into a char array and store into strArraySplit
		strArraySplit = strArray[whichString].ToCharArray();

		// loads the char array into textToPrintSplit
		textToPrintSplit = new char[strArraySplit.Length];

        PlayerPrefs.SetInt("haveWonGame1",1);
        Debug.Log("won game 1");
        Debug.Log("haveWonGame1= "+PlayerPrefs.GetInt("haveWonGame1"));
	}

	void Update()
	{
		// after strArray is fully printed
		if ((whichString == strArray.Length) && (runLoop))
		{
			// prevent re-entering this loop
			runLoop = false;

			// setup options box
			SetupOptions3();

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

		// after option2 is chosen for the first time
		// if the user chooses option B
		if ((response1 == "B") && (runLoop2)) // when option2 is first chosen
		{
			runLoop2 = false;
			strArray3Split = strArray3[whichString3].ToCharArray();
			textToPrint3Split = new char[strArray3Split.Length];
			whichString3 = 0;
			isPrinting = false;
			SetupChat();
			sequence = 4;
			skipClick = true;
		}

		// after strArray3 or strArray4 is fully printed
        if (((whichString2 == strArray2.Length) || (whichString3 == strArray3.Length))&&(runLoop3))
		{
            runLoop3 = false;
			sequence = 5;
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

			// printing strArray3
			case 4:
				if ((isChatButtonClicked) || (skipClick))
				{
					isChatButtonClicked = false;
					skipClick = false;
					if (!isPrinting)
                    {
                        AudioManager.instance.PlayCommonSound("click");
						textToPrint3Split = new char[strArray3Split.Length];
						StartCoroutine(PrintText3());
					}
					else
					{
						if (!donePrinting)
                        {
                            AudioManager.instance.PlayCommonSound("click");
							textToPrint3Split = strArray3Split;
							string s = new string(textToPrint3Split);
							uiText.text = s;
							donePrinting = true;
						}
						else
						{
							whichString3++;
							strArray3Split = strArray3[whichString3].ToCharArray();
							textToPrint3Split = new char[strArray3Split.Length];
							isPrinting = false;
						}
					}
				}
				break;

                // change scene to Sortify upon mouseclick
            case 5:
                if ((isChatButtonClicked)||(enterCase5))
				{
                    enterCase5 = true;
                    moveCam();
                    if (playUnlockSound)
                    {
                        playUnlockSound = false;
                        AudioManager.instance.PlaySound("menuLevelUnlocked");
                    }
				}
				break;

            default:
                button.SetActive(true);
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);

                Camera.main.gameObject.GetComponent<drag_cam>().enabled = true;
                chatBoxAfterGame1Won.SetActive(false);
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

	IEnumerator PrintText3()
	{
		isPrinting = true;
		donePrinting = false;

		for (int i = 0; i < strArray3Split.Length; i++)
		{
			textToPrint3Split[i] = strArray3Split[i];
			string s = new string(textToPrint3Split);
			uiText.text = s;
			if (donePrinting)
			{
				yield break;
			}
			yield return new WaitForSeconds(charPrintDelay);
		}
		isPrinting = false;
		donePrinting = true;
		whichString3++;
		strArray3Split = strArray3[whichString3].ToCharArray();
		textToPrint3Split = new char[strArray3Split.Length];
	}

	void SetupOptions3()
	{
		downArrow.SetActive(false);
		optionsBox3.SetActive(true);
	}

	void SetupChat()
	{
		downArrow.SetActive(true);
		optionsBox3.SetActive(false);
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

    public void moveCam()
    {
        if (Vector3.Distance(Camera.main.gameObject.transform.position,sortifyPoint)>1)
        {
            Debug.Log("panning");
            displacement = Camera.main.gameObject.transform.position - sortifyPoint;
            Camera.main.gameObject.transform.Translate(-displacement * cameraPanSpeed * Time.deltaTime);
        }
        else
        {
            sequence = 6;
        }
    }
}