using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFollow : MonoBehaviour
{
    [SerializeField] private float lootSpeed = 10;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            transform.position = Vector3.MoveTowards(transform.position, collision.transform.position, lootSpeed * Time.deltaTime);
        }
    }

    private void Update()
    {
        if(transform.childCount<1)
        { 
            Destroy(gameObject);
        }
    }

}
