using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    GameController gameController;

    [SerializeField] private LayerMask groundLayerMask;
    public float speed;
    public float jump;
    Vector2 move;
    Rigidbody2D rb;

    public SpriteRenderer charSprite;
    public SpriteRenderer gunSprite;
    public Animator charAnim;

    bool isFacingRight;
    private CapsuleCollider2D capsuleCollider2D;

    //CoyoteTime
    private float coyoteTime = 0.2f;
    private float coyoteTimeCounter;
    private Camera cam;

    public GameObject hand;

    private Vector2 playerScreenPoint;
    private Vector2 mousePosition;
    private WeaponController weaponController;

    private float stepTimer = 0.2f;
    public bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
        gameController.onGlitchChangeCallback += UpdateGlitches;
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        if(!charSprite.flipX){
            isFacingRight = true;
        }
        cam = gameController.cam;
        weaponController = GetComponent<WeaponController>();
        UpdateGlitches();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canMove){
            // Rigidbody Movement should only be put in FixedUpdate   
            if(move.x != 0){
                rb.velocity = new Vector2(move.x * speed * Time.deltaTime, rb.velocity.y);
                stepTimer -= Time.deltaTime;
                if(stepTimer <= 0){
                    SoundManager.PlaySound(SoundManager.Sound.PlayerMove);
                    stepTimer = 0.2f;
                }
            }
            if(IsGrounded() && move.x == 0){
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameController.isPaused){
            
            if(IsGrounded()){
                coyoteTimeCounter = coyoteTime;
            } else {
                coyoteTimeCounter -= Time.deltaTime;
            }

            move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if(coyoteTimeCounter > 0 && Input.GetButtonDown("Jump") && canMove){
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

            GunFaceMouse();

            playerScreenPoint = cam.WorldToScreenPoint(transform.position);

            //CHARACTER FLIPPING FOR TUNRING AROUND
            if(Input.mousePosition.x - playerScreenPoint.x < 0){
                if(isFacingRight){
                    Flip();
                }
            } else if(Input.mousePosition.x - playerScreenPoint.x > 0){
                if(!isFacingRight){
                    Flip();
                }
            }
            
        }

    }

    private void UpdateGlitches(){
        canMove = gameController.canMove;
        Debug.Log("eeeee");
    }

    private bool IsGrounded(){
        float offset = .01f;
        RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider2D.bounds.center, Vector2.down, capsuleCollider2D.bounds.extents.y + offset, groundLayerMask);
        
        return raycastHit.collider != null;
    }

    private void Flip(){
        isFacingRight = !isFacingRight;
        charSprite.flipX = !charSprite.flipX;
        gunSprite.flipY = !gunSprite.flipY;
    }
    private void Jump(){
        Debug.Log("Jump");
        rb.AddForce(transform.up * jump);
        SoundManager.PlaySound(SoundManager.Sound.PlayerJump);
    }

    private void GunFaceMouse(){
        mousePosition = Input.mousePosition;
        mousePosition = cam.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x, 
            mousePosition.y - transform.position.y
        );

        hand.transform.right = direction;
    }

    public void BlowBack(Vector2 hitPoint, float x){
        gameController.CanMove(false);
        StartCoroutine(MovementLock(0.5f));
        rb.velocity = Vector2.zero;
        Vector2 blowDir = (new Vector2(transform.position.x, transform.position.y) - hitPoint).normalized;
        rb.AddForce(blowDir * x);
        Debug.Log("weeee");
    }

    IEnumerator MovementLock(float x){
        yield return new WaitForSeconds(x);
        gameController.CanMove(true);
    }
}
