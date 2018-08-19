using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostGamePanelsScript : MonoBehaviour {

    public GameObject containerFishingPanel;
    public GameObject sortifyPanel;
    public GameObject symmetryPanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OpenContainerFishingPanel()
    {
        containerFishingPanel.SetActive(true);
    }

    public void OpenSortifyPanel()
    {
        sortifyPanel.SetActive(true);
    }

    public void OpenSymmetryPanel()
    {
        symmetryPanel.SetActive(true);
    }

    public void ClosePostGamePanel()
    {
        Debug.Log("closepostgamepanel");
        containerFishingPanel.SetActive(false);
        sortifyPanel.SetActive(false);
        symmetryPanel.SetActive(false);
    }
}
