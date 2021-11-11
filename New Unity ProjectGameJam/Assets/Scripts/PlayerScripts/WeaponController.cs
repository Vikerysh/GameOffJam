using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public GameObject projectile;
    public Transform barrel;
    public Transform hand;
    public float fireRate;
    private float fireRateCounter;
    public float blowbackPower;
    [SerializeField]
    private LayerMask impactMask;
    [SerializeField]
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    public void Fire(){
        Quaternion rotationOfHand = hand.rotation;
        if(fireRateCounter <= 0){
            Instantiate(projectile, barrel.position, rotationOfHand);
            fireRateCounter = fireRate;
        }
    }
    public void PowerShot(){
        Quaternion rotationOfHand = hand.rotation;
        if(fireRateCounter <= 0){
            RaycastHit2D hit = Physics2D.CircleCast(barrel.position, 0.2f, barrel.right, .1f, impactMask);
            if(hit){
                playerMovement.BlowBack(hit.point, blowbackPower);
            }
            fireRateCounter = fireRate;
        }
    }
}
