using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Author: Ben Stern
/// <summary>
/// A representation of any item that can be held in a cart
/// </summary>
[CreateAssetMenu(fileName = "CartItem", menuName = "ScriptableObjects/CartItem", order = 1)]
public class CartItem : ScriptableObject
{
	public int Size;
	public Sprite CartImage;
	public string ItemName;
	public GameObject prefab;
}
