using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] launchedObjects;

    private float cooldownTimer;
    [SerializeField] private float trapCooldown;
    public bool activated;

    private void Attack()
    {
        cooldownTimer = 0;
        launchedObjects[FindProjectile()].transform.position = firePoint.position;
        launchedObjects[FindProjectile()].GetComponent<ProjectileTrap>().Activate();
    }


    private int FindProjectile()
    {
        for(int i=0; i<launchedObjects.Length; i++)
        {
            if (!launchedObjects[i].activeInHierarchy)
                return i; 
        }

        return 0; 
    }
    private void Update()
    {
        if(activated)
        {
            Attack();
            StartCoroutine(Deactivate());
            cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
            Attack();
        }
        

    }



    IEnumerator Deactivate()
    {
        yield return new WaitForSeconds(trapCooldown);
        activated = false;
    }
}
