using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private void Update()
    {
		// THIS IS FOR TESTING !!!!!
		// PRESS W TO WIN
		if (Input.GetKeyDown(KeyCode.W))
		{
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);

			PlayerPrefs.SetInt("chatScene", 5);
		}

		// PRESS L TO LOSE
		if (Input.GetKeyDown(KeyCode.L))
		{
			SceneManager.LoadScene("Menu", LoadSceneMode.Single);

			PlayerPrefs.SetInt("chatScene", 6);
		}
    }
}