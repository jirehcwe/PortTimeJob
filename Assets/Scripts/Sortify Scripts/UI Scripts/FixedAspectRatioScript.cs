using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedAspectRatioScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.SetResolution(800, 1280, true);
    }
	
}
