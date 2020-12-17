using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author: Trenton Plager
public enum MeatballStates
{
    Bagged, 
    LooseFrozen, 
    LooseCoooked, 
    LooseBurned
}

/// <summary>
/// A script to manage the state and script updating of the meatballs
/// </summary>
[RequireComponent(typeof(InteractableBase))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CookableObject))]
public class MeatballScript : MonoBehaviour
{
    //various components of that the script keeps track of
    private InteractableBase interactableComponent;
    private Image imageComponent;

    /// <summary>
    /// The current state of the meatball
    /// </summary>
    private MeatballStates meatballState;

    /// <summary>
    /// The image representing the meatballs when they are still in the bag
    /// </summary>
    [SerializeField]
    private Sprite baggedImage;

    /// <summary>
    /// the image repsenting the meatballs when they are frozen
    /// </summary>
    [SerializeField]
    private Sprite frozenImage;

    /// <summary>
    /// the image representing the meatballs when they are cooked properly
    /// </summary>
    [SerializeField]
    private Sprite cookedImage;

    /// <summary>
    /// the image representing the meatballs when they are burned
    /// </summary>
    [SerializeField]
    private Sprite burnedImage;

    void Start()
    {
        //get the interactable component and add interactions to it
        interactableComponent = GetComponent<InteractableBase>();
        interactableComponent.AddInteractionToList("On Place In Container", OnPlaceInContainer);

        //get the image component and set the sprite
        imageComponent = GetComponent<Image>();

        ChangeMeatballState(MeatballStates.Bagged);
    }

    // Author: Trenton Plager
    /// <summary>
    /// Updates the meatball state and image based on the food cook state
    /// </summary>
    private void Update()
    {
        CookableObject test = GetComponent<CookableObject>(); 

        // If the egg is fully cooked
        if (GetComponent<CookableObject>().IsCooked)
        {
            // Update it's state and image
            ChangeMeatballState(MeatballStates.LooseCoooked);
        }

        // If the sauce is scorched
        if (GetComponent<CookableObject>().IsBurnt)
        {
            ChangeMeatballState(MeatballStates.LooseBurned);
        }
    }

    //takes an interactable base so it can be a delegate
    /// <summary>
    /// A function to call when the meatball is placed in to a container
    /// </summary>
    /// <param name="container">the container the egg is being placed in</param>
    public void OnPlaceInContainer(InteractableBase container)
    {
        if (meatballState == MeatballStates.Bagged)
        {
            ChangeMeatballState(MeatballStates.LooseFrozen);
        }
    }

    /// <summary>
    /// A helper method to change the meatball state and sprite depending on the new state
    /// </summary>
    /// <param name="newState"></param>
    public void ChangeMeatballState(MeatballStates newState)
    {
        meatballState = newState;

        switch (meatballState)
        {
            case MeatballStates.Bagged:
                imageComponent.sprite = baggedImage;
                break;
            case MeatballStates.LooseBurned:
                imageComponent.sprite = burnedImage;
                break;
            case MeatballStates.LooseCoooked:
                imageComponent.sprite = cookedImage;
                break;
            case MeatballStates.LooseFrozen:
                imageComponent.sprite = frozenImage;
                break;
            default:
                imageComponent.sprite = frozenImage;
                break;
        }
    }
}
