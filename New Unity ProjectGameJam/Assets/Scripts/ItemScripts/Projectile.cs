using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float lifetime;
    public float speed;
    public int damage;
    Rigidbody2D rb;
    public GameObject groundImpactSplash;
    [SerializeField]
    bool charged;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if(lifetime == 0){
            lifetime = 20f;
        }
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;
        if(lifetime <= 0){
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Enemy"){
            if(other.gameObject.GetComponent<Enemy>() != null){
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            } else if (other.gameObject.GetComponent<FlyingEnemy>() != null){
                other.gameObject.GetComponent<FlyingEnemy>().TakeDamage(damage);
            } else if (other.gameObject.GetComponent<FloatingEnemy>() != null){
                other.gameObject.GetComponent<FloatingEnemy>().TakeDamage(damage);
            }
            Instantiate(groundImpactSplash, other.gameObject.transform.position, Quaternion.identity);
        } else {
            Instantiate(groundImpactSplash, transform.position, Quaternion.identity);
        }
        if(charged){
            SoundManager.PlaySound(SoundManager.Sound.ProjectileChargedImpact);
        }
        Destroy(gameObject);
    }
}
