using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Author: Trenton Plager
/// <summary>
/// Purpose: A scriptable object that contains all the data in the cookbook
/// Restrictions: None
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "Cookbook", menuName = "ScriptableObjects/Cookbook", order = 1)]
public class Cookbook : ScriptableObject
{
    #region Fields
    // An array of all the recipes contained in the cookbook
    public Recipe[] recipes; 
    #endregion
}
