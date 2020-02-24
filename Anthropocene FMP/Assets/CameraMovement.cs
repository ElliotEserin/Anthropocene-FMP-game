using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject targetLookAt;
    public GameObject player;

    Vector3 offset = new Vector3(0,0,-10);

    void Start()
    {
        targetLookAt.transform.parent = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = targetLookAt.transform.position + offset;
    }
}
