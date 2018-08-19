using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour {

    [SerializeField]
    private GameObject scoreCanvas;

    public Transform lookAt;
    public float smoothSpeed = 0.125f;

    private Vector3 offset;
    public GameObject[] planeArray = new GameObject[5];

    private float offsetY = 10.0f;
    private float offsetZ = -5.0f;

    private int planeSwap = 0;
    private const int PLANESWAPMAX = 4;
    private const int PLANESWAPMIN = 0;

    private Animation startSequence;

    private void Awake()
    {
        startSequence = GetComponent<Animation>();
        startSequence.Play("CamStart");
    }


    void Start () {
        offset = new Vector3(0, offsetY, offsetZ);
	}

	void Update () {
        if(transform.rotation != Quaternion.Euler(70, 0, 0))
        {
            Quaternion startCam = Quaternion.Euler(70, 0, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, startCam, Time.deltaTime * 2);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SlideCamera(true);
            //Debug.Log(planeSwap);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SlideCamera(false);
            //Debug.Log(planeSwap);
        }
	}

    public void SlideCamera(bool up)
    {
        if (up)
        {
            planeSwap += 1;
            planeSwap = Mathf.Clamp(planeSwap, PLANESWAPMIN, PLANESWAPMAX);
            lookAt.position = planeArray[planeSwap].transform.position;
        }
        else
        {
            planeSwap -= 1;
            planeSwap = Mathf.Clamp(planeSwap, PLANESWAPMIN, PLANESWAPMAX);
            lookAt.position = planeArray[planeSwap].transform.position;
        }
    }   

    private void LateUpdate()
    {
        Vector3 desiredPosition = lookAt.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;
    }
}
