using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Author: Trenton Plager
/// Purpoose: Right now, this class just closes rooms
/// However, I suspect that it will have more uses as we continue to flesh out the pantry
/// Restrictions: None
/// </summary>
public class PantryManager : MonoBehaviour
{
    #region Fields
    // The parent object of all of the rooms in the main Overview canvas in the pantry
    private GameObject roomsContainer;
    #endregion

    #region Methods
    ///Author: Trenton Plager
    /// <summary>
    /// Sets the roomsContainer object so that the reference is maintained
    /// even if it is set to be inactive
    /// </summary>
    void Start()
    {
        roomsContainer = GameObject.Find("/OverviewCanvas/Rooms");
    }

    /// Author: Trenton Plager
    /// <summary>
    /// Closes the submenu canvas in the pantry and goes back to the display of rooms
    /// </summary>
    public void CloseRoom()
    {
        roomsContainer.SetActive(true);
        GameObject subMenuCanvas = GameObject.Find("SubMenuCanvas");

        subMenuCanvas.GetComponent<Canvas>().enabled = false;
    }
    #endregion
}
