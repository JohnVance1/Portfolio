using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Trenton Plager
/// <summary>
/// Purpose: A data container to assist in reading in recipes from the json file
/// Restrictions: None
/// </summary>
[System.Serializable]
public class Recipe
{
    #region Fields
    // The name of the recipe
    public string name;

    // A string array of ingredients in the recipe and their quantities
    public string[] ingredients;

    // A string array of instructions for the recipe
    // Should be in a numbered list
    public string[] instructions;

    // A string array of sources that were used to create the recipe for citation purposes
    public string[] sources;

    // A boolean indicating whether the recipe has been unlocked or not
    public bool unlocked; 
    #endregion
}
