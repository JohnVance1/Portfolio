using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

// Author: Trenton Plager
/// <summary>
/// Purpose: A script to hold all of the references needed for the Cookbook's UI and contain all of its functionality
/// Restrictions: None
/// </summary>
public class CookbookUI : MonoBehaviour
{
    #region Fields
    // The cookbook serializable object
    [SerializeField]
    private Cookbook cookbook;

    // The current page index of the cookbook
    [SerializeField]
    private int currentRecipeIndex = 0;

    // A reference to the text that contains the current recipe's title
    [SerializeField]
    private Text recipeTitle;

    // A reference to the panel that contains all of the ingredient objects
    [SerializeField]
    private GameObject ingredientsPanel;

    // A reference to the panel that contains all of the instruction objects
    [SerializeField]
    private GameObject instructionsPanel;

    // A reference to the prefab used to instantiate more ingredients if there aren't enough
    [SerializeField]
    private GameObject ingredientsPrefab;

    // A reference to the prefab used to instantiate more instructions if there aren't enough
    [SerializeField]
    private GameObject instructionsPrefab;

    // A reference to the image that indicates whether the recipe is on the specials list or not
    [SerializeField]
    private GameObject isSpecialIndicator;

    // A list of strings that contains all of the specials for the day
    [SerializeField]
    private List<string> specials = new List<string>();

    // A dictionary that contains all of the orders for the day and their quantities
    [SerializeField]
    private Dictionary<string, int> orders = new Dictionary<string, int>();

    // A reference to the text object that displays how many of a recipe has been ordered
    [SerializeField]
    private Text orderQuantity;

    // A reference to the CafeteriaManager gameObject
    [SerializeField]
    private CafeteriaManager cafeteriaManager;
    #endregion

    #region Methods
    // Author: Trenton Plager, Kyle Weekley
    /// <summary>
    /// Purpose: Updates the cookbook display to the current recipe index (which should be 0) 
    /// Obtains reference to CafeteriaManager to inform the specials and orders lists
    /// Restrictions: None
    /// </summary>
    void Start()
    {
        // The cookbook needs to find the CafeteriaManager, which may not exist based on scenes entered
        GameObject CafeteriaManagerObject = GameObject.Find("CafeteriaManager");

        // If the CafeteriaManager does exist, grab the script from it
        // Then set up the specials and orders info
        if (CafeteriaManagerObject)
        {
            cafeteriaManager = CafeteriaManagerObject.GetComponent<CafeteriaManager>();
            specials = new List<string>(cafeteriaManager.Specials.Keys);
            orders = cafeteriaManager.Orders;
        }

        UpdateCookbookDisplay();
    }

    // Author: Kyle Weekley
    /// <summary>
    /// Purpose: Updates current page when opening cookbook to reflect current order quantity
    /// Restrictions: None
    /// </summary>
    private void OnEnable()
    {
        UpdateCookbookDisplay();
    }

    // Author: Trenton Plager
    /// <summary>
    /// Purpose: Goes to the next recipe in the cookbook by advancing the current index and updating the display
    /// Restrictions: None
    /// </summary>
    public void NextRecipe()
    {
        if (currentRecipeIndex + 1 < cookbook.recipes.Length && cookbook.recipes[currentRecipeIndex + 1].unlocked)
        {
            currentRecipeIndex++;
            UpdateCookbookDisplay();
        }
        else
        {
            Debug.Log("No more unlocked recipes."); 
        }
    }

    // Author: Trenton Plager
    /// <summary>
    /// Purpose: Goes to the previous recipe in the cookbook by decrementing the current index and updating the display
    /// Restrictions: None
    /// </summary>
    public void PreviousRecipe()
    {
        if (currentRecipeIndex > 0)
        {
            currentRecipeIndex--;
            UpdateCookbookDisplay();
        }
        else
        {
            Debug.Log("You have reached the first page.");
        }
    }

    // Author: Trenton Plager, Kyle Weekley
    /// <summary>
    /// Purpose: Updates the cookbook display by changing the recipe title and updating the ingredients and instructions lists
    /// Restrictions: None
    /// </summary>
    public void UpdateCookbookDisplay()
    {
        // Updates the title on the page
        recipeTitle.text = cookbook.recipes[currentRecipeIndex].name; 
        
        // Sets the isSpecial indicator if the name of the recipe is contained in the specials list
        isSpecialIndicator.SetActive(specials.Contains(cookbook.recipes[currentRecipeIndex].name));

        // Sets the order quantity
        // Check if orders list contains this recipe
        if (orders.ContainsKey(cookbook.recipes[currentRecipeIndex].name))
        {
            orderQuantity.text = "x" + orders[cookbook.recipes[currentRecipeIndex].name].ToString();
        }
        //If not, set quantity to zero
        else
        {
            orderQuantity.text = "x0";
        }
        

        // Loops through all the ingredients in the recipe
        for (int i = 0; i < cookbook.recipes[currentRecipeIndex].ingredients.Length; i++)
        {
            // Updates any currently existing ingredient objects in the list display 
            // Then creates more if necessary
            string ingredient = cookbook.recipes[currentRecipeIndex].ingredients[i];
            try
            {
                GameObject ingredientObject = ingredientsPanel.transform.GetChild(i + 1).gameObject;
                ingredientObject.SetActive(true);
                ingredientObject.name = ingredient;
                ingredientObject.GetComponentInChildren<Text>().text = ingredient;
            }
            catch
            {
                GameObject ingredientObject = Instantiate(ingredientsPrefab, ingredientsPanel.transform);
                ingredientObject.name = ingredient;
                ingredientObject.GetComponentInChildren<Text>().text = ingredient;
            }
        }

        // Sets any ingredient list objects in the display to inactive if they aren't needed
        if (ingredientsPanel.transform.childCount - 1 > cookbook.recipes[currentRecipeIndex].ingredients.Length)
        {
            for (int i = cookbook.recipes[currentRecipeIndex].ingredients.Length + 1; i < ingredientsPanel.transform.childCount; i++)
            {
                ingredientsPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        // Loops through all the instructions in the recipe
        for (int i = 0; i < cookbook.recipes[currentRecipeIndex].instructions.Length; i++)
        {
            // Updates any currently existing instruction objects in the list display 
            // Then creates more if necessary
            string instruction = cookbook.recipes[currentRecipeIndex].instructions[i];
            try
            {
                GameObject instructionObject = instructionsPanel.transform.GetChild(i + 1).gameObject;
                instructionObject.SetActive(true);
                instructionObject.name = instruction;
                instructionObject.GetComponentInChildren<Text>().text = instruction;
            }
            catch
            {
                GameObject instructionObject = Instantiate(instructionsPrefab, instructionsPanel.transform);
                instructionObject.name = instruction;
                instructionObject.GetComponentInChildren<Text>().text = instruction;
            }
        }

        // Sets any instruction list objects in the display to inactive if they aren't needed
        if (instructionsPanel.transform.childCount - 1 > cookbook.recipes[currentRecipeIndex].instructions.Length)
        {
            for (int i = cookbook.recipes[currentRecipeIndex].instructions.Length + 1; i < instructionsPanel.transform.childCount; i++)
            {
                instructionsPanel.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
    #endregion
}
