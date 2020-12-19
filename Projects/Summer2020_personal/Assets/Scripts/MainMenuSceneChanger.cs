using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneChanger : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Loads the Game Scene
    public void LoadScene(int level)
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);

    }


}
