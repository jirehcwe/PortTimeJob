using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneControllerScript : MonoBehaviour {

    public GameObject chatBox;
    public GameObject chatBoxButton;
    public GameObject optionsBox;
    public GameObject optionsBox2;
	public GameObject chatBoxAfterGame1Won;
	public GameObject optionsBox3;
	public GameObject chatBoxAfterGame1Lose;
	public GameObject chatBoxAfterGame2Won;
	public GameObject optionsBox4;
	public GameObject optionsBox5;
	public GameObject optionsBox6;
	public GameObject chatBoxAfterGame2Lose;
	public GameObject chatBoxAfterGame3Lose;
	public GameObject chatBoxAfterGame3Won;
	public GameObject optionsBox7;
    public Button ftgInfoPoint;
    public Button agvInfoPoint;
    public Button portnetInfoPoint;
    public GameObject ftgLock;
    public GameObject agvLock;
    public GameObject portnetLock;
    public GameObject containerFishingPanel;
    public GameObject sortifyPanel;
    public GameObject symmetryPanel;
    public GameObject tuasArrow;

    private bool playUnlockSound = true;
     public Vector3 tuasPoint;     public float cameraPanSpeed = 5;

    private void Awake()
    {

        Debug.Log("chatScene= " + PlayerPrefs.GetInt("chatScene"));
        Debug.Log("Won0 = " + PlayerPrefs.GetInt("haveWonGame0"));
        Debug.Log("Won1= " + PlayerPrefs.GetInt("haveWonGame1"));
		Debug.Log("Lost1= " + PlayerPrefs.GetInt("haveLostGame1"));
		Debug.Log("Won2= " + PlayerPrefs.GetInt("haveWonGame2"));
		Debug.Log("Lost2= " + PlayerPrefs.GetInt("haveLostGame2"));
        Debug.Log("Lost3= " + PlayerPrefs.GetInt("haveLostGame3"));

        Camera.main.gameObject.transform.position = new Vector3(8, 2, -143.1f);

		chatBox = transform.parent.GetChild(0).gameObject;
		chatBoxButton = transform.parent.GetChild(1).gameObject;
		optionsBox = transform.parent.GetChild(2).gameObject;
        optionsBox2 = transform.parent.GetChild(3).gameObject;
        chatBoxAfterGame1Won = transform.parent.GetChild(4).gameObject;
        optionsBox3 = transform.parent.GetChild(5).gameObject;
		chatBoxAfterGame1Lose = transform.parent.GetChild(6).gameObject;
		chatBoxAfterGame2Won = transform.parent.GetChild(7).gameObject;
		optionsBox4 = transform.parent.GetChild(8).gameObject;
        optionsBox5 = transform.parent.GetChild(9).gameObject;
		optionsBox6 = transform.parent.GetChild(10).gameObject;
		chatBoxAfterGame2Lose = transform.parent.GetChild(11).gameObject;
		chatBoxAfterGame3Lose = transform.parent.GetChild(12).gameObject;
		chatBoxAfterGame3Won = transform.parent.GetChild(13).gameObject;
		optionsBox7 = transform.parent.GetChild(14).gameObject;

        tuasPoint = new Vector3(-88.4f, 92.6f, -143.1f);

		if (!PlayerPrefs.HasKey("chatScene"))
        {
            PlayerPrefs.SetInt("chatScene",0);
        }

		switch (PlayerPrefs.GetInt("chatScene"))
        {
            // intro chat
            case 0:
                chatBox.SetActive(true);
				chatBoxButton.SetActive(true);
				optionsBox.SetActive(false);
                optionsBox2.SetActive(false);
                chatBoxAfterGame1Won.SetActive(false);
                optionsBox3.SetActive(false);
                chatBoxAfterGame1Lose.SetActive(false);
				chatBoxAfterGame2Won.SetActive(false);
                optionsBox4.SetActive(false);
                optionsBox5.SetActive(false);
                optionsBox6.SetActive(false);
                chatBoxAfterGame2Lose.SetActive(false);
				chatBoxAfterGame3Lose.SetActive(false);
				chatBoxAfterGame3Won.SetActive(false);
				optionsBox7.SetActive(false);
                ftgInfoPoint.enabled=(false);
                agvInfoPoint.enabled=(false);
                portnetInfoPoint.enabled=(false);
                ftgLock.SetActive(true);
                agvLock.SetActive(true);
                portnetLock.SetActive(true);
                containerFishingPanel.SetActive(false);
                sortifyPanel.SetActive(false);
                symmetryPanel.SetActive(false);
                tuasArrow.SetActive(false);
				break;

            // game 1 win
            case 1:
                // only show win chat if player has NOT won game 1 before
                if (PlayerPrefs.GetInt("haveWonGame1") == 0)
                {
                    chatBox.SetActive(false);
                    chatBoxButton.SetActive(false);
                    optionsBox.SetActive(false);
                    optionsBox2.SetActive(false);
                    chatBoxAfterGame1Won.SetActive(true);
                    optionsBox3.SetActive(false);
                    chatBoxAfterGame1Lose.SetActive(false);
                    chatBoxAfterGame2Won.SetActive(false);
                    optionsBox4.SetActive(false);
                    optionsBox5.SetActive(false);
                    optionsBox6.SetActive(false);
                    chatBoxAfterGame2Lose.SetActive(false);
                    chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable=(true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (false);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(true);
                    containerFishingPanel.SetActive(true);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
                }
                else
                {
                    chatBox.SetActive(false);
                    chatBoxButton.SetActive(false);
                    optionsBox.SetActive(false);
                    optionsBox2.SetActive(false);
                    chatBoxAfterGame1Won.SetActive(false);
                    optionsBox3.SetActive(false);
                    chatBoxAfterGame1Lose.SetActive(false);
                    chatBoxAfterGame2Won.SetActive(false);
                    optionsBox4.SetActive(false);
                    optionsBox5.SetActive(false);
                    optionsBox6.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (false);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(true);
                    containerFishingPanel.SetActive(true);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
                }
				break;

            // game 1 lose
            case 2:
                // only show lose chat if player (has NOT lost game 1) AND (has NOT won game 1)
                if ((PlayerPrefs.GetInt("haveLostGame1")==0)&&(PlayerPrefs.GetInt("haveWonGame1")==0))
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(true);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (false);
                    agvInfoPoint.interactable = (false);
                    portnetInfoPoint.interactable = (false);
                    ftgLock.SetActive(true);
                    agvLock.SetActive(true);
                    portnetLock.SetActive(true);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
                }
				else
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (false);
                    agvInfoPoint.interactable = (false);
                    portnetInfoPoint.interactable = (false);
                    ftgLock.SetActive(true);
                    agvLock.SetActive(true);
                    portnetLock.SetActive(true);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
				}
                break;

            // game 2 win
            case 3:
				// only show win chat if player has NOT won game 2 before
				if (PlayerPrefs.GetInt("haveWonGame2") == 0)
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(true);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (true);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(false);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(true);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
				}
				else // have won game 2 before, disable everything
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (true);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(false);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(true);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
				}
                break;

            // game 2 lose
            case 4:
				// only show win chat if player (has NOT lost game 2) AND (has NOT won game 2)
				if ((PlayerPrefs.GetInt("haveLostGame2") == 0) && (PlayerPrefs.GetInt("haveWonGame2") == 0))
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(true);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (false);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(true);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
				}
				else
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (false);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(true);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
				}
                break;

            // game 3 win
            case 5:
				// only show win chat if player has NOT won game 3 before
				if (PlayerPrefs.GetInt("haveWonGame3") == 0)
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(true);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (true);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(false);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(true);
                    tuasArrow.SetActive(true);
				}
				else // have won game 3 before, disable everything
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (true);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(false);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(true);  
                    tuasArrow.SetActive(true);
				}
				break;

            // game 3 lose
            case 6:
				// only show win chat if player (has NOT lost game 3) AND (has NOT won game 3)
				if ((PlayerPrefs.GetInt("haveLostGame3") == 0) && (PlayerPrefs.GetInt("haveWonGame3") == 0))
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(true);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (true);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(false);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
				}
				else
				{
					chatBox.SetActive(false);
					chatBoxButton.SetActive(false);
					optionsBox.SetActive(false);
					optionsBox2.SetActive(false);
					chatBoxAfterGame1Won.SetActive(false);
					optionsBox3.SetActive(false);
					chatBoxAfterGame1Lose.SetActive(false);
					chatBoxAfterGame2Won.SetActive(false);
					optionsBox4.SetActive(false);
					optionsBox5.SetActive(false);
					optionsBox6.SetActive(false);
					chatBoxAfterGame2Lose.SetActive(false);
					chatBoxAfterGame3Lose.SetActive(false);
					chatBoxAfterGame3Won.SetActive(false);
                    optionsBox7.SetActive(false);
                    ftgInfoPoint.interactable = (true);
                    agvInfoPoint.interactable = (true);
                    portnetInfoPoint.interactable = (true);
                    ftgLock.SetActive(false);
                    agvLock.SetActive(false);
                    portnetLock.SetActive(false);
                    containerFishingPanel.SetActive(false);
                    sortifyPanel.SetActive(false);
                    symmetryPanel.SetActive(false);
                    tuasArrow.SetActive(false);
				}
                break;

            default: // after watching video (playerprefs haveFinishedGame==1)
                chatBox.SetActive(false);
                chatBoxButton.SetActive(false);
                optionsBox.SetActive(false);
                optionsBox2.SetActive(false);
                chatBoxAfterGame1Won.SetActive(false);
                optionsBox3.SetActive(false);
                chatBoxAfterGame1Lose.SetActive(false);
                chatBoxAfterGame2Won.SetActive(false);
                optionsBox4.SetActive(false);
                optionsBox5.SetActive(false);
                optionsBox6.SetActive(false);
                chatBoxAfterGame2Lose.SetActive(false);
                chatBoxAfterGame3Lose.SetActive(false);
                chatBoxAfterGame3Won.SetActive(false);
                optionsBox7.SetActive(false);
                ftgInfoPoint.interactable = (true);
                agvInfoPoint.interactable = (true);
                portnetInfoPoint.interactable = (true);
                ftgLock.SetActive(false);
                agvLock.SetActive(false);
                portnetLock.SetActive(false);
                containerFishingPanel.SetActive(false);
                sortifyPanel.SetActive(false);
                symmetryPanel.SetActive(false);
                tuasArrow.SetActive(true);
                break;
        }
    }

    private void Start()
    {
        AudioManager.instance.PlaySound("menuBackground");
    }

    // Update is called once per frame
    void Update () {
		// resets game testing if 'r' is pressed
		if (Input.GetKeyDown(KeyCode.R)){
            ResetPlayerPrefs();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            AfterWinningGame1();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            AfterWinningGame2();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            AfterWinningGame3();
        }

        if ((PlayerPrefs.GetInt("haveFinishedGame")==1)&&(PlayerPrefs.GetInt("chatScene")==7))
        {
            Camera.main.gameObject.GetComponent<drag_cam>().enabled = false;
            moveCam();
        }
	}

    public void moveCam()     {         if (Camera.main.gameObject.transform.position.x > -88)         {             //displacement = Camera.main.gameObject.transform.position - tuasPoint;             //Camera.main.gameObject.transform.Translate(-displacement * cameraPanSpeed * Time.deltaTime);
            Camera.main.gameObject.transform.position = Vector3.Lerp(Camera.main.gameObject.transform.position, tuasPoint, 2 * Time.deltaTime);
            print("moving cam");
            print(Vector3.Distance(Camera.main.gameObject.transform.position, tuasPoint));         }         else         {
            if (playUnlockSound)
            {
                playUnlockSound = false;
                AudioManager.instance.PlaySound("menuLevelUnlocked");
            }             PlayerPrefs.SetInt("chatScene",8);         }     } 
    public void ResetPlayerPrefs()
    {
        // Story playerPrefs
        PlayerPrefs.SetInt("chatScene", 0);
        PlayerPrefs.SetInt("haveWonGame0", 0);
        PlayerPrefs.SetInt("haveWonGame1", 0);
        PlayerPrefs.SetInt("haveLostGame1", 0);
        PlayerPrefs.SetInt("haveWonGame2", 0);
        PlayerPrefs.SetInt("haveLostGame2", 0);
        PlayerPrefs.SetInt("haveWonGame3", 0);
        PlayerPrefs.SetInt("haveLostGame3", 0);
        PlayerPrefs.SetInt("haveFinishedGame", 0);
        PlayerPrefs.SetString("gender", "");

        // Score playerPrefs
        PlayerPrefs.SetInt("Symmetry Highscore", 0);
        PlayerPrefs.SetInt("Catching Items Highscore", 0);
        PlayerPrefs.SetInt("Sortify Highscore", 0);

        // Stars playerPrefs
        PlayerPrefs.SetInt("Symmetry Stars", 0);
        PlayerPrefs.SetInt("Sortify Stars", 0);
        PlayerPrefs.SetInt("CatchingItems Stars", 0);

        Debug.Log("reset");
    }

    public void AfterWinningGame1()
    {
        PlayerPrefs.SetInt("chatScene", 1);
        PlayerPrefs.SetInt("haveWonGame0", 1);
        PlayerPrefs.SetInt("haveWonGame1", 0);
        PlayerPrefs.SetInt("haveLostGame1", 0);
        PlayerPrefs.SetInt("haveWonGame2", 0);
        PlayerPrefs.SetInt("haveLostGame2", 0);
        PlayerPrefs.SetInt("haveWonGame3", 0);
        PlayerPrefs.SetInt("haveLostGame3", 0);
        PlayerPrefs.SetInt("haveFinishedGame", 0);
        PlayerPrefs.SetString("gender", "");

        // Score playerPrefs
        PlayerPrefs.SetInt("Catching Items Highscore", 10000);
        PlayerPrefs.SetInt("Sortify Highscore", 0);
        PlayerPrefs.SetInt("Symmetry Highscore", 0);

        // Stars playerPrefs
        PlayerPrefs.SetInt("CatchingItems Stars", 1);
        PlayerPrefs.SetInt("Sortify Stars", 4);
        PlayerPrefs.SetInt("Symmetry Stars", 4);

        Debug.Log("won game 1");
    }

    public void AfterWinningGame2()
    {
        PlayerPrefs.SetInt("chatScene", 3);
        PlayerPrefs.SetInt("haveWonGame0", 1);
        PlayerPrefs.SetInt("haveWonGame1", 1);
        PlayerPrefs.SetInt("haveLostGame1", 0);
        PlayerPrefs.SetInt("haveWonGame2", 0);
        PlayerPrefs.SetInt("haveLostGame2", 0);
        PlayerPrefs.SetInt("haveWonGame3", 0);
        PlayerPrefs.SetInt("haveLostGame3", 0);
        PlayerPrefs.SetInt("haveFinishedGame", 0);
        PlayerPrefs.SetString("gender", "");

        // Score playerPrefs
        PlayerPrefs.SetInt("Catching Items Highscore", 10000);
        PlayerPrefs.SetInt("Sortify Highscore", 10000);
        PlayerPrefs.SetInt("Symmetry Highscore", 0);

        // Stars playerPrefs
        PlayerPrefs.SetInt("CatchingItems Stars", 1);
        PlayerPrefs.SetInt("Sortify Stars", 1);
        PlayerPrefs.SetInt("Symmetry Stars", 4);


        Debug.Log("won game 2");
    }

    public void AfterWinningGame3()
    {
        PlayerPrefs.SetInt("chatScene", 5);
        PlayerPrefs.SetInt("haveWonGame0", 1);
        PlayerPrefs.SetInt("haveWonGame1", 1);
        PlayerPrefs.SetInt("haveLostGame1", 0);
        PlayerPrefs.SetInt("haveWonGame2", 1);
        PlayerPrefs.SetInt("haveLostGame2", 0);
        PlayerPrefs.SetInt("haveWonGame3", 0);
        PlayerPrefs.SetInt("haveLostGame3", 0);
        PlayerPrefs.SetInt("haveFinishedGame", 0);
        PlayerPrefs.SetString("gender", "");

        // Score playerPrefs
        PlayerPrefs.SetInt("Catching Items Highscore", 10000);
        PlayerPrefs.SetInt("Sortify Highscore", 10000);
        PlayerPrefs.SetInt("Symmetry Highscore", 10000);

        // Stars playerPrefs
        PlayerPrefs.SetInt("CatchingItems Stars", 1);
        PlayerPrefs.SetInt("Sortify Stars", 1);
        PlayerPrefs.SetInt("Symmetry Stars", 1);


        Debug.Log("won game 3");
    }}
