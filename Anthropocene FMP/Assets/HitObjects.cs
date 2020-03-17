using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObjects : MonoBehaviour
{
    public Item item;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            EnemyMovement enemy = collision.GetComponent<EnemyMovement>();
            enemy.health -= item.damage;
        }
    }

    private void Update()
    {
        Destroy(gameObject, 0.2f);
    }
}
