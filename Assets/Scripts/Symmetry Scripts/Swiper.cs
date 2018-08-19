using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swiper : MonoBehaviour {

    public Transform lookAt;
    private Vector3 offset;

    private float yOffset = 5.0f;
    private float zOffset = 5.0f;

    void Start () {
        offset = new Vector3(0, yOffset, zOffset);
	}
	void Update () {
        transform.position = lookAt.position + offset;
	}
}
