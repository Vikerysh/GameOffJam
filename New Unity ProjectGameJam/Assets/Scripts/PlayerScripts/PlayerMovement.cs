using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    Vector2 move;
    Rigidbody2D rb;

    public SpriteRenderer charSprite;
    public Animator charAnim;

    bool isFacingRight;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(!charSprite.flipX){
            isFacingRight = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rigidbody Movement should only be put in FixedUpdate   
        rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(move.x != 0){
            charAnim.SetBool("isMoving", true);
        } else {
            charAnim.SetBool("isMoving", false);
        }

        if(move.x < 0){
            if(isFacingRight){
                Flip();
            }
        } else if(move.x > 0){
            if(!isFacingRight){
                Flip();
            }
        }

    }

    private void Flip(){
        isFacingRight = !isFacingRight;
        charSprite.flipX = !charSprite.flipX;
    }

}
