using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 MouseLoc;    // Player's mouse location
    public Vector2 SpawnLoc;    // Player's location
    private GameObject proj;    // Projectile Instance
    private Rigidbody2D projRB; // Projectile's RidgidBody2D

    private float distance;     // The distance from the player to the mouse

    [SerializeField]
    private float projSpeed;    // The projectile's speed

    private void Awake()
    {
        Destroy(gameObject, 2);

    }


    // Start is called before the first frame update
    void Start()
    {
        proj = this.gameObject;

        projSpeed = 500;
        distance = 0;
        

        // Allows for the Projetile to move toward the Mouse
        MouseLoc = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SpawnLoc = gameObject.transform.position;

        projRB = GetComponent<Rigidbody2D>();

        float horizontal = MouseLoc.x - SpawnLoc.x;
        float vertical = MouseLoc.y - SpawnLoc.y;

        distance = Mathf.Sqrt(Mathf.Pow((horizontal), 2) + Mathf.Pow((vertical), 2));        

        Vector2 direction = new Vector2(horizontal, vertical);

        direction.Normalize();

        // Rotates the projetile
        Debug.Log(Mathf.Atan(vertical / horizontal) * Mathf.Rad2Deg);

        if(horizontal < 0)
        {
            projRB.rotation = Mathf.Atan(vertical / horizontal) * Mathf.Rad2Deg + 180;

        }
        else
        {
            projRB.rotation = Mathf.Atan(vertical / horizontal) * Mathf.Rad2Deg;

        }

        // Moves the projectile
        projRB.AddForce(direction * projSpeed);


    }

    // Update is called once per frame
    void FixedUpdate()
    {
       

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "enemy" || collision.gameObject.tag == "platform")
        {
            Destroy(gameObject);

        }

    }


   


}
