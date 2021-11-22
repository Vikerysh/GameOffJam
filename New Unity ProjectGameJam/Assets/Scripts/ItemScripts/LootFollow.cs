using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFollow : MonoBehaviour
{
    [SerializeField] private float lootSpeed = 10;
    [SerializeField] private float lootDelay = 0.5f;

    private Rigidbody2D rb;
    private bool canMove = false;

    void Start(){
        //have drop float out for just a second
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = new Vector2((float)Random.Range(-1000,1000), (float)Random.Range(-1000,1000));
        rb.AddForce(direction * 0.1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(canMove){
                transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, lootSpeed * Time.deltaTime);
            }
        }
    }

    private void Update()
    {
        if(lootDelay <= 0){
            canMove = true;
        } else {
            lootDelay -= Time.deltaTime;
        }
        if(transform.childCount<1)
        { 
            Destroy(gameObject);
        }
    }

}
