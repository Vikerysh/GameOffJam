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
    // Start is called before the first frame update
    void Start()
    {
        fireRateCounter = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if(fireRateCounter > 0){
            fireRateCounter -= Time.deltaTime;
        }
    }

    public void Fire(){
        Quaternion rotationOfHand = hand.rotation;
        if(fireRateCounter <= 0){
            Instantiate(projectile, barrel.position, rotationOfHand);
            fireRateCounter = fireRate;
        }
    }
}
