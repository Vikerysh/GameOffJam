using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        target = GameController.instance.player;
    }

    // Update is called once per frame
    void Update()
    {
        //Have Camera Follow Player
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }
}
