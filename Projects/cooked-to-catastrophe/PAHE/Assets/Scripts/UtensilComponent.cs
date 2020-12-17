using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: John Vance, Ben Stern(Most of this code is reused from the ContainerComponent Script written by Ben)

/// <summary>
/// a component allowing an object to be a container
/// </summary>
[RequireComponent(typeof(InteractableBase))]
public class UtensilComponent : MonoBehaviour
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
	/// A refrence to the container that this utensil is being used on
	/// </summary>
	private InteractableBase currentContainer;

	private void Start()
	{
		//initialize all of the interactions and triggers
		interactableComponent = GetComponent<InteractableBase>();
		interactableComponent.AddInteractionToList("Place Spatula", HoldItem);
	}


    //author: John Vance, Ben Stern
    /// <summary>
    /// Gets the Frying Pan to be used elsewhere
    /// </summary>
    /// <param name="pan">The container that the utensil is being used on</param>
    public void HoldItem(InteractableBase pan)
	{
        currentContainer = pan;

        //move the item into the container
        transform.position = currentContainer.transform.position + PlacePositionRelative;
        //transform.SetParent(itemHolding.transform);

        if (currentContainer.transform.childCount > 0)
        {
            //move item inside container to new container
            InteractableBase child = currentContainer.transform.GetChild(0).gameObject.GetComponent<InteractableBase>();
            Debug.Log("3: " + child);

            child.transform.position = transform.position;
            child.transform.SetParent(transform);

            //objects in containers should not be selectable until taken out of the container
            currentContainer.GetComponent<SelectableObject>().enabled = false;

            //add the empty into to interaction to the list
            interactableComponent.AddInteractionToList("Empty into", EmptyInto);

        }
    }

    //Author: John Vance, Ben Stern
    /// <summary>
    /// Attempt to empty the item in this container into something else
    /// </summary>
    /// <param name="itemToEmptyInto">an interactable Item containing</param>
    public void EmptyInto(InteractableBase itemToEmptyInto)
	{
        // Allows for the spatula to move onto the frying pan and plate
        itemToEmptyInto.Interact("Take Egg", interactableComponent);
        

        //double check that the item is no longer in this container befor editing properties.
        if (currentContainer.transform.parent != transform)
		{
            currentContainer = null;
			//remove the empty into interaction from the list
			interactableComponent.RemoveInteractionFromList("Empty into");
		}
	}

    
}
