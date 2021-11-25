using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingEnemy : MonoBehaviour
{
    public int health;
    public int damage = 1;
    public float speed;
    public float maxSpeed;
    public float hoverLength;
    private float playerDist;
    //private float floorDist;
    public Rigidbody2D rb;
    public GameObject player;
    private Vector2 playerDir;


    public GameObject loot;
    public SpriteRenderer enemySprite;

    [SerializeField]
    private float heightDifference;
    private float sideDifference;

    private Vector2 startPos;

    public Animator anim;
    [SerializeField]
    private LayerMask floorMask;

    private bool goingUp;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        player = GameController.instance.player;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(goingUp){
            rb.AddForce(transform.up * (speed * 2) * Time.deltaTime);
        }else if(!goingUp){
            rb.AddForce(transform.up * -(speed * 2) * Time.deltaTime);
        }
        if(rb.velocity.magnitude > maxSpeed){
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }
    }
    void Update()
    {
        if(transform.position.y <= startPos.y){
            goingUp = true;
        }
        if(transform.position.y  >=  startPos.y + hoverLength){
            goingUp = false;
        }
        sideDifference = transform.position.x - player.transform.position.x;
        playerDist = Vector2.Distance(player.transform.position, transform.position);

            playerDir = player.transform.position - transform.position;

            if(playerDir.x < 0){
                Flip();
            } else {
                Flip();
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
        enemySprite.flipX = !enemySprite.flipX;
    }
    public void TakeDamage(int x){
        health -= x;
        SoundManager.PlaySound(SoundManager.Sound.EnemyHit);
        if(health <= 0){
            Die();
        }
        anim.SetTrigger("Hit");
    }


    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            GameController.instance.player.GetComponent<HealthSystem>().Damage(damage);
        }
    }

    public virtual void Die(){
        LootDrop();
        SoundManager.PlaySound(SoundManager.Sound.EnemyDie);
        Destroy(gameObject);
    }

    public void LootDrop()
    {
        GameObject drop = Instantiate(loot, transform.position, Quaternion.identity);
    }
}
