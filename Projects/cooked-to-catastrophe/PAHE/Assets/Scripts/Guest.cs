using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Guest : MonoBehaviour
{
    // Author: Nick Engell, Kyle Weekley
    // Whether or not the guest has a key for the player
    private bool hasKey;
    // Whether or not the guest's order has been taken
    private bool orderTaken;
    // A list of possible dialogue lines
    private List<string> dialogueLines;
    // Sprite for guest's neutral face
    [SerializeField] private Sprite neutralFace;
    // Sprite for guest's happy face
    [SerializeField] private Sprite happyFace;
    // Sprite for guest's sad face
    [SerializeField] private Sprite sadFace;
    // Reference to the gameManager of the scene
    private GameObject gameManager;
    // Quick reference to the cafeteria manager
    private CafeteriaManager cafeteriaManager;
    // The key of what order the guest wants from the specials list
    public string orderKeyRequested;
    // The unique greeting line for this specific guest
    private string uniqueGreetingLine;

    // Checks to see if the order has been given to the guest
    private int orderServed;


    // Author: Kyle Weekley
    /// <summary>
    /// Used to set DontDestroyOnLoad
    /// Guests should be remembered despite moving to other scenes
    /// </summary>
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialized the dialogueLines list
        dialogueLines = new List<string>();
        // Populated the list with some possible lines
        dialogueLines.Add("Man, it smells so good in here compared to outside.");
        dialogueLines.Add("Ooh, this is gonna be good!");
        dialogueLines.Add("Can't really find much good food out there.");

        // Get reference to GameManager (Kyle: Added single .find here since guests are now being instantiated at runtime)
        gameManager = GameObject.Find("CafeteriaManager");

        // Quick reference to the cafeteria manager script
        cafeteriaManager = gameManager.GetComponent<CafeteriaManager>();

        // If an order hasn't already been specified, pick a random order from the available specials when they are initally created
        if (orderKeyRequested == null)
        {
            PickOrder();
        }

        // Give the guest a unique dialogue line when they are initally created
        uniqueGreetingLine = GetRandomDialogueLine();
    }

    // Author: Nick Engell
    /// <summary>
    /// Returns a random dialogue line from the list of possible dialogue lines
    /// </summary>
    /// <returns>A random string from the list of dialogue lines</returns>
    public string GetRandomDialogueLine()
    {
        return dialogueLines[Random.Range(0, dialogueLines.Count)];
    }

    // Author: Nick Engell
    /// <summary>
    /// Gets a random order from the available specials
    /// </summary>
    public void PickOrder()
    {
        // Pick a random order from the dictionary of specials
        orderKeyRequested = cafeteriaManager.Specials.Keys.ToArray()[(int)Random.Range(0, cafeteriaManager.Specials.Keys.Count)];
    }

    // Author: Nick Engell
    /// <summary>
    /// Checks to see if the dish passed in is the one the guest ordered
    /// </summary>
    /// <param name="orderToCompare"></param>
    public void CompareDishAndOrder(string orderToCompare, GameObject food)
    {
        // If the incoming order is what the guest initally ordered
        if(orderToCompare == orderKeyRequested)
        {
            if(food.GetComponent<CookableObject>() != null)
            {
                if(food.GetComponent<CookableObject>().IsCooked == true)
                {
                    orderServed = 1;


                }
                else
                {
                    orderServed = 3;


                }
                UpdateGuestInfo();


            }
            //cafeteriaManager.GuestInfo.SetActive(true);
            // Not entirely sure how inefficient this is. I could find it by manually getting the indexes but it'd be messy to read
            // Sets the guest picture in the info screen to happy
            //cafeteriaManager.GuestInfo.transform.Find("Border/Guest Picture").gameObject.GetComponent<Image>().sprite = happyFace;

        }
        else
        {
            //cafeteriaManager.GuestInfo.SetActive(true);
            // Not entirely sure how inefficient this is. I could find it by manually getting the indexes but it'd be messy to read
            // Sets the guest picture in the info screen to sad
            //cafeteriaManager.GuestInfo.transform.Find("Border/Guest Picture").gameObject.GetComponent<Image>().sprite = sadFace;
            orderServed = 2;
            UpdateGuestInfo();

        }
    }

    // Author: Nick Engell, Kyle Weekley, John Vance
    /// <summary>
    /// Updates the guest info based on the current guest clicked on
    /// </summary>
    public void UpdateGuestInfo()
    {
        // I used transform.Find() here and i'm not entirely sure how inefficient it is. I could find it by manually getting the indexes but it'd be messy to read

        // If the guest info screen isn't already open
        if(cafeteriaManager.GuestInfo.activeSelf == false)
        {
            // Open it
            cafeteriaManager.GuestInfo.SetActive(true);

            // Checks if the order was already served to the guest depending on 
            // if it was what the guest ordered or not their mood changes
            switch(orderServed)
            {
                // Neutral face: The default value
                case 0:
                    // Update the guest face with their neutral face
                    cafeteriaManager.GuestInfo.transform.Find("Border/Guest Picture").gameObject.GetComponent<Image>().sprite = neutralFace;

                    // Update the meal text with the dish name
                    cafeteriaManager.GuestInfo.transform.Find("Meal Background/Request Text").gameObject.GetComponent<Text>().text = string.Format("{0}, please!", orderKeyRequested);

                    break;

                // Happy face: If the dish is what they ordered
                case 1:
                    cafeteriaManager.GuestInfo.transform.Find("Border/Guest Picture").gameObject.GetComponent<Image>().sprite = happyFace;

                    cafeteriaManager.GuestInfo.transform.Find("Meal Background/Request Text").gameObject.GetComponent<Text>().text = string.Format("Thanks for the {0}!", orderKeyRequested);

                    break;

                // Sad face: If the dish is NOT what they ordered
                case 2:
                    cafeteriaManager.GuestInfo.transform.Find("Border/Guest Picture").gameObject.GetComponent<Image>().sprite = sadFace;

                    cafeteriaManager.GuestInfo.transform.Find("Meal Background/Request Text").gameObject.GetComponent<Text>().text = string.Format("This isn't {0}!", orderKeyRequested);

                    break;
                // Sad Face: If the food they ordered is correct but on cooked properly
                case 3:
                    cafeteriaManager.GuestInfo.transform.Find("Border/Guest Picture").gameObject.GetComponent<Image>().sprite = sadFace;

                    cafeteriaManager.GuestInfo.transform.Find("Meal Background/Request Text").gameObject.GetComponent<Text>().text = string.Format("{0} is what I ordered, but it's not cooked properly.", orderKeyRequested);

                    break;

            }

            // Update the dish icon with the correct dish
            cafeteriaManager.GuestInfo.transform.Find("Meal Background/Plate Background/Dish Requested").gameObject.GetComponent<Image>().sprite = cafeteriaManager.Specials[orderKeyRequested];
            
            // Update the greeting text with their unique dialogue line with a "- " at the beginning
            cafeteriaManager.GuestInfo.transform.Find("Greeting Background/Greeting Text").gameObject.GetComponent<Text>().text = "- " + uniqueGreetingLine;

            // Check if this guest's order has already been taken
            if (orderTaken == false)
            {
                // Check if the guest's order is already present in the orders list
                if (cafeteriaManager.Orders.ContainsKey(orderKeyRequested))
                {
                    // If it is, increment the count of that order by 1
                    cafeteriaManager.Orders[orderKeyRequested]++;
                }
                else
                {
                    // If not, add the guest's order to the orders dictionary with a quantity of 1
                    cafeteriaManager.Orders.Add(orderKeyRequested, 1);
                }

                // Set the guest's order as taken
                orderTaken = true;
            }

            foreach (KeyValuePair<string, int> kvp in cafeteriaManager.Orders)
            {
                //textBox3.Text += ("Key = {0}, Value = {1}", kvp.Key, kvp.Value);
                Debug.Log(string.Format("Key = {0}, Value = {1}", kvp.Key, kvp.Value));
            }
        }
    }
}
