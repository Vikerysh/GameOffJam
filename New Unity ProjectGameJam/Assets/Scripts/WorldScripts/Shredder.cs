using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shredder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            collision.gameObject.SetActive(false);
        }

        if (collision.tag == "Player")
        {
            HealthSystem healthSystem = collision.GetComponent<HealthSystem>();
            PlayerMovement player = collision.GetComponent<PlayerMovement>();
            print("Kill player");

            healthSystem.KillPlayer();

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.SetActive(false);
        }
    }
}
