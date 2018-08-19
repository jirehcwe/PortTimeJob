using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SymmetryInstructionText : MonoBehaviour {

    private string printString;
    private string[] displayText = {"Welcome to the loading port!",
        "You're here to replace Mr.Citos today.",
        "Assign the location of each container.",
        "Tap on the slot, to load the container on to the vessel.",
        "Tap again to change the colour or remove the container.",
        "Remember, the containers are of different weight!",
        "Ensure that the layout is symmetrical,",
        "such that the ship will be stable." };

    private Vector3 toPosition;
    private Vector3 startingPos;
    private bool moveLeft;
    private AsyncOperation preloadScene;



    private void Start () {
        StartCoroutine(animatateText(displayText));
        toPosition = new Vector3(-10, 0, 0);
        startingPos = transform.parent.GetChild(3).transform.localPosition;
    }

    private void Update()
    {
        GetComponent<Text>().text = printString;

        if (moveLeft)
        {
            print("enter move left.");
            startingPos = transform.parent.GetChild(3).transform.localPosition;
            transform.parent.GetChild(3).transform.localPosition = Vector3.Lerp(startingPos, toPosition, 0.1f);
        }
    }

    public void skipInstructions()
    {
        preloadScene.allowSceneActivation = true;
    }


    IEnumerator animateSymmetry()
    {
        transform.parent.GetChild(4).gameObject.SetActive(true);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        transform.parent.GetChild(4).GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        transform.parent.GetChild(4).GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        transform.parent.GetChild(4).GetChild(2).gameObject.SetActive(false);
        transform.parent.GetChild(4).GetChild(3).gameObject.SetActive(false);
        transform.parent.GetChild(4).GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        transform.parent.GetChild(4).GetChild(5).gameObject.SetActive(true);
    }

    IEnumerator animatateText(string[] text)
    {
        preloadScene = SceneManager.LoadSceneAsync("Symmetry", LoadSceneMode.Single);
        preloadScene.allowSceneActivation = false;
        yield return new WaitForSeconds(0.5F);
        printString = "";
        for(int i = 0; i<text.Length; i++)
        {
            int j = 0;
            while (j < text[i].Length)
            {
                if (j < text[i].Length)
                {
                    printString += text[i] [j++];
                    yield return new WaitForSeconds(0.02F);
                }
            }

            switch (i)
            {
                case 0:
                    transform.parent.GetChild(5).gameObject.SetActive(true);
                    break;
                case 3:
                    transform.parent.GetChild(3).gameObject.SetActive(true);
                    print(transform.parent.GetChild(3).transform.position);
                    break;
                case 5:
                    moveLeft = true;
                    yield return new WaitForSeconds(0.5f);
                    StartCoroutine(animateSymmetry());
                    break;
                case 6:
                    yield return new WaitForSeconds(3);
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                    preloadScene.allowSceneActivation = true;
                    break;
                default:
                    break;
            }

            if (i==text.Length - 1)
            {
                yield break;
            }
            yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
            AudioManager.instance.PlayCommonSound("Button Click");
            printString = "";
        }
    }
}
