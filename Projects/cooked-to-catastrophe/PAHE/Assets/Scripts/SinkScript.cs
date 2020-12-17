using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


// Author: Nick Engell

public enum WaterStates
{
	Empty,
	Filled,
	Boiling
}

[RequireComponent(typeof(InteractableBase))]

public class SinkScript : MonoBehaviour
{
	//various components of that the script keeps track of

	private InteractableBase interactableComponent;
	//private Image imageComponent;

	/// <summary>
	/// The current state of the water
	/// </summary>
	private WaterStates waterState;

	private GameObject waterContainer;

	[SerializeField] GameObject waterInPotPrefab;
	private GameObject waterInPot;


	/// <summary>
	/// the image repsenting water
	/// </summary>
	public Sprite filled;

	/// <summary>
	/// the image representing boiling water
	/// </summary>
	public Sprite boiling;

	void Start()
	{
		//get the interactable component and add interactions to it
		interactableComponent = GetComponent<InteractableBase>();
		interactableComponent.AddInteractionToList("Fill With Water", FillWithWater);
	}

	// Author: Nick Engell
	/// <summary>
	/// Updates the water state and image based on the food cook state
	/// </summary>
	private void Update()
	{
		// If the water should be boiling
		if(waterContainer != null && waterContainer.GetComponent<CookableObject>().IsCooked)
        {
			// Update state and image
			waterState = WaterStates.Boiling;
			waterInPot.GetComponent<Image>().sprite = boiling;
        }
	}

	// Author: Nick Engell
	//takes an interactable base so it can be a delegate
	/// <summary>
	/// A function to call when the water is placed into a container
	/// </summary>
	/// <param name="container">the container the water is being placed in</param>
	public void FillWithWater(InteractableBase container)
	{
		// If the container doesn't want water in it yet
		if(waterState == WaterStates.Empty)
        {
			// Set state to filled
			waterState = WaterStates.Filled;

			// Place the water sprite in the pot
			waterInPot = Instantiate(waterInPotPrefab);
			waterInPot.transform.SetParent(container.transform);
			waterInPot.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

		// Save where the water is for later
		waterContainer = container.gameObject;

	}
}
