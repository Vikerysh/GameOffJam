using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    public GameObject trap;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            trap.SetActive(true);
                trap.GetComponent<ShootingTrap>().activated = true;
        }


    }


}
