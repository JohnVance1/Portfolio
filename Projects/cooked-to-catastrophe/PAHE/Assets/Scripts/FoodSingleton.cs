using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Author: John Vance
/// Purpose: Allows for any food to be transfered from the Kitchen Scene to the Cafeteria Scene
/// Restrictions: None
/// </summary>
public class FoodSingleton : MonoBehaviour
{
    // Checks to see if the singleton has been transfered or not
    private bool transfered;

    // The instance of the singleton
    private static FoodSingleton _instance;


    // Property to get the singleton outside of the script
    public static FoodSingleton Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("FoodSingleton");
                _instance = obj.AddComponent<FoodSingleton>();
            }
            return _instance;
        }
    }

    // Sets up everything for the singletons
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        transfered = true;
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);


    }

    /// <summary>
    /// Author: John Vance
    /// Purpose: Called whenever a scene loads
    /// Restrictions: None
    /// </summary>
    /// <param name="scene">The scene being loaded</param>
    /// <param name="mode">What mode the scene loads in</param>
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //food = null;
        if (scene.name == "Kitchen" || scene.name == "130_Kitchen")
        {
            GameObject obj = GameObject.Find("/Canvas/Plate");

            if (obj != null && (transfered == false))
            {
                SetToPlate(obj);
                transfered = true;

            }
            transfered = false;

        }
        if (scene.name == "Cafeteria" || scene.name == "130_Cafeteria")
        {
            GameObject obj = GameObject.Find("/Canvas/Serving Plate");

            if (obj != null && (transfered == false))
            {
                SetToPlate(obj);
                transfered = true;


            }
            transfered = false;

        }
    }

    /// <summary>
    /// Author: John Vance
    /// Purpose: Allows for the food to be placed on the plate in the Kitchen and Cafeteria scenes
    /// </summary>
    /// <param name="obj"></param>
    public void SetToPlate(GameObject obj)
    {
        // Gets the plate in the scene and sets the singleton to it
        if (_instance != null)
        {
            if (_instance.gameObject.GetComponent<SelectableObject>() != null)
            {
                _instance.transform.position = obj.transform.position;
                _instance.transform.SetParent(obj.transform);
                _instance.gameObject.GetComponent<SelectableObject>().enabled = true;

                Destroy(_instance);
            }
        }

    }

}
