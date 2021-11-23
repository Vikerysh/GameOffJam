using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField]
    private string levelToLoad; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            LoadLevel();
        }
    }
    public void LoadLevel(){
        SceneManager.LoadScene(levelToLoad);
    }
}
