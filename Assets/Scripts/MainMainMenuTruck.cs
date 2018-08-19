using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMainMenuTruck : MonoBehaviour {


    // Red truck
    private Vector3 directionRed;
    private Vector3 posRed;

    // Green truck
    private Vector3 directionGreen;
    private Vector3 posGreen;

    // Green truck2
    private Vector3 directionGreen2;
    private Vector3 posGreen2;

    // Green truck3
    private Vector3 directionGreen3;
    private Vector3 posGreen3;

    // Yellow truck
    private Vector3 directionYellow;
    private Vector3 posYellow;
    private bool yellowMovingLeft = false;

    [SerializeField]
    public Sprite yellowRight;
    [SerializeField]
    private Sprite yellowLeft;

    [SerializeField]
    private Sprite[] downRightSprite;

    [SerializeField]
    private Sprite[] upLeftSprite;

    [SerializeField]
    private Sprite[] downLeftSprite;

    private Vector3 dirUpleft;
    private Vector3 dirUpRight;
    private Vector3 dirDownLeft;
    private Vector3 dirDownRight;
    private Vector3 swapPos;

    public float speed;

    // Use this for initialization
    void Start ()
    {
        speed = 0.02f;
        dirUpleft = new Vector3(-100.8f, 59.2f, 0);
        dirUpRight = new Vector3(100.8f, 59.2f, 0);
        dirDownLeft = new Vector3(-100.8f, -59.2f, 0);
        dirDownRight = new Vector3(100.8f, -59.2f, 0);

        // Vehicle Red
        posRed = transform.GetChild(0).transform.localPosition;
        directionRed = dirUpleft;

        // Vehicle Green
        posGreen = transform.GetChild(1).transform.localPosition;
        directionGreen = dirDownLeft;

        // Vehicle Green2
        posGreen2 = transform.GetChild(2).transform.localPosition;
        directionGreen2 = dirUpleft;

        // Vehicle Green3
        posGreen3 = transform.GetChild(3).transform.localPosition;
        directionGreen3 = dirDownRight;

        // Vehicle Yellow
        posYellow = transform.GetChild(4).transform.localPosition;
        directionYellow = dirUpRight;

    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.GetChild(0).transform.Translate(directionRed * Time.deltaTime * speed);
        if(transform.GetChild(0).transform.localPosition.x < 0)
        {
            transform.GetChild(0).transform.localPosition = posRed;
            transform.GetChild(0).GetComponent<Image>().sprite = upLeftSprite[Random.Range(0, 3)];
        }

        transform.GetChild(1).transform.Translate(directionGreen * Time.deltaTime * speed);
        if (transform.GetChild(1).transform.localPosition.x < -750)
        {
            transform.GetChild(1).transform.localPosition = posGreen;
            transform.GetChild(1).GetComponent<Image>().sprite = downLeftSprite[Random.Range(0, 3)];
        }

        transform.GetChild(2).transform.Translate(directionGreen2 * Time.deltaTime * speed);
        if (transform.GetChild(2).transform.localPosition.x < -750)
        {
            transform.GetChild(2).transform.localPosition = posGreen2;
            transform.GetChild(2).GetComponent<Image>().sprite = upLeftSprite[Random.Range(0, 3)];
        }

        transform.GetChild(3).transform.Translate(directionGreen3 * Time.deltaTime * speed);
        if (transform.GetChild(3).transform.localPosition.x > 16)
        {
            transform.GetChild(3).transform.localPosition = posGreen3;
            transform.GetChild(3).GetComponent<Image>().sprite = downRightSprite[Random.Range(0,3)];
        }

        transform.GetChild(4).transform.Translate(directionYellow * Time.deltaTime * speed);
        if (transform.GetChild(4).transform.localPosition.x > 130 && !yellowMovingLeft)
        {
            swapPos = transform.GetChild(4).transform.localPosition;
            swapPos.x += 70;
            transform.GetChild(4).transform.localPosition = swapPos;
            directionYellow = dirDownLeft;
            transform.GetChild(4).GetComponent<Image>().sprite = yellowLeft;
            yellowMovingLeft = true;
        }
        if (transform.GetChild(4).transform.localPosition.x < -700 && yellowMovingLeft)
        {
            swapPos = transform.GetChild(4).transform.localPosition;
            swapPos.x -= 70;
            transform.GetChild(4).transform.localPosition = swapPos;
            directionYellow = dirUpRight;
            transform.GetChild(4).GetComponent<Image>().sprite = yellowRight;
            yellowMovingLeft = false;
        }
    }
}
