using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    public Vector3 target_Offset;
    private Vector3 velocity = Vector3.zero;
    private void Start()
    {
        //get player instance
        target = GameController.instance.player;

        target_Offset = transform.position - target.transform.position;
    }
    void Update()
    {
        if (target)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position + target_Offset, ref velocity, 0.2f);
        }
    }
}
