using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeDisappear : MonoBehaviour
{

    public GameObject StartingTruck;

    void Start()
    {
        StartingTruck = gameObject.GetComponentInParent<Transform>().gameObject;
    }
    void Update()
    {
        if (StartingTruck.transform.position.y < -4.6)
        {
            Destroy(this.gameObject);
        }

    }
}
