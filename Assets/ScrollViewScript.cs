using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewScript : MonoBehaviour {

    public ScrollRect scrollRect;

    public void ResetScrollRect ()
    {
        scrollRect.verticalNormalizedPosition = 1;
    }
}
