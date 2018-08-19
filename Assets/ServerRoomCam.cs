using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ServerRoomCam : MonoBehaviour {


    private Vector3 startPos;
    private Vector3 positionOne;
    private Vector3 positionTwo;
    private Vector3 endPos;

    private Vector3 panPos;
    private Vector3 targetPos;

    private bool moveActive = false;

    [SerializeField]
    private GameObject buttons;

    [SerializeField]
    private GameObject finalScreen;

    private void Start ()
    {
    AudioManager.instance.PlaySound("Server Room BGM");

    startPos = new Vector3 (-34f, -55.6f, -10f);
    positionOne = new Vector3(17.8f, -55.6f, -10);
    positionTwo = new Vector3(-10.5f, 2.3f, -10);
    endPos = new Vector3(51.4f, 40.8f, -10);

    if(PlayerPrefs.GetInt("haveWonGame3") == 1)
    {
        buttons.SetActive(true);
    }

    transform.position = startPos;
    }
	
	void Update () {
        if (moveActive)
        {
            panPos = transform.position;
            transform.position = Vector3.Lerp(panPos, targetPos, 0.1f);
        }
	}

    public void move1()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        targetPos = positionOne;
        moveActive = true;
        Debug.Log("move1");
        GetComponent<VideoPlayer>().Prepare();
    }

    public void move2()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        targetPos = positionTwo;
        moveActive = true;
    }

    public void move3()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        targetPos = endPos;
        moveActive = true;
        StartCoroutine(startVideo());
    }

    IEnumerator startVideo()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<VideoPlayer>().Play();
        AudioManager.instance.PlaySound("VideoMusic");
        yield return new WaitForSeconds(35f);
        Debug.Log("end");
        GetComponent<VideoPlayer>().enabled = false;
        finalScreen.SetActive(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        AudioManager.instance.PlayCommonSound("Click Button");
        AudioManager.instance.StopSound("Server Room BGM");

        PlayerPrefs.SetInt("haveFinishedGame",1);
        PlayerPrefs.SetInt("chatScene",7);
        PlayerPrefs.SetInt("toScene", 0);
        SceneManager.LoadScene("LoadingScreen", LoadSceneMode.Single);
    }
}
