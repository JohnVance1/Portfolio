using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField]
    GameObject target;
    [SerializeField]
    float strength;
    
    Rigidbody body;

    private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        UI = GameObject.Find("UI_Manager");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 direction = Seek.direction;
        direction.Normalize();


        // If the pause menu is active, do NOT stack forces or else them boney boys be real goofy. The force stacks while paused.
        if (!UI.GetComponent<GameManager>().GetPauseEnabled())
        {
            body.AddTorque(new Vector3(direction.z * strength, 0, -direction.x * strength));
        }
    }
}
