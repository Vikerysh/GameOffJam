using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public float damage;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(lifetime == 0){
            lifetime = 20f;
        }
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        Destroy(gameObject);
    }
}
