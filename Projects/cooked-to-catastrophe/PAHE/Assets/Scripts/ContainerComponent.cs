using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Author: Ben Stern

/// <summary>
/// a component allowing an object to be a container
/// </summary>
[RequireComponent(typeof(InteractableBase))]
public class ContainerComponent : MonoBehaviour
{
	/// <summary>
	/// A vector representing where to place object put in this container relative to its position
	/// </summary>
	public Vector3 PlacePositionRelative;

	/// <summary>
	/// The containers interactable component
	/// </summary>
	private InteractableBase interactableComponent;

	//May want to make this a list in the future
	/// <summary>
	/// A refrence to the item that this container is holding
	/// </summary>
	private List<InteractableBase> itemsHolding = new List<InteractableBase>();

	public bool HoldingWater;

	private void Start()
	{
		//initialize all of the interactions and triggers
		interactableComponent = GetComponent<InteractableBase>();
		interactableComponent.AddInteractionToList("Place Item", HoldItem);

		//interactableComponent.AddInteractionToList("Take Egg", SpatulaGrab);
		interactableComponent.AddInteractionTrigger("Empty into");

		interactableComponent.AddInteractionToList("Fill water", FillWater);
	}


	//author: Ben Stern
	/// <summary>
	/// Place an item into this container
	/// </summary>
	/// <param name="itemToHold">The item to place into the container</param>
	public void HoldItem(InteractableBase itemToHold)
	{

		itemsHolding.Add(itemToHold);
		//move the item into the container
		itemToHold.transform.position = transform.position + PlacePositionRelative;
		itemToHold.transform.SetParent(transform);
		//call any interaction that happen when this item is placed in a container
		itemToHold.Interact("On Place In Container", interactableComponent);
		//objects in containers should not be selectable until taken out of the container
		itemToHold.GetComponent<SelectableObject>().enabled = false;

		//add the empty into to interaction to the list
		interactableComponent.AddInteractionToList("Empty into", EmptyInto);
	}

	//Author: Ben Stern
	/// <summary>
	/// Attempt to empty the item in this container into something else
	/// </summary>
	/// <param name="itemToEmptyInto">an interactable Item containing</param>
	public void EmptyInto(InteractableBase itemToEmptyInto)
	{
		bool completed = false;
		for (int i = 0; i < itemsHolding.Count; i++)
		{
			itemToEmptyInto.Interact("Place Item", itemsHolding[i]);

			// Allows for the spatula to move onto the frying pan and plate
			itemToEmptyInto.Interact("Take Egg", interactableComponent);

			itemToEmptyInto.Interact("Place On Plate", itemsHolding[i]);

			//double check that the item is no longer in this container befor editing properties.
			if (itemsHolding[i].transform.parent != transform)
			{
				itemsHolding.RemoveAt(i);
				i--;
				completed = true;
				continue;
			}
			completed = false;
		}

		if (completed)
		{
			//remove the empty into interaction from the list
			interactableComponent.RemoveInteractionFromList("Empty into");
		}

        if(HoldingWater)
        {
            if(itemToEmptyInto.gameObject.name == "Strainer")
            {
                itemToEmptyInto.Interact("Empty water", interactableComponent);

            }

            else
            {
                itemToEmptyInto.Interact("Fill water", interactableComponent);

                EmptyWater();
            }
        }
           

		
	}

	public void EmptyWater(InteractableBase item = null)
	{
		HoldingWater = false;
		GetComponent<Image>().color = Color.white;
		interactableComponent.RemoveInteractionFromList("Empty water");
		interactableComponent.AddInteractionToList("Fill water", FillWater);
	}

	public void FillWater(InteractableBase item)
	{
		HoldingWater = true;
		GetComponent<Image>().color = Color.blue;
		interactableComponent.RemoveInteractionFromList("Fill water");
		interactableComponent.AddInteractionToList("Empty water", EmptyWater);
	}


}
