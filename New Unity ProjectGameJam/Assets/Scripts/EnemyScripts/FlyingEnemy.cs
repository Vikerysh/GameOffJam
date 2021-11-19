using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    public int health;
    public int damage = 1;
    public float speed;
    public float maxSpeed;
    public float aggroRange;
    private float playerDist;
    //private float floorDist;
    public Rigidbody2D rb;
    public GameObject player;
    private Vector2 playerDir;


    public GameObject loot;

    private bool moveRight = true;
    private bool aggro;//is player in range to be attacked?
    private bool aggresive = false;//is player in range to be targeted?
    private bool striking = false;
    private bool onCooldown = false;
    public SpriteRenderer enemySprite;

    [SerializeField]
    private float heightDifference;
    private float sideDifference;

    public Animator anim;
    [SerializeField]
    private LayerMask floorMask;

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
            if(aggro == true){
                if(!striking){
                    if(sideDifference >= 2 || sideDifference <= -2){
                        if(moveRight == false){
                            rb.AddForce(-transform.right * speed * Time.deltaTime);
                        } else if(moveRight == true){
                            rb.AddForce(transform.right * speed * Time.deltaTime);
                        }
                    }
                    if(heightDifference < 2){
                        rb.AddForce(transform.up * speed * Time.deltaTime);
                    } else if (heightDifference > 2){
                        rb.AddForce(-transform.up * speed * Time.deltaTime);
                    }

                    if(rb.velocity.magnitude > maxSpeed){
                        rb.velocity = rb.velocity.normalized * maxSpeed;
                    }
                }
                if(playerDist < 3 && !onCooldown) {
                    striking = true;
                    onCooldown = true;
                    StartCoroutine(Strike());
                }
            }  
        } else {
            if(FloorDistance() < 5f){
                rb.AddForce(transform.up * (speed * 2) * Time.deltaTime);
            }
            if(FloorDistance() > 6f){
                rb.AddForce(transform.up * -(speed * 2) * Time.deltaTime);
            }
            if(rb.velocity.magnitude > maxSpeed){
                rb.velocity = rb.velocity.normalized * maxSpeed;
            }
        }
    }
    void Update()
    {
        heightDifference = transform.position.y - player.transform.position.y;
        sideDifference = transform.position.x - player.transform.position.x;
        playerDist = Vector2.Distance(player.transform.position, transform.position);

        if(playerDist <= aggroRange){
            aggro = true;
        }
        if(aggro == true){

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
        }
        //EnemyBehaviour();
    }

    private float FloorDistance(){
        RaycastHit2D hit = Physics2D.Raycast(gameObject.transform.position, -transform.up, Mathf.Infinity, floorMask);
        float floorDist;
 
        if (hit.collider != null)
        {
        floorDist = hit.distance;
        return floorDist;
        }else{
            return 1f;
        }

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

    IEnumerator Strike(){
        rb.velocity = Vector2.zero;
        anim.SetTrigger("Ready");

        yield return new WaitForSeconds(1f);

        Vector2 direction = player.transform.position - transform.position;
        rb.velocity = direction*2.5f;
        anim.SetTrigger("Strike");
        
        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector2.zero;
        striking = false;
        anim.SetTrigger("Done");
        yield return new WaitForSeconds(2f);
        onCooldown = false;
    }


    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            GameController.instance.player.GetComponent<HealthSystem>().Damage(damage);
        }
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

    public virtual void EnemyBehaviour(){

    }
    public virtual void Die(){
        LootDrop();
        Destroy(gameObject);
    }

    public void LootDrop()
    {
        GameObject drop = Instantiate(loot, transform.position, Quaternion.identity);
    }
}
