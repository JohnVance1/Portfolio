using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Item_manager : MonoBehaviour
{
    public GameObject player;
    public int itemsCollected;
    public List<GameObject> interactables;
    public bool sendToGate;

    private List<GameObject> itemList = new List<GameObject>();
    private GameObject[] items;
    private bool itemNear = false;
    private bool lookingAtItem = false;
    private bool interactableNear = false;
    private bool lookingAtInteractable = false;
    private float distance;
    private GameObject closest;

    // Start is called before the first frame update
    void Start()
    {
        distance = 2.0f;
        items = GameObject.FindGameObjectsWithTag("Item");
        foreach(GameObject item in items)
        {
            itemList.Add(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        itemCheck();
        interactableCheck();
        if(itemList.Count <= 0)
        {
            SceneManager.LoadScene(4);
        }
    }

    void itemCheck()
    {
        int counter = 0;
        //if player is near an item, allow them to collect it
        foreach (GameObject item in itemList)
        {
            //player is within a certain radius of item
            if (player.transform.position.x > item.transform.position.x - distance &&
                player.transform.position.x < item.transform.position.x + distance &&
                player.transform.position.z > item.transform.position.z - distance &&
                player.transform.position.z < item.transform.position.z + distance)
            {
                itemNear = true;

                //raycasting to see if the player is looking at the object
                RaycastHit hit;
                if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
                {
                    if(hit.collider.tag == "Item")
                    {
                        lookingAtItem = true;
                    }
                }
                else
                {
                    lookingAtItem = false;
                }

                //set closest
                if (closest == null)
                {
                    closest = item;
                }
                else if (closest != null || closest != item)
                {
                    //if this item is closer
                    if (Vector3.Distance(player.transform.position, item.transform.position) < Vector3.Distance(player.transform.position, closest.transform.position))
                    {
                        closest = item;
                    }
                }
            }
            else
            {
                counter++;
            }
        }
        //if none of the items were near, don't bring up the option
        if (counter == itemList.Count)
        {
            itemNear = false;
        }

        //if the player is near an item, let it pick it up
        if (itemNear && lookingAtItem)
        {
            if (Input.GetKey(KeyCode.E))
            {
                itemList.Remove(closest);
                Destroy(closest);
                itemsCollected++;
            }
        }
    }

    void interactableCheck()
    {
        int counter = 0;
        //if player is near an interactable, allow them to collect it
        foreach (GameObject interactable in interactables)
        {
            //player is within a certain radius of interactable
            if (player.transform.position.x > interactable.transform.position.x - distance &&
                player.transform.position.x < interactable.transform.position.x + distance &&
                player.transform.position.z > interactable.transform.position.z - distance &&
                player.transform.position.z < interactable.transform.position.z + distance)
            {
                interactableNear = true;

                //raycasting to see if the player is looking at the object
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
                {
                    if (hit.collider.tag == "Interactable")
                    {
                        lookingAtInteractable = true;
                    }
                }
                else
                {
                    lookingAtInteractable = false;
                }

                //set closest
                if (closest == null)
                {
                    closest = interactable;
                }
                else if (closest != null || closest != interactable)
                {
                    //if this interactable is closer
                    if (Vector3.Distance(player.transform.position, interactable.transform.position) < Vector3.Distance(player.transform.position, closest.transform.position))
                    {
                        closest = interactable;
                    }
                }

                if (lookingAtInteractable && Input.GetKeyDown(KeyCode.F))
                {
                    if(sendToGate)
                    {
                        interactable.transform.position += new Vector3(0, 0, 3);
                        sendToGate = false;
                    }
                    else
                    {
                        interactable.transform.position -= new Vector3(0, 0, 3);
                        sendToGate = true;
                    }
                }
            }
            else
            {
                counter++;
            }
        }
        //if none of the interactables were near, don't bring up the option
        if (counter == interactables.Count)
        {
            interactableNear = false;
        }
    }

    void OnGUI()
    {
        //display current progress
        GUI.Label(new Rect(10, 5, 250, 50), "Collected: " + itemsCollected + "/" + items.Length);

        if (itemNear && lookingAtItem) //if the player is near an item, tell them they can pick it up
        {
            GUI.Label(new Rect(Screen.width / 2 - 70, Screen.height / 2 + 60, 250, 50), "Press E to pickup item");
        }

        else if (interactableNear && lookingAtInteractable) //if the player is near an interactable, tell them they can pick it up
        {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 + 60, 250, 50), "Press F to interact with environment object");
        }
    }
}
