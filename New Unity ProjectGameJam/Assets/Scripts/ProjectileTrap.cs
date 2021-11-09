using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileTrap : MonoBehaviour
{

    public int health; 
    public int damage = 1;
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    private float lifetime;


    public void Activate()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);
        lifetime += Time.deltaTime;
        if(lifetime>resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.tag =="Player")
        {        gameObject.SetActive(false); 

            collision.gameObject.GetComponent<HealthSystem>().Damage(damage);
        }       

    }
}
