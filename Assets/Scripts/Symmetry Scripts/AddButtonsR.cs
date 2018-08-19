using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddButtonsR : MonoBehaviour {

    [SerializeField]
    private Transform panelField;

    [SerializeField]
    private GameObject btn;

    [SerializeField]
    private GameObject RedCon;
    [SerializeField]
    private GameObject BlueCon;
    [SerializeField]
    private GameObject BrownCon;


    private Transform[] locations;

    private void Awake()
    {
        initializeCrates();
    }

    private void initializeCrates()
    {
        for (int i = 0; i < 20; i++)
        {
            GameObject button = Instantiate(btn);
            button.name = "" + i;
            button.transform.SetParent(panelField, false);
            if (Random.Range(1, 100) < 20)
            {
                createRed(button);
            }else if(i == 19)
            {
                createRed(button);
            }
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

}
