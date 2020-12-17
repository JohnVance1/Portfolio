using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StoveLocation
{
    TopLeftBurner,
    TopRightBurner,
    BottomLeftBurner,
    BottomRightBurner
}

public class CookingUtensil : MonoBehaviour
{
    [SerializeField] private bool isOnStove;
    private GameObject foodInside;

    [SerializeField] private StoveLocation currentBurner;


    // Author: Nick Engell
    /// <summary>
    /// Property for if the object is on the stove
    /// </summary>
    public bool IsOnStove
    {
        get { return isOnStove; }
        set { isOnStove = value; }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for foodInside variable, checks to see if the object has any children. If so, then return it
    /// </summary>
    public CookableObject[] FoodsInside
    {
        get
        {
            if(gameObject.transform.childCount >= 1)
            {
                return gameObject.transform.GetComponentsInChildren<CookableObject>();
            }
            else
            {
                return null;
            }
        }
    }

    // Author: Nick Engell
    /// <summary>
    /// Property for the current burner
    /// </summary>
    public StoveLocation CurrentBurner
    {
        get { return currentBurner; }
        set { currentBurner = value; }
    }

}
