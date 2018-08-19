using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class restart_YF : MonoBehaviour {

	private void Start()
	{
		GetComponent<Button>().onClick.AddListener(() => restart());
	}
	public void restart()
	{
		Time.timeScale = 1;
		AudioManager.instance.PlayCommonSound("Button Click");
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name, LoadSceneMode.Single);
	}
}