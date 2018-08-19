using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_agv : MonoBehaviour {

    //private Vector3 desiredPosition;
    //private Vector3 smoothPosition;
    private Vector3 direction;
    private Vector3 dirUpleft;
    private Vector3 dirUpRight;
    private Vector3 dirDownLeft;
    private Vector3 dirDownRight;

    [SerializeField]
    private Sprite agvLoaded;
    [SerializeField]
    private Sprite agvEmpty;

    private Vector3 startPoint;
    public float speed;
    private int segment;

    void Start() {
        {
            speed = 0.1f;
            dirUpleft = new Vector3(-100.8f, 59.2f, 0);
            dirUpRight = new Vector3(100.8f, 59.2f, 0);
            dirDownLeft = new Vector3(-100.8f, -59.2f, 0);
            dirDownRight = new Vector3(100.8f, -59.2f, 0);

            startPoint = new Vector3(126f, -14.2f, 0);

            direction = dirDownLeft;
            segment = 1;
        }
    }


    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);

        switch (segment)
        {
            case 0:
                if (transform.position.x > 125)
                {
                    transform.position = startPoint;
                    downLeft();
                    segment = 1;
                }
                break;
            case 1:
                if (transform.position.x < 95f)
                {
                    getContainer();
                    segment = 0;
                }
                break;
        }

    }

    private void getContainer()
    {
        direction = dirUpRight;
        GetComponent<SpriteRenderer>().sprite = agvLoaded;
    }

    private void downLeft()
    {
        direction = dirDownLeft;
        GetComponent<SpriteRenderer>().sprite = agvEmpty;
    }

}