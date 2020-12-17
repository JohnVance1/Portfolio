using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class EggTimerManager : MonoBehaviour
{

    [SerializeField] private GameObject timerMenu;
    [SerializeField] private GameObject timeToSkipText;
    [SerializeField] private GameObject timeElapsedText;

    [SerializeField] private GameObject secondsInputField;
    [SerializeField] private GameObject minutesInputField;
    [SerializeField] private GameObject hoursInputField;

    public List<GameObject> listOfUtensils;

    // In seconds
    private float timeElapsed;
    private float timeToSkip;

    private StoveTemp stoveTemps;

    // Author: Nick Engell
    /// <summary>
    /// Gives starting values to the variables
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        timeToSkip = 0;

        // Gets a reference to the stove
        stoveTemps = GetComponent<KitchenManager>().Stove.GetComponent<StoveTemp>();
    }

    // Author: Nick Engell
    /// <summary>
    /// Limits the timeToSkip variable and keeps the timer menu text updated
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        // Prevent time to skip from going below 0
        if(timeToSkip <= 0)
        {
            timeToSkip = 0;
        }

        // Update the time elapsed and time to skip text on the menu
        timeElapsedText.GetComponent<Text>().text = ConvertTimeToString(timeElapsed);
        timeToSkipText.GetComponent<Text>().text = ConvertTimeToString(timeToSkip);
    }

    // Nick Engell
    /// <summary>
    /// Converts seconds to a formated string
    /// </summary>
    /// <param name="seconds">Seconds to be converted</param>
    /// <returns>A formated time string</returns>
    public string ConvertTimeToString(float seconds)
    {
        // Converts seconds into a TimeSpan object
        TimeSpan convertedSeconds = TimeSpan.FromSeconds(seconds);

        // Formats it and returns it as a string
        return convertedSeconds.ToString(@"hh\:mm\:ss");
    }

    // Author: Nick Engell
    /// <summary>
    /// Propety for time elapsed
    /// </summary>
    public float TimeElapsed
    {
        get { return timeElapsed; }
    }
    // Author: Nick Engell
    /// <summary>
    /// Property for time to skip
    /// </summary>
    public float TimeToSkip
    {
        get { return timeToSkip; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Increments the seconds to pass field
    /// </summary>
    public void AddSeconds()
    {
        // If the input field for seconds is empty or set to 00 (default)
        if(secondsInputField.GetComponent<InputField>().text == "" || secondsInputField.GetComponent<InputField>().text == "00")
        {
            // Increase the time to skip by 1 second
            timeToSkip += 1;
        }
        else
        {
            // Otherwise increase the time to skip by how many seconds are in the input field
            timeToSkip += int.Parse(secondsInputField.GetComponent<InputField>().text);
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Decrements the seconds to pass field
    /// </summary>
    public void SubtractSeconds()
    {
        // If the input field for seconds is empty or set to 00 (default)
        if (secondsInputField.GetComponent<InputField>().text == "" || secondsInputField.GetComponent<InputField>().text == "00")
        {
            // Decrease the time to skip by 1 second
            timeToSkip -= 1;
        }
        else
        {
            // Otherwise decrease the time to skip by how many seconds are in the input field
            timeToSkip -= int.Parse(secondsInputField.GetComponent<InputField>().text);
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Increments the minutes to pass field
    /// </summary>
    public void AddMinutes()
    {
        // If the input field for minutes is empty or set to 00 (default)
        if (minutesInputField.GetComponent<InputField>().text == "" || minutesInputField.GetComponent<InputField>().text == "00")
        {
            // Increase the time to skip by 1 minute
            timeToSkip += 60;
        }
        else
        {
            // Otherwise increase the time to skip by how many minutes are in the input field
            timeToSkip += int.Parse(minutesInputField.GetComponent<InputField>().text) * 60;
        }
    }
    // Author: Nick Engell
    /// <summary>
    /// Decrements the minutes to pass field
    /// </summary>
    public void SubtractMinutes()
    {
        // If the input field for minutes is empty or set to 00 (default)
        if (minutesInputField.GetComponent<InputField>().text == "" || minutesInputField.GetComponent<InputField>().text == "00")
        {
            // Decrease the time to skip by 1 minutes
            timeToSkip -= 60;
        }
        else
        {
            // Otherwise decrease the time to skip by how many minutes are in the input field
            timeToSkip -= int.Parse(minutesInputField.GetComponent<InputField>().text) * 60;
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Increments the hours to pass field
    /// </summary>
    public void AddHours()
    {
        // If the input field for hours is empty or set to 00 (default)
        if (hoursInputField.GetComponent<InputField>().text == "" || hoursInputField.GetComponent<InputField>().text == "00")
        {
            // Increase the time to skip by 1 hour
            timeToSkip += 3600;
        }
        else
        {
            // Otherwise increase the time to skip by how many hours are in the input field
            timeToSkip += int.Parse(hoursInputField.GetComponent<InputField>().text) * 3600;
        }
    }
    // Author: Nick Engell
    /// <summary>
    /// Decrements the hours to pass field
    /// </summary>
    public void SubtractHours()
    {
        // If the input field for hours is empty or set to 00 (default)
        if (hoursInputField.GetComponent<InputField>().text == "" || hoursInputField.GetComponent<InputField>().text == "00")
        {
            // Decrease the time to skip by 1 hours
            timeToSkip -= 3600;
        }
        else
        {
            // Otherwise decrease the time to skip by how many hours are in the input field
            timeToSkip -= int.Parse(hoursInputField.GetComponent<InputField>().text) * 3600;
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Opens the timer menu
    /// </summary>
    public void OpenMenu()
    {
        // Open the menu
        timerMenu.SetActive(true);
    }
    // Author: Nick Engell
    /// <summary>
    /// Closes the timer menu and resets the time to skip fields
    /// </summary>
    public void CloseMenu()
    {
        // Close the menu
        timerMenu.SetActive(false);

        // Reset time to skip
        timeToSkip = 0;

        // Reset time input fields
        secondsInputField.GetComponent<InputField>().text = "";
        minutesInputField.GetComponent<InputField>().text = "";
        hoursInputField.GetComponent<InputField>().text = "";
    }

    // Author: Nick Engell
    /// <summary>
    /// Advances time by moving the time to pass to the elapsed time field
    /// </summary>
    public void AdvanceTime()
    {
        // Close the menu
        timerMenu.SetActive(false);

        // Reset time input fields
        secondsInputField.GetComponent<InputField>().text = "";
        minutesInputField.GetComponent<InputField>().text = "";
        hoursInputField.GetComponent<InputField>().text = "";

        // If a burner is on
        if(CheckIfAStoveBurnerIsOn())
        {
            // Attempt to cook the food in the utensils on the stove
            AttemptToCookObjects();
        }

        // Increase time elapsed by time to skip
        timeElapsed += timeToSkip;
        // Reset time to skip
        timeToSkip = 0;
    }

    // Author: Nick Engell
    /// <summary>
    /// Check if any of the burners are on, and return true if so
    /// </summary>
    /// <returns>If any burner is on</returns>
    private bool CheckIfAStoveBurnerIsOn()
    {
        // If any of the burners are on
        if(stoveTemps.BurnerTL > 0 || stoveTemps.BurnerTR > 0 || stoveTemps.BurnerBL > 0 || stoveTemps.BurnerBR > 0)
        {
            return true;
        }

        return false;
    }

    // Author: Nick Engell, Trenton Plager
    /// <summary>
    /// Goes through the utensils and attemps to cook the food in them
    /// </summary>
    private void AttemptToCookObjects()
    {
        // Loop through all the utensils
        for(int i = 0; i < listOfUtensils.Count; i ++)
        {
            // Store a reference to the current utensil
            CookingUtensil currentUtensil = listOfUtensils[i].GetComponent<CookingUtensil>();
            // If the current one is on the stove and there is a food in it
            if(currentUtensil.IsOnStove && currentUtensil.FoodsInside != null)
            {
                // Store a reference to the current food in the utensil
                CookableObject[] currentFoods = currentUtensil.FoodsInside;

                for (int j = 0; j < currentFoods.Length; j++)
                {
                    CookableObject currentFood = currentFoods[j];
                    // Check which burner it's on

                    CookFoodHelper(currentFood, currentUtensil.CurrentBurner);
                }
            }
        }
    }

    // Author: Trenton Plager
    /// <summary>
    /// A helper method that cooks food depending on the current burner it is on
    /// </summary>
    /// <param name="currentFood">The food that needs to be cooked</param>
    /// <param name="burner">The burner that the food is on</param>
    public void CookFoodHelper(CookableObject currentFood, StoveLocation burner)
    {
        int burnerTemp = stoveTemps.GetBurnerTemp(burner);

        switch (burnerTemp)
        {
            case 1:
                currentFood.Cook(timeToSkip * currentFood.LowHeatMultiplier); 
                break;
            case 2:
                currentFood.Cook(timeToSkip * currentFood.MediumHeatMultiplier);
                break;
            case 3:
                currentFood.Cook(timeToSkip * currentFood.HighHeatMultiplier);
                break;
        }
    }
}
