using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFunction : MonoBehaviour
{
    [SerializeField]
    WeaponController weaponController;

    public void Charged(){
        weaponController.charged = true;
    }

}
