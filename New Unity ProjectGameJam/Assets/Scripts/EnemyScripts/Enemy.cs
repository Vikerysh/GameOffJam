using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int damage = 1;
    public float speed;
    public float maxSpeed;
    public Rigidbody2D rb;
    public GameObject player;

    public GameObject loot;
    public int lootValue;

    private Vector2 playerDir;
    private bool moveRight = true;
    public SpriteRenderer enemySprite;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameController.instance.player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(moveRight == false){
            rb.AddForce(-transform.right * speed * Time.deltaTime);
        } else if(moveRight == true){
            rb.AddForce(transform.right * speed * Time.deltaTime);
        }
        if(rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    void Update()
    {
        
        playerDir = player.transform.position - transform.position;

        if(playerDir.x < 0){
            if(moveRight){
                Flip();
            }
        } else {
            if(!moveRight){
                Flip();
            }
        }

        //EnemyBehaviour();
    }

    private void Flip(){
        moveRight = !moveRight;
        enemySprite.flipX = !enemySprite.flipX;
    }
    public void TakeDamage(int x){
        health -= x;
        if(health <= 0){
            Die();
        }
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Hit");
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            GameController.instance.player.GetComponent<HealthSystem>().Damage(damage);
        }
    }

    public virtual void EnemyBehaviour(){

    }
    public virtual void Die(){
        //LootDrop();
        Destroy(gameObject);
    }

    public void LootDrop()
    {
        GameObject drop = Instantiate(loot, transform.position, Quaternion.identity);

    }
}
