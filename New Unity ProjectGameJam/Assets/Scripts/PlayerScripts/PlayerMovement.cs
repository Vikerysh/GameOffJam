using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private LayerMask groundLayerMask;
    public float speed;
    public float jump;
    Vector2 move;
    Rigidbody2D rb;

    public SpriteRenderer charSprite;
    public Animator charAnim;

    bool isFacingRight;
    private CapsuleCollider2D capsuleCollider2D;

    //CoyoteTime
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
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
        if(IsGrounded()){
            coyoteTimeCounter = coyoteTime;
        } else {
            coyoteTimeCounter -= Time.deltaTime;
        }
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(coyoteTimeCounter > 0 && Input.GetButtonDown("Jump")){
            Jump();

        }

        if(Input.GetButtonUp("Jump") && rb.velocity.y > 0f){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }
        
        //CHARACTER MOVE DETECTION FOR ANIMATION
        if(move.x != 0){
            charAnim.SetBool("isMoving", true);
        } else {
            charAnim.SetBool("isMoving", false);
        }

        //CHARACTER FLIPPING FOR TUNRING AROUND
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

    private bool IsGrounded(){
        float offset = .01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, capsuleCollider2D.bounds.extents.y + offset, groundLayerMask);
        Color rayColor;
        if(raycastHit.collider != null){
            rayColor = Color.green;
        } else {
            rayColor = Color.red;
        }
        Debug.DrawRay(capsuleCollider2D.bounds.center, Vector2.down * (capsuleCollider2D.bounds.extents.y + offset), rayColor);
        return raycastHit.collider != null;
    }

    private void Flip(){
        isFacingRight = !isFacingRight;
        charSprite.flipX = !charSprite.flipX;
    }
    private void Jump(){
        Debug.Log("Jump");
        rb.AddForce(transform.up * jump);
    }

}
