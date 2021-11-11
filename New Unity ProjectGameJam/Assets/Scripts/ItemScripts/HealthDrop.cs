using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop: MonoBehaviour
{
    public int healthValue;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<HealthSystem>().Heal(healthValue);
            Destroy(gameObject);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerBullet"))
        {

            Debug.Log("bullet");
            Physics2D.IgnoreCollision(collision.gameObject.GetComponent<CircleCollider2D>(), GetComponentInParent<BoxCollider2D>());

        }
    }

}
