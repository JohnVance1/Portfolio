using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// Author: Ben Stern
/// <summary>
/// A singleton that represents the cart, is not destroyed when a new scene is loaded
/// </summary>
public class CartManager : MonoBehaviour
{

	/// <summary>
	/// A dictionary of items in the cart, uses a cart object as a key and an int as a value
	/// </summary>
	private Dictionary<CartItem, int> itemsInCart;

	//private Dictionary<string,(GameObject, int)> itemsInCart;
	/// <summary>
	/// a representation of the maximum space in the cart
	/// </summary>
	public int MaxCartSpace = 50;

	/// <summary>
	/// a representation of the used space in the cart
	/// </summary>
	private int usedCartSpace;

	/// <summary>
	/// The shoping cart list in the scene
	/// </summary>
	private Text shoppingCartList;

    /// <summary>
    /// The section for displaying the weight in the cart
    /// </summary>
    private Text weightDisplayed;

	/// <summary>
	/// the singleton instance
	/// </summary>
	private static CartManager _instance;

	/// <summary>
	/// the instance of the cart manager
	/// </summary>
	public static CartManager Instance
	{
		get
		{
			if(_instance == null)
			{
				GameObject obj = new GameObject("CartManager");
				_instance = obj.AddComponent<CartManager>();
			}
			return _instance;
		}
	}

	//Im using Awake here because it is called before start
	private void Awake()
	{
		if(_instance != null && _instance != this)
		{
			Destroy(gameObject);
			return;
		}
		_instance = this;
		DontDestroyOnLoad(gameObject);
		//itemsInCart = new Dictionary<string, (GameObject, int)>();
		itemsInCart = new Dictionary<CartItem, int>();
		//because cart manager is not destroyed when new scenes are loaded we need to reget certain elements when entering a new scene
		SceneManager.sceneLoaded += OnSceneLoaded;

		OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
		//I dont like doing this, but I want to get this done quickly so im doing it like this
		/*GameObject obj = GameObject.Find("/OverviewCanvas/Grocery List/Text");
		if (obj != null)
		{
			shoppingCartList = obj.GetComponent<Text>();
		}

        // Gets the Weight Display Text component
        GameObject weight = GameObject.Find("/OverviewCanvas/Weight Display/Text");
        if (weight != null)
        {
            weightDisplayed = weight.GetComponent<Text>();
        }*/


    }

	//Author: Ben Stern
	/// <summary>
	/// Attempts to add an item to the cart
	/// </summary>
	/// <param name="item">The Item being added to the cart</param>
	/// <returns>True if the item was added to the cart, false other wise</returns>
	public bool AddItemToCart(CartItem item)
	{
		if (usedCartSpace + item.Size > MaxCartSpace)
		{
			//Debug.Log("Not Enough Space");
			return false;
		}

		if (!itemsInCart.ContainsKey(item))
		{
			itemsInCart[item] = 0;
		}

		itemsInCart[item] += 1;
		usedCartSpace += item.Size;
		//Debug.Log(item.name + ": " + itemsInCart[item]);
		UpdateCartList();
		return true;
	}


	//Author: Ben Stern
	/// <summary>
	/// attempts ot remove an item from the cart
	/// </summary>
	/// <param name="item">the item being removed from the cart</param>
	/// <returns>true if the item was removed from the cart, false other wise</returns>
	public bool RemoveItemFromCart(CartItem item)
	{
		if (!itemsInCart.ContainsKey(item) || itemsInCart[item] <= 0)
		{
			//Debug.Log("Item Not in Cart");
			return false;
		}

		itemsInCart[item] -= 1;
		usedCartSpace -= item.Size;
		//Debug.Log(item.name + ": " + itemsInCart[item]);
		UpdateCartList();
		return true;
	}

	// Author: Ben Stern, John Vance
	/// <summary>
	/// Update the cart list when done
	/// </summary>
	public void UpdateCartList()
	{
		if(shoppingCartList != null && weightDisplayed != null)
		{
			string s = "";
            int n = 0;
            string str = "";
            foreach (KeyValuePair<CartItem, int> item in itemsInCart)
			{
				if(item.Value > 0)
				{
					s += item.Key.ItemName;
					s += "  X";
					s += item.Value;
                    s += "  ";
                    s += item.Key.Size * item.Value;
					s += '\n';

                    n += item.Key.Size * item.Value;

                }
			}
            str += "Space Used: " + n + "/" + MaxCartSpace;
            weightDisplayed.text = str;


            shoppingCartList.text = s;
		}
	}

	// Author: Ben Stern, John Vance
	/// <summary>
	/// Is  called when a new scene is loaded
	/// </summary>
	/// <param name="scene">the scene being loaded</param>
	/// <param name="mode">the way the scene is being loaded</param>
	void OnSceneLoaded(Scene scene, LoadSceneMode mode)
	{
		shoppingCartList = null;
        if (scene.name == "Pantry" || scene.name == "133-Pantry")
		{
			//I dont like doing this, but I want to get this done quickly so im doing it like this
			GameObject obj = GameObject.Find("/OverviewCanvas/Grocery List/Text");
			if (obj != null)
			{
				shoppingCartList = obj.GetComponent<Text>();
			}

            GameObject weight = GameObject.Find("/OverviewCanvas/Weight Display/Text");
            if (weight != null)
            {
                weightDisplayed = weight.GetComponent<Text>();
            }
        }
		if(scene.name == "Kitchen" || scene.name == "133-KitchenScene")
		{
			GameObject inventory = GameObject.Find("Cart Inventory");
			if(inventory != null)
			{
				KitchenCartAccesor kca = inventory.GetComponent<KitchenCartAccesor>();
				if (kca != null)
				{
					foreach (KeyValuePair<CartItem, int> item in itemsInCart)
					{
						kca.AddInventoryItemToScene(item.Key, item.Value);
					}
					inventory.transform.parent.gameObject.SetActive(false);
				}
			}
		}
	}

	private void OnDisable()
	{
		//remove from scene manager when the program is closed
		SceneManager.sceneLoaded -= OnSceneLoaded;
	}

	//Author: Ben Stern
	/// <summary>
	/// A simple getter to the number of an item in a cart
	/// </summary>
	/// <param name="item">the item we are getting</param>
	/// <returns>the number of the item in a cart</returns>
	public int GetNumberInShoppingCart(CartItem item)
	{
		if (!itemsInCart.ContainsKey(item))
		{
			return 0;
		}
		return itemsInCart[item];
	}
}
