using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunFactsText : MonoBehaviour {

    private string[] funFactsTexts = {
        "PSA's port in Singapore is connected to 600 ports in over 120 countries!",
        "PSA moves a total of 190,000 containers daily!",
        "PSA has won Best Container Terminal - (Asia) 28 times!",
        "All of PSA's container movement and planning is done by computer!",
        "PSA manages and is involved in 40 terminals and 16 countries!",
        "PSA handles all kinds of cargo, from live animals to MRT trains!",
        "The use of automated cranes increases productivity by 6 times!",
        "PSA has fully Automated Guided Vehicles(AGV) to transport their containers!"};
    

    void Start()
    {
        GetComponent<Text>().text = funFactsTexts[Random.Range(0, funFactsTexts.Length)];
    }


}
