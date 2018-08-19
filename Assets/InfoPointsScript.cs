using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPointsScript : MonoBehaviour {

    public Button ftgButt;
    public Button agvButt;
    public Button portnetButt;
    public GameObject infoPointsPanel;
    public Text infoPointsText;

    // Use this for initialization
    void Start() { 
    
    }

    public void OpenFtg()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        infoPointsText.text = "The Flow-Through Gate is a fully automated system that can identify container trucks and gives drivers instructions within 25 seconds! \n\n"
            + "Every day, up to 9,000 trucks enter the port with shipping containers. These containers are to be sorted according to their destination and characteristics like weight and contents. Each container has a specific address to be stacked in the yard. \n\n"
            + "To make this process efficient, several things happen concurrently. The truck is weighed at the weighbridge while the Container Number Recognition System (CNRS) captures the container number with CCTV cameras. The driver’s information is recorded by the system, checked against the manifest in PORTNET, and then relevant information is sent to the driver. \n\n"
            + "The Flow-Through Gate handles a traffic flow rate of 700 trucks per peak hour, and was conferred an Innovation Award at the 11th UK Seatrade Awards.";
        infoPointsPanel.SetActive(true);
        Camera.main.gameObject.GetComponent<drag_cam>().enabled = false;
    }

    public void OpenAgv()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        infoPointsText.text = "The Automated Guided Vehicles are a fleet of automatic vehicles as part of an investment in developing advanced port technologies. The AGVs are battery powered and are able to operate 24/7. Using state of the art navigation systems, the AGVs will be able to accurately transport containers between the quay and container yards completely without human drivers. \n\nThe AGVs are undergoing constant review and improvement, working toward the zero-emissions operations and improving efficiency in the ports.";
        infoPointsPanel.SetActive(true);
        Camera.main.gameObject.GetComponent<drag_cam>().enabled = false;
    }

    public void OpenPortnet()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        infoPointsText.text = "The PORTNET is the world’s first nation-wide business to business (B2B) port community solution. It helps the entire port and shipping community to increase productivity and efficiency through the greater use of information technology and the Internet. \n\nThe PSA has connected shipping lines, hauliers, freight forwarders and government agencies, helping them to manage information better and synchronise their complex operational processes. \n\nPORTNET is able to simplify and synchronise millions of processes for customers moving their cargo through Singapore. Over 10, 000 integrated users rely on the system’s unparalleled capability to provide real - time, detailed information on all port, shipping, and logistics processes crucial to their businesses. PORTNET processes more than 220 million transactions a year.";
        infoPointsPanel.SetActive(true);
        Camera.main.gameObject.GetComponent<drag_cam>().enabled = false;
    }

    public void CloseInfoPointsPanel()
    {
        AudioManager.instance.PlayCommonSound("Button Click");
        infoPointsPanel.SetActive(false);
        Camera.main.gameObject.GetComponent<drag_cam>().enabled = true;
    }
}
