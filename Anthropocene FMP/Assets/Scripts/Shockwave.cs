using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Shockwave : MonoBehaviour
{

    float speed = 0.025f;
    void Update()
    {
        transform.localScale += new Vector3(speed, speed, speed);
        if (transform.localScale.x > 2f)
        {
            transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
        }
    }
}
