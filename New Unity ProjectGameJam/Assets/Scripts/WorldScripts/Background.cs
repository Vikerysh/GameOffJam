using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{

    GameObject cam;
    float length, startpos, startposY;
    public float parallaxEffect;
    public float parallaxEffectY;
    // Start is called before the first frame update
    void Start()
    {
        cam = GameController.instance.cam.gameObject;
        startpos = transform.position.x;
        startposY = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);
        float distY = (cam.transform.position.y * parallaxEffectY);

        transform.position = new Vector3(startpos + dist, startposY + distY, transform.position.z);

        if(temp > startpos + length) startpos += length;
        else if(temp < startpos - length) startpos -= length;
    }
}
