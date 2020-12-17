using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/// <summary>
/// Author: Kyle Weekley
/// Purpose: Allows an object to be selected and appropriately updates the current selection UI
/// Restrictions: None
/// </summary>
public class SelectableObject : MonoBehaviour, IPointerDownHandler
{
    public bool selected;
    private KitchenManager kitchenManager;
    private CafeteriaManager cafeteriaManager;

    private Image selectionIcon;

    /// <summary>
    /// Author: Kyle Weekley
    /// Purpose: Sets references to objects necessary for item seleciton
    /// </summary>
    void Start()
    {
        selected = false;

        // Changes the manager based on the scene
        SceneManager.sceneLoaded += OnSceneLoaded;
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);

        //if (SceneManager.GetActiveScene().name == "Cafeteria" || SceneManager.GetActiveScene().name == "130_Cafeteria")
        //{
        //    cafeteriaManager = GameObject.Find("CafeteriaManager").GetComponent<CafeteriaManager>();

        //}

        //else
        //{
        //    kitchenManager = GameObject.Find("Game Manager").GetComponent<KitchenManager>();

        //}
        //selectionIcon = GameObject.Find("Selection Sprite").GetComponent<Image>();

    }

    void Update()
    {

    }

    /// <summary>
    /// Author: Kyle Weekley
    /// Purpose: Object is selected when clicked
    /// Restrictions: None
    /// </summary>
    /// <param name="eventData">Used for recognizing clicks on 2D objects</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        // Only select this object if another object is not already selected
        /*if (kitchenManager.currentSelection == null)
        {
            selected = true;
            kitchenManager.currentSelection = this.gameObject;

            //Set current selection sprite in UI to this object's sprite
            //selectionIcon.sprite = this.GetComponent<Image>().sprite;

            //Currently using sprite color for testing
            selectionIcon.color = this.GetComponent<Image>().color;
        }*/

        if (SceneManager.GetActiveScene().name == "Cafeteria" || SceneManager.GetActiveScene().name == "130_Cafeteria")
        {
            cafeteriaManager.ObjectSelected(this);

        }
        else
        {
            kitchenManager.ObjectSelected(this);
        }
    }

    /// <summary>
    /// Author: John Vance
    /// Purpose: Changes where the selected object code runs
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Kitchen" || scene.name == "130_Kitchen")
        {
            kitchenManager = GameObject.Find("Game Manager").GetComponent<KitchenManager>();


        }
        if (scene.name == "Cafeteria" || scene.name == "130_Cafeteria")
        {
            cafeteriaManager = GameObject.Find("CafeteriaManager").GetComponent<CafeteriaManager>();


        }
        if (GameObject.Find("Selection Sprite"))
        {
            selectionIcon = GameObject.Find("Selection Sprite").GetComponent<Image>();
        }
    }
}
