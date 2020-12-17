using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CafeteriaManager : MonoBehaviour
{
    // Author: Nick Engell, Kyle Weekley
    // Dictionary that holds a string for food name and a sprite for the cooked sprite of that food
    private Dictionary<string, Sprite> specials;
    // List that holds guest gameObjects
    private List<GameObject> guests;
    // Dictionary of accepted guest orders and quantities
    private Dictionary<string, int> orders;
    // Guest prefab
    [SerializeField] private GameObject guest;
    //Canvas reference for guest instantiation
    [SerializeField] private Canvas guestCanvas;
    //Locations to spawn guests
    [SerializeField] private Vector3 guestPositionLeft;
    [SerializeField] private Vector3 guestPositionCenter;
    [SerializeField] private Vector3 guestPositionRight;
    // Sprite for the fried egg
    [SerializeField] private Sprite cookedEggSprite;
    // Sprite for rice
    [SerializeField] private Sprite cookedRiceSprite;
    // Sprite for spaghetti & meatballs
    [SerializeField] private Sprite cookedSpaghettiMeatballSprite;
    // The guest info canvas panel
    [SerializeField] private GameObject guestInfo;
    // The button to close the guest info canvas panel
    [SerializeField] private Button guestInfoCloseButton;
    // Specials list UI object
    [SerializeField] private GameObject specialsUIItemText;

    // Holds all of the info for the plates in the Cafeteria scene
    [SerializeField] private GameObject plate1;
    [SerializeField] private GameObject plate2;
    [SerializeField] private GameObject plate3;
    private List<GameObject> plates;


    public SelectableObject currentSelection;
    private Image selectionIcon;


    // Author: Kyle Weekley, Ben Stern
    /// <summary>
	/// the singleton instance
	/// </summary>
	private static CafeteriaManager _instance;

    // Author: Kyle Weekley, Ben Stern
    /// <summary>
    /// the instance of the cart manager
    /// </summary>
    public static CafeteriaManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("CafeteriaManager");
                _instance = obj.AddComponent<CafeteriaManager>();
            }
            return _instance;
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Get property for the specials dictionary
    /// </summary>
    public Dictionary<string, Sprite> Specials
    {
        get { return specials; }
    }

    // Author: Kyle Weekley
    /// <summary>
    /// Get property for the orders dictionary
    /// </summary>
    public Dictionary<string, int> Orders
    {
        get { return orders; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Get property for the guest info canvas element
    /// </summary>
    public GameObject GuestInfo
    {
        get { return guestInfo; }
    }

    // Author: Kyle Weekley, Ben Stern
    /// <summary>
    /// Called before Start to handle singleton setup
    /// </summary>
    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(gameObject);
        //because cafeteria manager is not destroyed when new scenes are loaded we need to reget certain elements when entering a new scene
        SceneManager.sceneLoaded += OnSceneLoaded;

        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    // Set up everything needed for Day 1
    void Start()
    {
        SetupDayOneSpecials();
        SetupDayOneGuests();
        orders = new Dictionary<string, int>();
        selectionIcon = GameObject.Find("Selection Sprite").GetComponent<Image>();
        plates = new List<GameObject>();
        //plates.Add(plate1);
        //plates.Add(plate2);
        //plates.Add(plate3);

    }

    // Author: Nick Engell, Kyle Weekley
    /// <summary>
    /// Setup the specials for day 1
    /// </summary>
    private void SetupDayOneSpecials()
    {
        // Initialize the specials dictionary
        specials = new Dictionary<string, Sprite>();
        
        // Add the specials available for day 1
        // These names should definitely be grabbed from the cookbook json file but I don't feel like changing it right now sorry
        specials.Add("Over-Medium Fried Egg", cookedEggSprite);
        specials.Add("Long Grain White Rice", cookedRiceSprite);
        specials.Add("Spaghetti & Meatballs", cookedSpaghettiMeatballSprite);

        //Populate specials UI
        SetupSpecialsUI(specials);
    }

    // Author: Kyle Weekley
    /// <summary>
    /// Create guests for day 1
    /// </summary>
    private void SetupDayOneGuests()
    {
        //Initialize guest list
        guests = new List<GameObject>();

        //Create three guests, each ordering one of our MVP specials
        SetupGuest(guestPositionLeft, specials.Keys.ToArray()[0]);
        SetupGuest(guestPositionCenter, specials.Keys.ToArray()[1]);
        SetupGuest(guestPositionRight, specials.Keys.ToArray()[2]);
    }

    // Author: Nick Engell
    /// <summary>
    /// Turns the guest info screen off
    /// </summary>
    public void CloseInfoScreen()
    {
        // Turns the guest info screen off
        guestInfo.SetActive(false);
    }

    // Author: Kyle Weekley
    /// <summary>
    /// Populates specials UI element with the current specials
    /// </summary>
    /// <param name="currentSpecials">Dictionary of the day's specials</param>
    public void SetupSpecialsUI(Dictionary<string, Sprite> currentSpecials)
    {
        //Clear specials UI text
        specialsUIItemText.GetComponent<Text>().text = "";

        //Set Specials UI text to match available specials
        foreach (KeyValuePair<string, UnityEngine.Sprite> special in specials)
        {
            //Add special name to specials UI text followed by two line breaks
            specialsUIItemText.GetComponent<Text>().text += "- " + special.Key + "\r\n\r\n";
        }
    }

    // Author: Kyle Weekley
    /// <summary>
    /// Creates a guest with or without specified order
    /// </summary>
    /// <param name="order">Optionally specified order the guest will ask for</param>
    public void SetupGuest(Vector3 position, string order = null)
    {
        //Instantiate guest at the proper position
        GameObject newGuest = Instantiate(guest, position, Quaternion.identity);

        //Set guest as a child of canvas
        newGuest.transform.SetParent(guestCanvas.transform, false);

        //Set guest's order if specified
        //Otherwise guest will make a random order on its own
        if (order != null)
        {
            newGuest.GetComponent<Guest>().orderKeyRequested = order;
        }

        //Add guest to guest list
        guests.Add(newGuest);
    }

    // Author: Kyle Weekley
    /// <summary>
    /// Removes a guest from the guest list
    /// To be called when a guest has received their order and leaves the restaurant
    /// </summary>
    /// <param name="removedGuest">Guest object to be removed from list</param>
    public void RemoveGuestFromList(GameObject removedGuest)
    {
        guests.Remove(removedGuest);
    }

    /// <summary>
    /// Author: John Vance
    /// Purpose: Checks to see if the order on the plate is the same as the one the guest ordered
    /// </summary>
    public void CheckOrder()
    {
        plates.Clear();

        plate1 = GameObject.Find("Meal1");
        plate2 = GameObject.Find("Meal2");
        plate3 = GameObject.Find("Meal3");

        plates.Add(plate1);
        plates.Add(plate2);
        plates.Add(plate3);

        foreach (GameObject plate in plates)
        {
            if(plate.transform.childCount > 0)
            {
                // Checks the first plate
                if (plate == plate1)
                {
                    if(plate.transform.GetChild(0).gameObject.GetComponent<EggScript>() == true)
                    {
                        guests[0].GetComponent<Guest>().CompareDishAndOrder("Over-Medium Fried Egg", plate.transform.GetChild(0).gameObject);
                        Destroy(plate.transform.GetChild(0).gameObject);

                    }

                    else
                    {
                        guests[0].GetComponent<Guest>().CompareDishAndOrder(plate.transform.GetChild(0).name, plate.transform.GetChild(0).gameObject);

                    }


                }

                // Checks the second plate
                if (plate == plate2)
                {
                    if (plate.transform.GetChild(0).gameObject.GetComponent<Rice>() == true)
                    {
                        guests[1].GetComponent<Guest>().CompareDishAndOrder("Long Grain White Rice", plate.transform.GetChild(0).gameObject);
                        Destroy(plate.transform.GetChild(0).gameObject);

                    }

                    else
                    {
                        guests[1].GetComponent<Guest>().CompareDishAndOrder(plate.transform.GetChild(0).name, plate.transform.GetChild(0).gameObject);

                    }
                }

                // Checks the third plate
                if (plate == plate3)
                {
                    if (plate.transform.GetChild(0).gameObject.GetComponent<SpaghettiScript>() == true)
                    {
                        guests[2].GetComponent<Guest>().CompareDishAndOrder("Spaghetti & Meatballs", plate.transform.GetChild(0).gameObject);
                        Destroy(plate.transform.GetChild(0).gameObject);

                    }

                    else
                    {
                        guests[2].GetComponent<Guest>().CompareDishAndOrder(plate.transform.GetChild(0).name, plate.transform.GetChild(0).gameObject);

                    }
                }


            }
        }

    }

    // Author: Kyle Weekley, Ben Stern
    /// <summary>
    /// Is  called when a new scene is loaded
    /// </summary>
    /// <param name="scene">the scene being loaded</param>
    /// <param name="mode">the way the scene is being loaded</param>
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Pantry" || scene.name == "133-Pantry")
        {
            guestCanvas.enabled = false;
        }
        else if (scene.name == "Kitchen" || scene.name == "129 Kitchen" || scene.name == "130_Kitchen")
        {
            guestCanvas.enabled = false;
        }
        else if (scene.name == "Cafeteria" || scene.name == "129 Cafeteria" || scene.name == "130_Cafeteria")
        {
            guestCanvas.enabled = true;
            // You can't find an inactive gameObject, so this needs some extra steps
            guestInfo = GameObject.Find("UICanvas");
            guestInfo = guestInfo.transform.Find("Guest Info").gameObject;
            specialsUIItemText = GameObject.Find("/Canvas/SpecialsUI/ItemText");
            guestInfoCloseButton = guestInfo.GetComponentInChildren<Button>();
            // Set the close button's onClick behavior to close the menu
            // This is done here as well as in-editor because the button will lose its reference to the CafeteriaManager singleton after scene switches
            guestInfoCloseButton.onClick.AddListener(CloseInfoScreen);
        }
    }

    // Author: Kyle Weekley, Ben Stern
    /// <summary>
    /// Remove from scene manager when the program is closed
    /// </summary>
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// Author: Kyle Weekley, John Vance
	/// <summary>
	/// Attempt to select an object or interact with it in some way
    /// Added so that the objects in the cafeteria scene can be selected in interacted with
	/// </summary>
	/// <param name="selectedObject">The is attempting to be selected</param>
	public void ObjectSelected(SelectableObject selectedObject)
    {
        if (currentSelection == null)
        {
            selectionIcon = GameObject.Find("Selection Sprite").GetComponent<Image>();

            selectedObject.selected = true;
            currentSelection = selectedObject;
            selectionIcon.color = selectedObject.GetComponent<Image>().color;
            selectionIcon.sprite = selectedObject.GetComponent<Image>().sprite;
        }
        else
        {
            //TODO: call interactable object methods
            InteractableBase currentInteractable = currentSelection.GetComponent<InteractableBase>();
            InteractableBase selectedInteractable = selectedObject.GetComponent<InteractableBase>();
            if (currentInteractable != null && selectedInteractable != null)
            {
                if ((currentSelection.name == "Spatula" && selectedObject.name == "Frying Pan") ||
                    (currentSelection.name == "Spatula" && selectedObject.name == "Plate"))
                {
                    selectedInteractable.AttemptInteraction(currentInteractable);

                }

                else if(selectedInteractable == currentInteractable)
                {
                    ClearSelection();

                }

                else if (selectedInteractable.AttemptInteraction(currentInteractable))
                {
                    CheckOrder();
                    ClearSelection();
                }
            }
        }
    }

    /// <summary>
    /// Author: Kyle Weekley, John Vance
    /// Purpose: Clears the currently selected object
    /// Restrictions: None
    /// </summary>
    public void ClearSelection()
    {
        selectionIcon = GameObject.Find("Selection Sprite").GetComponent<Image>();

        if (currentSelection != null)
        {
            currentSelection.selected = false;
        }
        currentSelection = null;

        

        //Reset selection sprite
        selectionIcon.sprite = null;
        selectionIcon.color = Color.white;

        //Currently using sprite color for testing
        //selectionIcon.color = Color.white;
    }
}
