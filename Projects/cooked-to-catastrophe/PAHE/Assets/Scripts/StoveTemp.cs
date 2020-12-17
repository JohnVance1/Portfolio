using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Author: John Vance
/// Purpose: Allows for each stove burner to be set independently.
/// Restrictions: Won't fully work until the Kitchen Object Interactions section is finished.
/// </summary>
public class StoveTemp : MonoBehaviour
{
    [SerializeField]
    private int burnerTR;       // Top Right Burner temp

    [SerializeField]
    private int burnerBR;       // Bottom Right Burner temp

    [SerializeField]
    private int burnerBL;       // Bottom Left Burner temp

    [SerializeField]
    private int burnerTL;       // Top Left Burner temp


    public GameObject statusGameObjectBR;  // The status' GameObject to get the text
    private Text statusTextBR;  // The burner's status

    
    public GameObject statusGameObjectBL;  // The status' GameObject to get the text
    private Text statusTextBL;  // The burner's status

    
    public GameObject statusGameObjectTR;  // The status' GameObject to get the text
    private Text statusTextTR;  // The burner's status

    
    public GameObject statusGameObjectTL;  // The status' GameObject to get the text
    private Text statusTextTL;  // The burner's status

    

    #region Getters/Setters
    /// <summary>
    /// Getter/Setter for Top Right burner
    /// </summary>
    /// <returns></returns>
    public int BurnerTR
    {
        get { return burnerTR; }
        set
        {
            burnerTR = value;

            // Updates the burner's current status
            ChangeBurnerStatus(burnerTR, statusTextTR);

        }
    }


    /// <summary>
    /// Getter/Setter for Bottom Right burner
    /// </summary>
    /// <returns></returns>
    public int BurnerBR
    {
        get { return burnerBR; }
        set
        {
            burnerBR = value;

            // Updates the burner's current status
            ChangeBurnerStatus(burnerBR, statusTextBR);

        }
    }

    /// <summary>
    /// Getter/Setter for Bottom Left burner
    /// </summary>
    /// <returns></returns>
    public int BurnerBL
    {
        get { return burnerBL; }
        set
        {
            burnerBL = value;

            // Updates the burner's current status
            ChangeBurnerStatus(burnerBL, statusTextBL);

        }

    }

    /// <summary>
    /// Getter/Setter for Top Left burner
    /// </summary>
    /// <returns></returns>
    public int BurnerTL
    {
        get { return burnerTL; }
        set
        {
            burnerTL = value;

            // Updates the burner's current status
            ChangeBurnerStatus(burnerTL, statusTextTL);
        }
    }
    #endregion


    /// <summary>
    /// Author: John Vance
    /// Purpose: Sets the burners to default values.
    /// Restrictions: None.
    /// </summary>
    void Start()
    {
        burnerTR = 0;
        burnerBR = 0;
        burnerBL = 0;
        burnerTL = 0;

        statusTextBL = statusGameObjectBL.GetComponent<Text>();
        statusTextBR = statusGameObjectBR.GetComponent<Text>();
        statusTextTL = statusGameObjectTL.GetComponent<Text>();
        statusTextTR = statusGameObjectTR.GetComponent<Text>();

    }

    /// <summary>
    /// Author: John Vance
    /// Purpose: Allows for the Burner's status to be displayed in the UI
    /// </summary>
    /// <param name="burnerTemp">The temperature of the burner</param>
    /// <param name="burnerText">The text component to write out to</param>
    public void ChangeBurnerStatus(int burnerTemp, Text burnerText)
    {
        switch (burnerTemp)
        {
            case 0:
                burnerText.text = "Burner Status: Off";
                break;
            case 1:
                burnerText.text = "Burner Status: Low";
                break;
            case 2:
                burnerText.text = "Burner Status: Medium";
                break;
            case 3:
                burnerText.text = "Burner Status: High";
                break;
        }

    }

    // Author: Trenton Plager
    /// <summary>
    /// Returns the temp of the passed-in burner
    /// </summary>
    /// <param name="burner">The burner to return the temp of</param>
    /// <returns></returns>
    public int GetBurnerTemp(StoveLocation burner)
    {
        switch(burner)
        {
            case StoveLocation.BottomLeftBurner:
                return burnerBL;
            case StoveLocation.BottomRightBurner:
                return burnerBR;
            case StoveLocation.TopLeftBurner:
                return burnerTL;
            case StoveLocation.TopRightBurner:
                return burnerTR;
            default:
                return 0;
        }
    }
}

