using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class move_green : MonoBehaviour
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

        startPoint = new Vector3(-41.43f, 94.37f, 0);

        direction = dirDownRight;
        segment = 1;
    }

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);

        switch (segment)
        {
            case 1:
                if (transform.position.x > 143)
                {
                    downLeft();
                    segment = 2;
                }
                break;
            case 2:
                if (transform.position.x < 47.8f)
                {
                    upLeft();
                    segment = 3;
                }
                break;
            case 3:
                if (transform.position.x < 17.4f)
                {
                    upRight();
                    segment = 4;
                }
                break;
            case 4:
                if (transform.position.x > 93.6f)
                {
                    upLeft();
                    segment = 5;
                }
                break;
            case 5:
                if (transform.position.x < 26f)
                {
                    downLeft();
                    segment = 6;
                }
                break;
            case 6:
                if (transform.position.x < -30.8f)
                {
                    upLeft();
                    segment = 7;
                }
                break;
            case 7:
                if (transform.position.x < -135.5f)
                {
                    upRight();
                    segment = 8;
                }
                break;
            case 8:
                if (transform.position.x > -109.4f)
                {
                    downRight();
                    segment = 9;
                }
                break;
            case 9:
                if (transform.position.x > -26.5f)
                {
                    upRight();
                    segment = 10;
                }
                break;
            case 10:
                if (transform.position.x > 3.7f)
                {
                    upLeft();
                    segment = 11;
                }
                break;
            case 11:
                if (transform.position.x < -63f)
                {
                    upRight();
                    segment = 12;
                }
                break;
            case 12:
                if (transform.position.x > -45.1)
                {
                    transform.position = startPoint;
                    downRight();
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