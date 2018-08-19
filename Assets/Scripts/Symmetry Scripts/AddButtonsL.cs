using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AddButtonsL : MonoBehaviour {

    [SerializeField]
    private Transform panelField;

    [SerializeField]
    private GameObject RedCon;

    [SerializeField]
    private GameObject BlueCon;

    [SerializeField]
    private GameObject BrownCon;

    [SerializeField]
    private GameObject btn;

    [SerializeField]
    private GameObject clearText;

    [SerializeField]
    private GameObject scoreScreen;

    public bool timeStart = false;
    public bool stageWon = false;
    private bool gameEnd = false;
    public bool addScore = false;


    private int stageCleared = 0;
    public int correctFields = 0;


    private void Awake()
    {
        createButtons();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stageWon = true;

        }
        if (stageWon && stageCleared<5)
        {
            stageCleared++;
            Camera.main.GetComponent<CameraMotor>().SlideCamera(true);
            restart();
            addScore = true;
            stageWon = false;
        }
        else if(stageCleared == 5 && !gameEnd)
        {
            makeUninteractable();
            gameEnd = true;
            destroyLeftCrates();
            destroyRightCrates();
            Debug.Log("gameended");
            timeStart = false;
            StartCoroutine(waitScore());
            scoreScreen.SetActive(true);
        }
    }


    public void spawnContainers()
    {
        timeStart = true;

        // no container
        if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.childCount == 0)
        {
            GameObject container = Instantiate(RedCon);
            container.transform.SetParent(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform, false);
            container.transform.localEulerAngles = new Vector3(270, 0, 0);
            container.transform.localScale = new Vector3(0.13F, 0.13F, 0.13F);
            correctFields = 0;

            AudioManager.instance.PlaySound("PlaceBlock");

            checkMatch();
            if (correctFields == 20)
            {
                StartCoroutine(printStageCleared());
                makeUninteractable();
                printWinText();
            }
        }// 1 container
        else if(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Prop_Container_04(Clone)")
        {
            Destroy(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.transform.parent = null;
            GameObject container = Instantiate(BlueCon);
            container.transform.SetParent(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform, false);
            container.transform.localEulerAngles = new Vector3(270, 0, 0);
            container.transform.localScale = new Vector3(0.13F, 0.13F, 0.13F);
            correctFields = 0;

            AudioManager.instance.PlaySound("PlaceBlock");

            checkMatch();
            if (correctFields == 20)
            {
                StartCoroutine(printStageCleared());
                makeUninteractable();
                printWinText();
            }
        }// 2nd container
        else if (UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.name == "Prop_Container_01(Clone)")
        {
            Destroy(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.transform.parent = null;
            GameObject container = Instantiate(BrownCon);
            container.transform.SetParent(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform, false);
            container.transform.localEulerAngles = new Vector3(270, 0, 0);
            container.transform.localScale = new Vector3(0.13F, 0.13F, 0.13F);
            correctFields = 0;

            AudioManager.instance.PlaySound("PlaceBlock");

            checkMatch();

            if (correctFields == 20)
            {
                StartCoroutine(printStageCleared());
                makeUninteractable();
                printWinText();
            }
        }
        else
        {
            Destroy(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject);
            UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).gameObject.transform.parent = null;
            correctFields = 0;

            checkMatch();
            AudioManager.instance.PlaySound("RemoveBlock");
            //Debug.Log(correctFields);

            if (correctFields == 20)
            {
                StartCoroutine(printStageCleared());
                makeUninteractable();
                printWinText();
            }
        }
    }
    private void checkMatch()
    {
        for (int i = 0; i < 20; i++)
        {
            if (sameChildCount(i))
            {
                if(transform.GetComponentInParent<WinCondition>().leftButtons[i].transform.childCount == 1)
                {
                    if (sameChild(i))
                    {
                        correctFields++;
                    }
                    else continue;
                }
                else
                {
                    correctFields++;
                }
            }
        }
    }

    private bool sameChild(int i)
    {
        if (transform.GetComponentInParent<WinCondition>().leftButtons[i].transform.GetChild(0).gameObject.name
            == transform.GetComponentInParent<WinCondition>().rightButtons[(i / 5) * 5 + (4 - (i % 5))].transform.GetChild(0).gameObject.name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool sameChildCount(int i)
    {
        if (this.transform.GetComponentInParent<WinCondition>().leftButtons[i].transform.childCount
        == this.transform.GetComponentInParent<WinCondition>().rightButtons[(i / 5) * 5 + (4 - (i % 5))].transform.childCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void restart()
    {
        destroyLeftCrates();
        destroyRightCrates();
        createRightCrates();
        makeInteractable();
    }

    private void printWinText()
    {
        Debug.Log("You WON!!");
    }

    private void createButtons()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject button = Instantiate(btn) as GameObject;
            button.name = "" + i;
            button.transform.SetParent(panelField, false);
            button.GetComponent<Button>().onClick.AddListener(() => spawnContainers());
        }
    }

    private void destroyLeftCrates()
    {
        //Debug.Log("Entered destroy function.");
        for (int i = 0; i < 20; i++)
        {
            if(this.transform.GetComponentInParent<WinCondition>().leftButtons[i].transform.childCount == 1)
            {
                //Debug.Log("Starting destroy.");
                Destroy(this.transform.GetComponentInParent<WinCondition>().leftButtons[i].transform.GetChild(0).gameObject);
            }
        }

    }

    private void destroyRightCrates()
    {
        //Debug.Log("Entered destroy function.");
        for (int i = 0; i < 20; i++)
        {
            if (this.transform.GetComponentInParent<WinCondition>().rightButtons[i].transform.childCount == 1)
            {
                //Debug.Log("Starting destroy.");
                Destroy(this.transform.GetComponentInParent<WinCondition>().rightButtons[i].transform.GetChild(0).gameObject);
            }
        }

    }

    private void createRightCrates()
    {
        if(stageCleared < 3)
        {
            for (int i = 0; i < 20; i++)
            {
                if (UnityEngine.Random.Range(1, 100) < 10 * (stageCleared + 1))
                {
                    twoColours(this.transform.GetComponentInParent<WinCondition>().rightButtons[i]);
                }
            }
        }
        else
        {
            for (int i = 0; i < 20; i++)
            {
                if (UnityEngine.Random.Range(1, 100) < 10 * (stageCleared + 1))
                {
                    threeColours(this.transform.GetComponentInParent<WinCondition>().rightButtons[i]);
                }
            }
        }
    }
    private void threeColours(GameObject button)
    {
        int temp = UnityEngine.Random.Range(1, 4);
        if (temp == 1)
        {
            createBrown(button);
        }
        else if (temp == 2)
        {
            createRed(button);
        }
        else
        {
            createBlue(button);
        }
    }

    private void twoColours(GameObject button)
    {
        int temp = UnityEngine.Random.Range(1, 3);
        if (temp == 1)
        {
            createBlue(button);
        }
        else
        {
            createRed(button);
        }
    }

    private void createRed(GameObject button)
    {
        GameObject container = Instantiate(RedCon);
        container.transform.SetParent(button.transform, false);
        container.transform.localEulerAngles = new Vector3(270, 0, 0);
        container.transform.localScale = new Vector3(0.13F, 0.13F, 0.13F);
    }
    private void createBlue(GameObject button)
    {
        GameObject container = Instantiate(BlueCon);
        container.transform.SetParent(button.transform, false);
        container.transform.localEulerAngles = new Vector3(270, 0, 0);
        container.transform.localScale = new Vector3(0.13F, 0.13F, 0.13F);
    }
    private void createBrown(GameObject button)
    {
        GameObject container = Instantiate(BrownCon);
        container.transform.SetParent(button.transform, false);
        container.transform.localEulerAngles = new Vector3(270, 0, 0);
        container.transform.localScale = new Vector3(0.13F, 0.13F, 0.13F);
    }

    private void makeInteractable()
    {
        for (int i = 0; i < 20; i++)
        {
            this.transform.GetComponentInParent<WinCondition>().leftButtons[i].GetComponent<Button>().interactable = true;
        }
    }

    public void makeUninteractable()
    {
        for (int i = 0; i < 20; i++)
        {
            this.transform.GetComponentInParent<WinCondition>().leftButtons[i].GetComponent<Button>().interactable = false;
        }
    }

    IEnumerator printStageCleared()
    {
        AudioManager.instance.PlaySound("Clear");
        clearText.SetActive(true);
        yield return new WaitForSeconds(1);
        clearText.SetActive(false);
        stageWon = true;
    }

    IEnumerator waitScore()
    {
        Debug.Log("entered end game");
        yield return new WaitForSeconds(1);
        Debug.Log("entered end game2");
        clearText.transform.parent.gameObject.SetActive(false);
    }

    public void pause()
    {
        Time.timeScale = 0;
        makeUninteractable();
    }

    public void resume()
    {
        Time.timeScale = 1;
        makeInteractable();
    }
}
