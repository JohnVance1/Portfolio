using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RiceStates
{
	Bagged,
	Raw,
	Cooked,
	Burnt
}


[RequireComponent(typeof(InteractableBase))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(CookableObject))]
public class Rice : MonoBehaviour
{

	//various components of that the script keeps track of

	private InteractableBase interactableComponent;
	private Image imageComponent;

	/// <summary>
	/// The current state of the egg
	/// </summary>
	private RiceStates riceState;

	/// <summary>
	/// The image representing the rice when it is in its bag
	/// </summary>
	public Sprite baggedImage;

	/// <summary>
	/// the image repsenting the rice when it is raw and uncooked
	/// </summary>
	public Sprite rawImage;

	/// <summary>
	/// the image representing the rice when it is cooked
	/// </summary>
	public Sprite cooked;

	// Image representing the rice when it's burnt
	public Sprite burnt;

	void Start()
	{
		//get the interactable component and add interactions to it
		interactableComponent = GetComponent<InteractableBase>();
		interactableComponent.AddInteractionToList("On Place In Container", OnPlaceInContainer);

		//get the image component and set the sprite
		imageComponent = GetComponent<Image>();
		imageComponent.sprite = baggedImage;
	}

	// Author: Nick Engell
	/// <summary>
	/// Updates the rice state and image based on the food cook state
	/// </summary>
	private void Update()
	{
		//if(transform.root.GetComponent<CookableObject>().TimeToCook <= 0)
		//{
		//	Destroy(transform.root.Find("WaterInPot(Clone)"));
		//}

		// If the rice is in a container (aka not in the bag)
		if (riceState != RiceStates.Bagged && transform.parent != null)
		{
			// If there is not water in the container
			if (transform.parent.gameObject.GetComponent<ContainerComponent>().HoldingWater == false)
			{
				// If the rice is trying to be cooked with no water in it
				if (GetComponent<CookableObject>().CurrentlyBeingCooked == true)
				{
					// Set the rice to burnt
					GetComponent<CookableObject>().IsBurnt = true;
				}
			}
		}

		// If the rice is fully cooked
		if (GetComponent<CookableObject>().IsCooked)
		{
			// Update it's state and image
			riceState = RiceStates.Cooked;
			imageComponent.sprite = cooked;
		}
		// If the rice is burnt
		if (GetComponent<CookableObject>().IsBurnt)
		{
			// Update it's state and image
			riceState = RiceStates.Burnt;
			imageComponent.sprite = burnt;
		}
	}

	//takes an interactable base so it can be a delegate
	/// <summary>
	/// A function to call when the rice is placed in to a container
	/// </summary>
	/// <param name="container">the container the rice is being placed in</param>
	public void OnPlaceInContainer(InteractableBase container)
	{
		//if the rice is currently in the bag, put it into container
		if (riceState == RiceStates.Bagged)
		{
			riceState = RiceStates.Raw;
			imageComponent.sprite = rawImage;
		}
	}

}
