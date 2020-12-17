using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Author: Ben Stern
/// <summary>
/// A class to fill up the inventory in the kitchen
/// </summary>
public class KitchenCartAccesor : MonoBehaviour
{

	/// <summary>
	/// The prefab for the cart item in the menu
	/// </summary>
	public GameObject CartMenuItem;

	/// <summary>
	/// The canvas where ingredients are spawned
	/// </summary>
	public GameObject canvas;

	/// <summary>
	/// the default position where ingredients are spawned
	/// </summary>
	public Vector3 defaultPosition;


	//this is onl here to make sure that the cart manager is initialized when someone enters the scene
	public void Awake()
	{
		bool test = CartManager.Instance != null;
	}

	//Author: Ben Stern
	/// <summary>
	/// Adds an inventory item to the menue
	/// </summary>
	/// <param name="item">the inventory item that is being added to the menu</param>
	/// <param name="number">the number of said item being added to the menu</param>
	public void AddInventoryItemToScene(CartItem item, int number)
	{
		//check to see if the number of the item is not zero
		if (number > 0)
		{
			GameObject obj = Instantiate(CartMenuItem, transform);
			obj.GetComponentsInChildren<Image>()[1].sprite = item.CartImage;
			obj.GetComponentInChildren<Text>().text = number.ToString();
			//set the on click button
			obj.GetComponent<Button>().onClick.AddListener(() => { CreateInventoryItem(item, obj); });
		}
	}

	//Author: Ben Stern
	/// <summary>
	/// Creates an inventory item in the scene
	/// </summary>
	/// <param name="item">The item being spawned</param>
	/// <param name="obj">the game object of the menu button for the item</param>
	public void CreateInventoryItem(CartItem item, GameObject obj)
	{
		//get the number of items in the menu
		int numInCart = CartManager.Instance.GetNumberInShoppingCart(item);
		//if there are zero or less of this item in the cart destroy the menu object
		if (numInCart <= 0)
		{
			Destroy(obj);
			return;
		}

		//instantiate the item in the world
		GameObject CartItem = Instantiate(item.prefab, canvas.transform);
		CartItem.transform.localPosition = defaultPosition;
		CartItem.transform.localScale = Vector3.one;
		//remove the item from the cart
		CartManager.Instance.RemoveItemFromCart(item);
		numInCart--;
		//if the number of items in the cart are zero or less than destroy the object
		if (numInCart <= 0)
		{
			Destroy(obj);
			return;
		}
		//set decrease the number of items in the menu
		obj.GetComponentInChildren<Text>().text = numInCart.ToString();
	}

}
