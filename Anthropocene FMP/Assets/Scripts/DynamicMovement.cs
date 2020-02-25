using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMovement : MonoBehaviour
{
    public void updateSortOrder()
    {
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        sr.sortingOrder = (int)(sr.gameObject.transform.position.y * -100);
    }
}
