using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_man : MonoBehaviour
{

    private Vector3 desiredPosition;
    private Vector3 smoothPosition;
    private Vector3 dirUpleft;
    private Vector3 dirUpRight;
    private Vector3 dirDownLeft;
    private Vector3 dirDownRight;

    public float speed;
    private int segment;

    void Start()
    {
        {
            speed = 0.05f;
            dirUpleft = new Vector3(-100.8f, 59.2f, 0);
            dirUpRight = new Vector3(100.8f, 59.2f, 0);
            dirDownLeft = new Vector3(-100.8f, -59.2f, 0);
            dirDownRight = new Vector3(100.8f, -59.2f, 0);
        }
    }


    void Update()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).transform.Translate(dirUpRight * Time.deltaTime * speed);
            if (transform.GetChild(0).transform.position.x > 42f)
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }

        if (transform.GetChild(1).gameObject.activeSelf)
        {
            transform.GetChild(1).transform.Translate(dirUpleft * Time.deltaTime * speed);
            if (transform.GetChild(1).transform.position.x < -80f)
            {
                transform.GetChild(1).gameObject.SetActive(false); ;
            }
        }
    }

}