using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroChatScript : MonoBehaviour
{
    [SerializeField]
    private GameObject LocationPinPanel;
    [SerializeField]
    private Button fishingButton;
    [SerializeField]
    private GameObject fishingStar;
    [SerializeField]
    private Sprite noStarSprite;
    public bool isChatButtonClicked = false;
	public static int sequence = 1; // starts with strArray chat
	public bool runLoop = true;
	public bool runLoop3 = true;
    public bool runLoop4 = true;
    public bool runLoop5 = true;
	public bool skipClick = false;
	public bool skipClick2 = false;
	public static string gender = ""; // static means it is for the class
	public static string option2 = "";
    private GameObject chatBox;
	private GameObject downArrow;
	public GameObject optionsBox;
	public GameObject optionsBox2;
	public Text uiText;
	public bool isPrinting = false;
	public bool donePrinting = false;
	public float charPrintDelay = 0.01f;
	public char[] strArraySplit;
	public char[] textToPrintSplit;
	public int whichString = 0;
	public string[] strArray ={
		"Mr Citos: Hello, there!",
		"Mr Citos: Glad to meet you!",
		"Mr Citos: Welcome to PSA Singapore’s Pasir Panjang terminal!",
		"Mr Citos: I’m Mr Citos.",
		"Mr Citos: I’ve been an employee of PSA since 1988 and I supervise all the operations around here.",
		"Mr Citos: Your job is simple- you’ll be filling in for me just for today while I’m on vacation.",
		"Mr Citos: Juuuust follow my instructions and you’ll be fine.",
		"Mr Citos: Geez, you’re so lucky to have me here to guide you!",
		"Mr Citos: But first, tell me a little about yourself.",
		"Mr Citos: Now tell me, are you a boy? Or are you a girl?"};
	public char[] strArray2Split;
	public char[] textToPrint2Split;
	public int whichString2 = 0;
	public string[] strArray2 ={
		string.Concat("Mr Citos: Ah! A ",gender,"!"),
		string.Concat("Mr Citos: Alright ",gender,", let's get straight to your first task of the day!")
		};
	public char[] strArray3Split;
	public char[] textToPrint3Split;
	public int whichString3 = 0;
	public string[] strArray3 ={
		"Mr Citos: What? Breakfast can wait! Come on, the vessel’s already docking!"
	};
	public char[] strArray4Split;
	public char[] textToPrint4Split;
	public int whichString4 = 0;
	public string[] strArray4 ={
		"Mr Citos: Brilliant! Down to the yard!"
	};

    public GameObject button;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;

	void Start()
	{
        LocationPinPanel.SetActive(false);
        button.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        button3.SetActive(false);

        Camera.main.gameObject.GetComponent<drag_cam>().enabled = false;
        chatBox = transform.parent.parent.GetChild(0).gameObject;
		downArrow = transform.parent.GetChild(0).gameObject;
		downArrow.SetActive(true);
		optionsBox.SetActive(false);
		optionsBox2.SetActive(false);

		// set reference to UIText
		uiText = GetComponent<Text>();

		// split strArray into a char array and store into strArraySplit
		strArraySplit = strArray[whichString].ToCharArray();

		// loads the char array into textToPrintSplit
		textToPrintSplit = new char[strArraySplit.Length];
	}

	void Update()
	{
		// FOR TESTING
		// PRESS S TO SKIP
		if (Input.GetKeyDown(KeyCode.S))
		{
			SceneManager.LoadScene("crate fishing", LoadSceneMode.Single);
		}

		// after strArray is fully printed
		if ((whichString == strArray.Length) && (runLoop))
		{
			// prevent re-entering this loop
			runLoop = false;

			// setup options box
			SetupOptions();

			// set sequence to 2
			sequence = 2;
		}

		// after gender is chosen for the first time
		if ((gender != "") && (sequence == 2))
		{
        	// setup strArray2 with the chosen gender
			strArray2[0] = string.Concat("Citos: Ah! A ", gender, "!");
			strArray2[1] = string.Concat("Citos: Alright ", gender, ", let's get straight to your first task of the day!");

			// split strArray2
			strArray2Split = strArray2[whichString2].ToCharArray();
			textToPrint2Split = new char[strArray2Split.Length];

			// setup chat
			SetupChat();

			// to skip the first click and start printing next chat immediately
			skipClick = true;
			isPrinting = false;
			whichString2 = 0;

			// set sequence to 3
			sequence = 3;
		}

		// after strArray2 is fully printed
		if ((whichString2 == strArray2.Length) && (runLoop3))
		{
			runLoop3 = false;
			SetupOptions2();
			sequence = 4;
		}

		// after option2 is chosen for the first time
		// if the user chooses option A
		if ((option2 == "A") && (runLoop4))
		{
			runLoop4 = false;
			strArray3Split = strArray3[whichString3].ToCharArray();
			textToPrint3Split = new char[strArray3Split.Length];
			whichString3 = 0;
			isPrinting = false;
			SetupChat2();
			skipClick2 = true;
			sequence = 5;
		}

		// after option2 is chosen for the first time
		// if the user chooses option B
		if ((option2 == "B") && (runLoop4)) // when option2 is first chosen
		{
			runLoop4 = false;
			strArray4Split = strArray4[whichString4].ToCharArray();
			textToPrint4Split = new char[strArray4Split.Length];
			whichString4 = 0;
			isPrinting = false;
			SetupChat2();
			skipClick2 = true;
			sequence = 6;
		}

		// after strArray3 or strArray4 is fully printed
        if (((whichString3 == strArray3.Length) || (whichString4 == strArray4.Length))&&(runLoop5))
		{
            runLoop5 = false;
			sequence = 7;
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

			// waiting for optionsBox response
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

			// waiting for optionsBox2 response
			case 4:
				break;

			// printing strArray3
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

			// printing strArray4
            case 6:
				if ((isChatButtonClicked) || (skipClick2))
				{
					isChatButtonClicked = false;

					skipClick2 = false;
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

            case 7:
                if (isChatButtonClicked)
                {
                    isChatButtonClicked = false;
                    LocationPinPanel.SetActive(true);
                    StartCoroutine(LocationPinDelay());
                }
                break;

			// change scene to container fishing upon mouseclick
			default:
			    PlayerPrefs.SetInt("haveWonGame0", 1);
                button.SetActive(true);
                button1.SetActive(true);
                button2.SetActive(true);
                button3.SetActive(true);
                fishingButton.interactable = true;
                fishingStar.GetComponent<SpriteRenderer>().sprite = noStarSprite;
                AudioManager.instance.PlaySound("menuLevelUnlocked");
                Camera.main.gameObject.GetComponent<drag_cam>().enabled = true;
                chatBox.SetActive(false);
				break;
		}
	}

	// these are the coroutines

    IEnumerator LocationPinDelay()
    {
        yield return new WaitForSeconds(2);
        LocationPinPanel.SetActive(false);
        sequence = 8;
    }
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
			yield return new WaitForSeconds(charPrintDelay);
		}
		isPrinting = false;
		donePrinting = true;
		whichString4++;
		strArray4Split = strArray4[whichString4].ToCharArray();
		textToPrint4Split = new char[strArray4Split.Length];
	}

	void SetupOptions()
	{
		downArrow.SetActive(false);
		optionsBox.SetActive(true);
	}

	void SetupOptions2()
	{
		downArrow.SetActive(false);
		optionsBox2.SetActive(true);
		//transform.parent.parent.GetComponentInChildren<RightArrowScript>().Restart();
		//transform.parent.parent.GetComponentInChildren<RightArrowScript2>().Restart();
		//optionsBox2.transform.GetChild(0).transform.GetChild(0).GetComponent<RightArrowScript>().Restart();
		//optionsBox2.transform.GetChild(1).transform.GetChild(0).GetComponent<RightArrowScript2>().Restart();
		//GetComponent<RightArrowScript>().Restart();
		//GetComponent<RightArrowScript2>().Restart();
	}

	void SetupChat()
	{
		downArrow.SetActive(true);
		optionsBox.SetActive(false);
		downArrow.GetComponent<DownArrowScript>().Restart();
	}

	void SetupChat2()
	{
		downArrow.SetActive(true);
		optionsBox2.SetActive(false);
		downArrow.GetComponent<DownArrowScript>().Restart();
	}

	public void SetGender(string chosenGender)
	{
		PlayerPrefs.SetString("gender", chosenGender);
		gender = chosenGender;
	}

	public void SetOption2(string chosenOption2)
	{
		option2 = chosenOption2;
	}

    public void nextChat()
    {
        isChatButtonClicked = true;
    }
}