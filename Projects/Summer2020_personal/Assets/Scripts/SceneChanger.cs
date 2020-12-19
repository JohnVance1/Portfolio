using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneChanger : MonoBehaviour
{

    //public GameObject player;

    public PlayerCollision player;    



    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        // Creates the Game Over scene
        if(player.health <= 0)
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);

        }


    }
}
