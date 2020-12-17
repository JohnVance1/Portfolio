using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Jason Kulp (based on MeatballScript by Trenton Plager)
public enum SpaghettiState
{
    Boxed,
    LooseUncooked,
    LooseCooked,
    LooseBurned
}

/// <summary>
/// Handles the logic for cooking the spaghetti
/// </summary>
[RequireComponent(typeof(SelectableObject))]
[RequireComponent(typeof(InteractableBase))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CookableObject))]
public class SpaghettiScript : MonoBehaviour
{
    //Various components of that the script keeps track of
    private InteractableBase interactableComponent;
    private Image imageComponent;
    private CookableObject cookableObject;

    /// <summary>
    /// The current state of the spaghetti
    /// </summary>
    private SpaghettiState spaghettiState = SpaghettiState.Boxed;

    /// <summary>
    /// The image representing the spaghetti when it's still in the box
    /// </summary>
    [SerializeField]
    private Sprite boxedImage = null;

    /// <summary>
    /// The image representing the spaghetti when it's out of the box but uncooked
    /// </summary>
    [SerializeField]
    private Sprite uncookedImage = null;

    /// <summary>
    /// The image representing the spaghetti when it's cooked
    /// </summary>
    [SerializeField]
    private Sprite cookedImage = null;

    /// <summary>
    /// The image representing the spaghetti when it's burned
    /// </summary>
    [SerializeField]
    private Sprite burnedImage = null;

    void Start()
    {
        //get the interactable component and add interactions to it
        interactableComponent = GetComponent<InteractableBase>();
        interactableComponent.AddInteractionToList("On Place In Container", OnPlaceInContainer);

        //get the image component and set the sprite
        imageComponent = GetComponent<Image>();

        cookableObject = GetComponent<CookableObject>();
		cookableObject.OnCookOveride = OnCookOveride;

        ChangeSpaghettiState(SpaghettiState.Boxed);
    }

    /// <summary>
    /// Updates the spaghetti state based on the state of the cookable object
    /// For future changes, this shouldn't be in update
    /// </summary>
    private void Update()
    {
        //Check if the spaghetti has been cooked
        if(cookableObject.IsCooked)
        {
            ChangeSpaghettiState(SpaghettiState.LooseCooked);
        }
        //Check if the spaghetti has been burned
        if(cookableObject.IsBurnt)
        {
            ChangeSpaghettiState(SpaghettiState.LooseBurned);
        }
    }

    /// <summary>
    /// A function to call when the spaghetti is placed in the container
    /// </summary>
    /// <param name="container">The container the spaghetti is being placed in</param>
    public void OnPlaceInContainer(InteractableBase container)
    {
        if(spaghettiState == SpaghettiState.Boxed)
        {
            ChangeSpaghettiState(SpaghettiState.LooseUncooked);
        }
    }

    /// <summary>
    /// Helper method to update the state of the spaghetti and its associated sprite
    /// </summary>
    /// <param name="newState">The new state of the spaghetti</param>
    public void ChangeSpaghettiState(SpaghettiState newState)
    {
        spaghettiState = newState;

        switch(spaghettiState)
        {
            case SpaghettiState.Boxed:
                imageComponent.sprite = boxedImage;
                break;
            case SpaghettiState.LooseUncooked:
                imageComponent.sprite = uncookedImage;
                break;
            case SpaghettiState.LooseCooked:
                imageComponent.sprite = cookedImage;
                break;
            case SpaghettiState.LooseBurned:
                imageComponent.sprite = burnedImage;
                break;
            default:
                break;
        }
    }

	public bool OnCookOveride()
	{
		ContainerComponent container = transform.parent.GetComponent<ContainerComponent>();
		//check if we are in water
		if (container == null || !container.HoldingWater)
		{
			cookableObject.IsCooked = true;
			cookableObject.IsBurnt = true;
			return true;
		}
		return false;
	}
}
