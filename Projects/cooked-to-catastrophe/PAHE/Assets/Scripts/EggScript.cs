using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Author: Ben Stern


public enum EggStates
{
	Shelled,
	Raw,
	Fried,
	Burnt
}

/// <summary>
/// May I offer you an egg in these trying times
/// </summary>
[RequireComponent(typeof(InteractableBase))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CookableObject))]
public class EggScript : MonoBehaviour
{
	
	//various components of that the script keeps track of

	private InteractableBase interactableComponent;
	private Image imageComponent;

	/// <summary>
	/// The current state of the egg
	/// </summary>
	private EggStates eggState;

	/// <summary>
	/// The image representing the Egg when it is in its shell
	/// </summary>
	public Sprite shelledImage;

	/// <summary>
	/// the image repsenting the egg when it is raw and unshelled
	/// </summary>
	public Sprite rawImage;

	/// <summary>
	/// the image representing the egg when it is fried
	/// </summary>
	public Sprite fried;

	// Image representing the egg when it's burnt
	public Sprite burnt;

	void Start()
    {
		//get the interactable component and add interactions to it
		interactableComponent = GetComponent<InteractableBase>();
		interactableComponent.AddInteractionToList("On Place In Container", OnPlaceInContainer);

		//get the image component and set the sprite
		imageComponent = GetComponent<Image>();
		imageComponent.sprite = shelledImage;
    }

	// Author: Nick Engell
	/// <summary>
	/// Updates the egg state and image based on the food cook state
	/// </summary>
    private void Update()
    {
		// If the egg is fully cooked
        if(GetComponent<CookableObject>().IsCooked)
        {
			// Update it's state and image
			eggState = EggStates.Fried;
			imageComponent.sprite = fried;
        }
		// If the egg is burnt
		if(GetComponent<CookableObject>().IsBurnt)
        {
			// Update it's state and image
			eggState = EggStates.Burnt;
			imageComponent.sprite = burnt;
        }
    }

    //takes an interactable base so it can be a delegate
    /// <summary>
    /// A function to call when the egg is placed in to a container
    /// </summary>
    /// <param name="container">the container the egg is being placed in</param>
    public void OnPlaceInContainer(InteractableBase container)
	{
		//if the egg is currently shelled, unshell it
		if(eggState == EggStates.Shelled)
		{
			eggState = EggStates.Raw;
			imageComponent.sprite = rawImage;
		}
	}

}
