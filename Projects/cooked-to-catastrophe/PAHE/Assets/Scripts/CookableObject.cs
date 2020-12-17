using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookableObject : MonoBehaviour
{

	private bool isCooked;
	private bool isBurnt;

	public float timeElapsed;

	// In seconds, how long till it will be done, will be counted down
	[SerializeField] private float timeToCook;
	// In seconds, how long till it will be burnt, will be counter down
	[SerializeField] private float timeToBurn;

	// How much elapsed time will count. So if 10 seconds have passed and the multiplier is 1.5, it will count as 15 seconds
	[SerializeField] private float lowHeatMultiplier;
	[SerializeField] private float mediumHeatMultiplier;
	[SerializeField] private float highHeatMultiplier;

	// How much space the object will take up in the cart
	[SerializeField] private float size;

	// A delegate that optionally overides on cook when set;
	public Func<bool> OnCookOveride;

    // Determines if the food is currently being cooked
    private bool currentlyBeingCooked;

    // Author: Nick Engell
    /// <summary>
    /// Property for how much space the object will take up in the cart
    /// </summary>
    public float Size
    {
        get { return size; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Status of whether the food is cooked or not
    /// </summary>
    public bool IsCooked
    {
        get { return isCooked; }
        set { isCooked = value; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Status of whether the food is burnt or not
    /// </summary>
    public bool IsBurnt
    {
        get { return isBurnt; }
        set { isBurnt = value; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for how long left till the food is cooked, and determines when the food is cooked
    /// </summary>
    public float TimeToCook
    {
        get { return timeToCook; }
        set 
        { 
            timeToCook = value;
            currentlyBeingCooked = true;
            if(timeToCook <= 0)
            {
                isCooked = true;
            }
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for how long left till the food is burnt, and determins when the food is burnt
    /// </summary>
    public float TimeToBurn
    {
        get { return timeToBurn; }
        set 
        { 
            timeToBurn = value;
            currentlyBeingCooked = true;
            if(timeToBurn <= 0)
            {
                isBurnt = true;
            }
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for how much elapsed time will count when on low heat
    /// </summary>
    public float LowHeatMultiplier
    {
        get { return lowHeatMultiplier; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for how much elapsed time will count when on medium heat
    /// </summary>
    public float MediumHeatMultiplier
    {
        get { return mediumHeatMultiplier; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for how much elapsed time will count when on high heat
    /// </summary>
    public float HighHeatMultiplier
    {
        get { return highHeatMultiplier; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for whether the food has started / is currently being cooked
    /// </summary>
    public bool CurrentlyBeingCooked
    {
        get { return currentlyBeingCooked; }
    }

    /// <summary>
    /// Updates the time and updates the cooked and burnt fields
    /// </summary>
    /// <param name="time"></param>
    public void Cook(float time)
    {
        timeElapsed += time;
        //Check if anything overides the on cook
        if (OnCookOveride == null || !OnCookOveride())
        {
            if (timeElapsed >= timeToBurn)
            {
                isBurnt = true;
                isCooked = true;
            }
            else if (timeElapsed >= timeToCook)
            {
                isCooked = true;
            }
        }
    }
}
