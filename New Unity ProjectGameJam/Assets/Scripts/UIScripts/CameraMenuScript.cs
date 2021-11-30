using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMenuScript : MonoBehaviour
{
    public float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }
}
