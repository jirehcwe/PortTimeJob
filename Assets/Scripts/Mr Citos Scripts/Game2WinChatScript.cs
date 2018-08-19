using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game2WinChatScript : MonoBehaviour
{
    public bool isChatButtonClicked = false;
    public static string gender;
    public static int sequence = 1; // starts with strArray chat
    public bool runLoop = true;
    public bool runLoop2 = true;
    public bool runLoop3 = true;
    public bool runLoop4 = true;
    public bool runLoop5 = true;
    public bool runLoop6 = true;
    public bool runLoop7 = true;
    public bool enterCase8 = false;
    public bool playUnlockSound = true;
    public bool skipClick = false;
    public bool skipClick2 = false;
    public bool skipClick3 = false;
    public static bool response1 = false;
    public static bool response2 = false;
    public static bool response3 = false;
    private GameObject chatBoxAfterGame2Won;
    private GameObject downArrow;
    public GameObject optionsBox4;
    public GameObject optionsBox5;
    public GameObject optionsBox6;
    public Text uiText;
    public bool isPrinting = false;
    public bool donePrinting = false;
    public float charPrintDelay = 0.01f;
    public char[] strArraySplit;
    public char[] textToPrintSplit;
    public int whichString = 0;
    public string[] strArray ={
        "Mr Citos: Woah!",
        "Mr Citos: Looks like you’ve gained access to the Loading Bay!",
        "Mr Citos: You've also unlocked the portnet and automated quay cranes info points!",
        "Mr Citos: Feel free to check it out in your own time!",
        "Mr Citos: Don't forget, they can be easily accessed through the jumping pink 'i's!",
        "Mr Citos: But don’t take too long, the outgoing vessels are loading in a moment!"
    };
    // Automated Guided Vehicle (AGV) Testing Bay
    public char[] strArray2Split;
    public char[] textToPrint2Split;
    public int whichString2 = 0;
    public string[] strArray2 ={
        "Mr Citos: Hah! Buckle up, son!",
        "Mr Citos: I haven’t eaten all day either!",
        "Mr Citos: This is a shipping port, not a charging port!"
    };
    public char[] strArray3Split;
    public char[] textToPrint3Split;
    public int whichString3 = 0;
    public string[] strArray3 ={
        "Mr Citos: Haha! Why w0uldN’t I be abl3 t0?",
        "Mr Citos: 1’m d001ng peRfeCTLy f1n3...!01100..10!11!01..."
    };
    public char[] strArray4Split;
    public char[] textToPrint4Split;
    public int whichString4 = 0;
    public string[] strArray4 ={
        "Mr Citos: Sur33 I am... 010011... AHh...",
        "Mr Citos: ... I feel... something rumbling in my system...",
        "Mr Citos: ... Well... go on then! Don’t worry about me!"
    };

    // these need to be changed to the location pins !!!
    public GameObject button;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

    Vector3 displacement;
    public Vector3 symmetryPoint = new Vector3(-53.97f, 10.36f, -143.1f);
    public float cameraPanSpeed = 5;

    void Start()
    {
        if (PlayerPrefs.GetInt("haveWonGame2") == 1)
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
		chatBoxAfterGame2Won = transform.parent.parent.GetChild(7).gameObject;
		gender = PlayerPrefs.GetString("gender");
		downArrow = transform.parent.GetChild(0).gameObject;
		downArrow.SetActive(true);
		optionsBox4.SetActive(false);
        optionsBox5.SetActive(false);
		optionsBox6.SetActive(false);

		// set reference to UIText
		uiText = GetComponent<Text>();

		// split strArray into a char array and store into strArraySplit
		strArraySplit = strArray[whichString].ToCharArray();

		// loads the char array into textToPrintSplit
		textToPrintSplit = new char[strArraySplit.Length];

		PlayerPrefs.SetInt("haveWonGame2", 1);
		Debug.Log("won game 2");
		Debug.Log("haveWonGame2= " + PlayerPrefs.GetInt("haveWonGame2"));
	}

	void Update()
	{
		// after strArray is fully printed
		if ((whichString == strArray.Length) && (runLoop))
		{
			// prevent re-entering this loop
			runLoop = false;

			// setup options box
			SetupOptions4();

			// set sequence to 2
			sequence = 2;
		}

		// after response1 is chosen for the first time
		if ((response1) && (runLoop2))
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

			// setup options box
			SetupOptions5();

			// set sequence to 2
			sequence = 4;

		}

		// after response2 is chosen for the first time
		if ((response2) && (runLoop4))
		{
			runLoop4 = false;
			strArray3Split = strArray3[whichString3].ToCharArray();
			textToPrint3Split = new char[strArray3Split.Length];
			whichString3 = 0;
			isPrinting = false;
			SetupChat2();
			sequence = 5;
			skipClick2 = true;
		}

		// after strArray2 is fully printed
		if ((whichString3 == strArray3.Length) && (runLoop5))
		{
			// prevent re-entering this loop
			runLoop5 = false;

			// setup options box
            SetupOptions6();

			// set sequence to 2
			sequence = 6;

		}

		// after response3 is chosen for the first time
		if ((response3) && (runLoop6))
		{
			runLoop6 = false;
			strArray4Split = strArray4[whichString4].ToCharArray();
			textToPrint4Split = new char[strArray4Split.Length];
			whichString4 = 0;
			isPrinting = false;
			SetupChat3();
			sequence = 7;
			skipClick3 = true;
		}

		// after strArray4 is fully printed
        if ((whichString4 == strArray4.Length)&&(runLoop7))
		{
            runLoop7 = false;
			sequence = 8;
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

			// waiting for Response2 response
			case 4:
				break;

			case 5:
				if ((isChatButtonClicked) || (skipClick2))
				{
					isChatButtonClicked = false;
					skipClick2 = false;
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

			// waiting for Response3 response
			case 6:
				break;

			// printing strArray4
			case 7:
				if ((isChatButtonClicked) || (skipClick3))
				{
					isChatButtonClicked = false;

					skipClick3 = false;
					if (!isPrinting)
                    {
                        AudioManager.instance.PlayCommonSound("click");
						textToPrint4Split = new char[strArray4Split.Length];
						StartCoroutine(PrintText4());
					}
					else
					{
						if (!donePrinting)
                        {
                            AudioManager.instance.PlayCommonSound("click");
							textToPrint4Split = strArray4Split;
							string s = new string(textToPrint4Split);
							uiText.text = s;
							donePrinting = true;
						}
						else
						{
							whichString4++;
							strArray4Split = strArray4[whichString4].ToCharArray();
							textToPrint4Split = new char[strArray4Split.Length];
							isPrinting = false;
						}
					}
				}
				break;

			// change scene to Sortify upon mouseclick
			case 8:
                if ((isChatButtonClicked)||(enterCase8))
                {
                    enterCase8 = true;
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
                chatBoxAfterGame2Won.SetActive(false);
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

	IEnumerator PrintText4()
	{
		isPrinting = true;
		donePrinting = false;

		for (int i = 0; i < strArray4Split.Length; i++)
		{
			textToPrint4Split[i] = strArray4Split[i];
			string s = new string(textToPrint4Split);
			uiText.text = s;
			if (donePrinting)
			{
				yield break;
			}
			yield return new WaitForSeconds(charPrintDelay);
		}
		isPrinting = false;
		donePrinting = true;
		whichString4++;
		strArray4Split = strArray4[whichString4].ToCharArray();
		textToPrint4Split = new char[strArray4Split.Length];
	}

	void SetupOptions4()
	{
		downArrow.SetActive(false);
		optionsBox4.SetActive(true);
	}

	void SetupOptions5()
	{
		downArrow.SetActive(false);
		optionsBox5.SetActive(true);
        transform.parent.GetChild(2).GetComponent<Button>().interactable = false;;
	}

	void SetupOptions6()
	{
		downArrow.SetActive(false);
		optionsBox6.SetActive(true);
	}

	void SetupChat()
	{
		downArrow.SetActive(true);
		optionsBox4.SetActive(false);
		downArrow.GetComponent<DownArrowScript>().Restart();
	}

	void SetupChat2()
	{
		downArrow.SetActive(true);
		optionsBox5.SetActive(false);
		downArrow.GetComponent<DownArrowScript>().Restart();
		transform.parent.GetChild(2).GetComponent<Button>().interactable = true ;
	}

	void SetupChat3()
	{
		downArrow.SetActive(true);
		optionsBox6.SetActive(false);
		downArrow.GetComponent<DownArrowScript>().Restart();
	}

	public void SetResponse1()
	{
        response1 = true;
	}

	public void SetResponse2()
	{
		response2 = true;
	}

	public void SetResponse3()
	{
		response3 = true;
	}

	public void nextChat()
	{
		isChatButtonClicked = true;
	}

    public void moveCam()
    {
        if (Vector3.Distance(Camera.main.gameObject.transform.position, symmetryPoint) > 1)
        {
            displacement = Camera.main.gameObject.transform.position - symmetryPoint;
            Camera.main.gameObject.transform.Translate(-displacement * cameraPanSpeed * Time.deltaTime);
        }
        else
        {
            sequence = 9;
        }
    }
}