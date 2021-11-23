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
    private bool aggresive = false;//is player in range to be targeted?
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
        if(aggresive){
            if(moveRight == false){
                rb.AddForce(-transform.right * speed * Time.deltaTime);
                anim.SetBool("isMoving", true);
            } else if(moveRight == true){
                rb.AddForce(transform.right * speed * Time.deltaTime);
                anim.SetBool("isMoving", true);
            }
            if(rb.velocity.magnitude > maxSpeed){
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        } else {
            anim.SetBool("isMoving", false);
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
        SoundManager.PlaySound(SoundManager.Sound.EnemyHit);
        if(health <= 0){
            Die();
        }
        rb.velocity = Vector2.zero;
        if(!aggresive){
            aggresive = true;
        }
        anim.SetTrigger("Hit");
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            GameController.instance.player.GetComponent<HealthSystem>().Damage(damage);
        }
    }

    public virtual void EnemyBehaviour(){

    }

    
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            aggresive = true;
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            aggresive = false;
        }
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
