using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPosition : MonoBehaviour
{
    public GameObject x;
    // Update is called once per frame
    void Update()
    {
        if(x != null){
            transform.position = x.transform.position;
        }
    }
}
