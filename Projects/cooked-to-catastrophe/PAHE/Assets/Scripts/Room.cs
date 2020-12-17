using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Author: Trenton Plager
/// Purpose: A class to manage the data and functions that belong to a room in the pantry
/// Restrictions: None as of right now
/// </summary>
public class Room : MonoBehaviour
{
	#region Fields
	// A bool value representing whether the room is currently locked or not
	// Eventually, this would probably be set by a manager that knows if it 
	// should be locked or not depending on the state of the recipe book
	[SerializeField]
	private bool locked;

	// A list of strings representing the subtypes of the room
	// This list will be used to populate the different ingredients
	// that can be taken from the room
	[SerializeField]
	private List<CartItem> subTypes = new List<CartItem>();

	// A prefab that represents 1 row in the submenu canvas
	// Each row represents 1 ingredient in the room
	[SerializeField]
	private GameObject ingredientListItem;

	// The parent object of all of the rooms in the main Overview canvas in the pantry
	private GameObject roomsContainer;
	#endregion

	#region Properties
	/// <summary>
	/// Returns whether the room is locked or not
	/// </summary>
	public bool Locked
	{
		get { return locked; }
		set { locked = value; }
	}
	#endregion

	#region Methods
	///Author: Trenton Plager
	/// <summary>
	/// Sets the room to be interactable and turns off the lock panel
	/// if the room is unlocked
	/// </summary>
	void Start()
	{
		if (!locked)
		{
			GetComponent<Button>().interactable = true;
			transform.Find("LockPanel").gameObject.SetActive(false);
		}

		roomsContainer = GameObject.Find("/OverviewCanvas/Rooms");
	}

	/// Author: Trenton Plager, Ben Stern
	/// <summary>
	/// Opens the submenu canvas, closes the containing object holding all the rooms in the overview canvas,
	/// and populates each of the rows in the canvas with the appropriate children and text
	/// </summary>
	public void OpenRoom()
	{
		roomsContainer.SetActive(false);
		GameObject subMenuCanvas = GameObject.Find("SubMenuCanvas");

		subMenuCanvas.GetComponent<Canvas>().enabled = true;
		subMenuCanvas.transform.Find("Title Panel/Text").GetComponent<Text>().text = gameObject.GetComponentInChildren<Text>().text;
		Transform roomContentTransform = subMenuCanvas.transform.Find("Scroll View/Viewport/Content");

		for (int i = 0; i < subTypes.Count; i++)
		{
			GameObject roomIngredient;
			CartItem cartItem = subTypes[i];
			try
			{
				roomIngredient = roomContentTransform.GetChild(i).gameObject;
				roomIngredient.SetActive(true);
			}
			catch
			{
				roomIngredient = Instantiate(ingredientListItem, roomContentTransform);
			}
			roomIngredient.name = cartItem.ItemName;
			roomIngredient.GetComponentInChildren<Text>().text = cartItem.ItemName;
			Component[] buttons = roomIngredient.GetComponentsInChildren(typeof(Button));
			for (int j = 0; j < buttons.Length; j++) {
				//Button button = (Button)buttons[j];
				string text = buttons[j].GetComponentInChildren<Text>().text;
				//we set a fresh q and bcartinstance because delegates act weird - Ben Stern
				int q = i;
				CartItem bCartItem = subTypes[q];
				buttons[j].GetComponent<Button>().onClick.RemoveAllListeners();
				if (text == "-")
				{
					buttons[j].GetComponent<Button>().onClick.AddListener(() => CartManager.Instance.RemoveItemFromCart(bCartItem));
				}
				else if (text == "+")
				{
					buttons[j].GetComponent<Button>().onClick.AddListener(() => CartManager.Instance.AddItemToCart(bCartItem));
				}
			}
		}

		if (roomContentTransform.childCount > subTypes.Count)
		{
			for (int i = roomContentTransform.childCount - subTypes.Count; i < roomContentTransform.childCount; i++)
			{
				roomContentTransform.GetChild(i).gameObject.SetActive(false);
			}
		}
	}
	#endregion
}


