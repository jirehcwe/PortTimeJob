using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingBarScript : MonoBehaviour {

    private float loadingDistance;
    private float loadProgress;
    private bool startLoading = false;

    private Vector2 startingPosition;
    private Vector2 targetPosition;

    private string[] sceneTransition = {
        "Menu",
        "CrateFishing",
        "CatchingItems",
        "Sortify",
        "SymmetryInstruction",
        "ServerRoom"};

	void Start () {
        startingPosition = transform.GetChild(0).transform.position;
        StartCoroutine(loadScreenWidth());
	}
	
	// Update is called once per frame
	void Update () {
        if (startLoading)
        {
            if(loadProgress < 1)
            {
                StartCoroutine(increaseLoadProgress());
            }
            else
            {
                SceneManager.LoadScene(sceneTransition[PlayerPrefs.GetInt("toScene")], LoadSceneMode.Single);
            }
            startLoading = false;
        }

        transform.GetChild(0).transform.position = Vector2.Lerp(startingPosition, targetPosition, loadProgress);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("startingPosition: " + startingPosition);
            print("targetPosition: " + targetPosition);
            print("loadProgress: " + loadProgress);
            print("loadScreenWidth: " + Screen.width * 9 / 16);
        }
	}

    IEnumerator loadScreenWidth()
    {
        yield return new WaitForSeconds(.5f);
        loadingDistance = Screen.width * 9 / 16;
        Vector2 addDistance = new Vector2(loadingDistance, 0);
        targetPosition = startingPosition + addDistance;
        startLoading = true;
    }

    IEnumerator increaseLoadProgress()
    {
        yield return new WaitForSeconds(Random.Range(0.03f, 0.05f));
        loadProgress += Random.Range(0.0001f, 0.04f);
        startLoading = true;
    }
}
