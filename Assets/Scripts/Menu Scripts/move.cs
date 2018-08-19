using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class move : MonoBehaviour
{
    private Vector3 desiredPosition;
    private Vector3 smoothPosition;
    private Vector3 direction;
    private Vector3 dirUpleft;
    private Vector3 dirUpRight;
    private Vector3 dirDownLeft;
    private Vector3 dirDownRight;

    [SerializeField]
    private Sprite spriteUpLeft;
    [SerializeField]
    private Sprite spriteUpRight;
    [SerializeField]
    private Sprite spriteDownLeft;
    [SerializeField]
    private Sprite spriteDownRight;


    private Vector3 startPoint;    
    public float speed;
    private int segment;

    void Start()
    {
        speed = 0.1f;
        dirUpleft = new Vector3(-100.8f, 59.2f, 0);
        dirUpRight = new Vector3(100.8f, 59.2f, 0);
        dirDownLeft = new Vector3(-100.8f, -59.2f, 0);
        dirDownRight = new Vector3(100.8f, -59.2f, 0);

        startPoint = new Vector3(60.23f, 33.1f, 0);

        direction = dirUpleft;
        segment = 1;
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);

        switch (segment)
        {
            case 1:
                if (transform.position.x < -45f)
                {
                    // Change to segment 2
                    downLeft();
                    segment = 2;
                }
                break;
            case 2:
                if (transform.position.x < -60f)
                {
                    downRight();
                    segment = 3;
                }
                break;
            case 3:
                if (transform.position.x > 6.3f)
                {
                    downLeft();
                    segment = 4;
                }
                break;
            case 4:
                if (transform.position.x <-27f)
                {
                    upLeft();
                    segment = 5;
                }
                break;
            case 5:
                if (transform.position.x < -108.3f)
                {
                    downLeft();
                    segment = 6;
                }
                break;
            case 6:
                if (transform.position.x < -133f)
                {
                    downRight();
                    segment = 7;
                }
                break;
            case 7:
                if (transform.position.x > -32.1f)
                {
                    upRight();
                    segment = 8;
                }
                break;
            case 8:
                if (transform.position.x > 25.8f)
                {
                    downRight();
                    segment = 9;
                }
                break;
            case 9:
                if (transform.position.x > 95.3f)
                {
                    downLeft();
                    segment = 10;
                }
                break;
            case 10:
                if (transform.position.x < 19.5f)
                {
                    downRight();
                    segment = 11;
                }
                break;
            case 11:
                if (transform.position.x > 48f)
                {
                    upRight();
                    segment = 12;
                }
                break;
            case 12:
                if (transform.position.x > 141.2f)
                {

                    upLeft();
                    segment = 13;
                }
                break;
            case 13:
                if (transform.position.x < 60.2f)
                {
                    transform.position = startPoint;
                    upLeft();
                    segment = 1;
                }
                break;
        }



    }

    private void upLeft()
    {
        direction = dirUpleft;
        GetComponent<SpriteRenderer>().sprite = spriteUpLeft;
    }
    private void upRight()
    {
        direction = dirUpRight;
        GetComponent<SpriteRenderer>().sprite = spriteUpRight;
    }
    private void downLeft()
    {
        direction = dirDownLeft;
        GetComponent<SpriteRenderer>().sprite = spriteDownLeft;
    }
    private void downRight()
    {
        direction = dirDownRight;
        GetComponent<SpriteRenderer>().sprite = spriteDownRight;
    }
}