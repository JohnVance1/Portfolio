using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Author: Nick Engell

[RequireComponent(typeof(InteractableBase))]

public class Burner : MonoBehaviour
{
	//various components of that the script keeps track of
	private InteractableBase interactableComponent;

	[SerializeField] StoveLocation burner;

	void Start()
	{
		//get the interactable component and add interactions to it
		interactableComponent = GetComponent<InteractableBase>();
		interactableComponent.AddInteractionToList("Place On Burner", PlacedOnBurner);
	}

	// Author: Nick Engell
	//takes an interactable base so it can be a delegate
	/// <summary>
	/// A function to call when the object should be placed on a burned
	/// </summary>
	/// <param name="container">the burner the object is being placed onto</param>
	public void PlacedOnBurner(InteractableBase container)
	{
		container.gameObject.GetComponent<CookingUtensil>().CurrentBurner = burner;
		container.gameObject.GetComponent<CookingUtensil>().IsOnStove = true;
		//container.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(-986, -147, 0);
		container.gameObject.GetComponent<RectTransform>().position = gameObject.transform.position;
	}

}
