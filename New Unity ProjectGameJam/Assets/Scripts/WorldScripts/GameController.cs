using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Singleton
    
    public static GameController instance;
    
    void Awake(){
        instance = this;
    }
    #endregion
    public GameObject player;
}
