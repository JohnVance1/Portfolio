using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// Author: Trenton Plager
/// <summary>
/// Purpose: Reads in the information from the json file to the Cookbook Scriptable Object
/// Restrictions: None
/// </summary>
public class JSONRecipeReader : MonoBehaviour
{
    #region Fields
    // The json file, assigned in the inspector
    [SerializeField]
    private TextAsset jsonFile;

    // The cookbook seriazable object, assigned in the inspector
    [SerializeField]
    private Cookbook cookbook;
    #endregion

    #region Methods
    // Author: Trenton Plager
    /// <summary>
    /// Purpose: Overwrites the current values in the cookbook object on start 
    ///          So that it matches up with the values in the json file
    /// Restrictions: None
    /// </summary>
    void Start()
    {        
        JsonUtility.FromJsonOverwrite(jsonFile.text, cookbook);

        //foreach (Recipe recipe in cookbook.recipes)
        //{
        //    Debug.Log($"Found Recipe: {recipe.name}");
        //}
    }
    #endregion
}
