using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixAspectRatioPortrait : MonoBehaviour {

    private void Awake()
    {
        Debug.Log("Changing aspectratio to portrait");
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.SetResolution(800, 1280, true);
    }
    private void Start()
    {
        Debug.Log("Changing aspectratio to portrait");
        Screen.orientation = ScreenOrientation.Portrait;
        Screen.SetResolution(800, 1280, true);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
//            Debug.Log("Changing aspectratio to portrait");
            Screen.orientation = ScreenOrientation.Portrait;
            Screen.SetResolution(800, 1280, true);
            float targetaspect = 800f / 1280f;

            // determine the game window's current aspect ratio
            float windowaspect = (float)Screen.width / (float)Screen.height;

            // current viewport height should be scaled by this amount
            float scaleheight = windowaspect / targetaspect;

            // obtain camera component so we can modify its viewport
            Camera camera = GetComponent<Camera>();

            // if scaled height is less than current height, add letterbox
            if (scaleheight < 1.0f)
            {
                Rect rect = camera.rect;

                rect.width = 1.0f;
                rect.height = scaleheight;
                rect.x = 0;
                rect.y = (1.0f - scaleheight) / 2.0f;

                camera.rect = rect;
            }
            else // add pillarbox
            {
                float scalewidth = 1.0f / scaleheight;

                Rect rect = camera.rect;

                rect.width = scalewidth;
                rect.height = 1.0f;
                rect.x = (1.0f - scalewidth) / 2.0f;
                rect.y = 0;

                camera.rect = rect;
            }
        }
    }
}
