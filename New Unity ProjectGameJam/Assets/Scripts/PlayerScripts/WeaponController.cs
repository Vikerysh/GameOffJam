using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    GameController gameController;
    private bool canShoot = true;
    public bool charged;

    public GameObject projectile;
    public GameObject chargedProjectile;
    public GameObject barrel;
    public Transform hand;
    public float fireRate;
    private float fireRateCounter;
    public float blowbackPower;
    [SerializeField]
    private LayerMask impactMask;
    [SerializeField]
    private PlayerMovement playerMovement;
    public GameObject groundPowerSplash;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private GameObject chargeEffect;
    private GameObject chargeFX;

    private bool charging = false;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameController.instance;
        gameController.onGlitchChangeCallback += UpdateGlitches;
        fireRateCounter = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameController.instance.isPaused){
            if(fireRateCounter > 0){
                fireRateCounter -= Time.deltaTime;
            }
        } 
        if(canShoot){
            if(Input.GetButton("Fire1") && !charging){
                Fire();
            }    
            if(Input.GetButtonDown("Fire2")){
                charging = true;
                anim.SetBool("Charging", charging);
                chargeFX = Instantiate(chargeEffect, barrel.transform.position, Quaternion.identity);
                chargeFX.GetComponent<TrackPosition>().x = barrel;
            } 

            if(Input.GetButtonUp("Fire2")){
                charging = false;
                anim.SetBool("Charging", charging);
                Destroy(chargeFX);
                if(charged){
                    PowerShot();
                }
            }
        }
    }

    private void UpdateGlitches(){
        canShoot = gameController.canShoot;
        Debug.Log("eeeee");
    }
    public void Fire(){
        Quaternion rotationOfHand = hand.rotation;
        if(fireRateCounter <= 0){
            Instantiate(projectile, barrel.transform.position, rotationOfHand);
            SoundManager.PlaySound(SoundManager.Sound.PlayerShoot);
            fireRateCounter = fireRate;
        }
    }
    public void PowerShot(){
        Quaternion rotationOfHand = hand.rotation;
        RaycastHit2D hit = Physics2D.CircleCast(barrel.transform.position, 0.2f, barrel.transform.right, .1f, impactMask);
        if(hit){
            Instantiate(groundPowerSplash, hit.point, Quaternion.identity);
            playerMovement.BlowBack(hit.point, blowbackPower);
        }
        Instantiate(chargedProjectile, barrel.transform.position, rotationOfHand);
        charged = false;
        fireRateCounter = fireRate/2;
    }
}
