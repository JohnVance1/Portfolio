using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public float playerSpeed = 1.0f;
    public float health = 10.0f;
    public bool isAttacking;
    private bool isAlive;
    private float projDir;

    private Vector3 worldPosition;
    private Vector3 mousePos;

    private Animator animator;


    private Rigidbody2D rb;

    public List<GameObject> Projectiles;
    public GameObject defaultProjectile;

    
    // Start is called before the first frame update
    void Start()
    {
        isAttacking = false;
        isAlive = true;

        animator = GetComponent<Animator>();
        // Checks to see if the Player has a RigidBody2D
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Player RidgidBody2D does not exist");

        }
        mousePos = Input.mousePosition;

    }

    // Update is called once per frame
    void Update()
    {
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);    


        // Allows for the player to attack
        if (Input.GetMouseButtonDown(0))
        {
            // Plays the attack animation
            //animator.SetTrigger("PlayerAttack");

            // Allows for the player to attack
            Projectiles.Add(defaultProjectile);

            SpawnProjectile(defaultProjectile);

        }


    }

    private void FixedUpdate()
    {
        // Allows for the Player to move 
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            float horizontalMovement = Input.GetAxisRaw("Horizontal") * playerSpeed;// * Time.deltaTime;
            float verticalMovement = Input.GetAxisRaw("Vertical") * playerSpeed;// * Time.deltaTime;

            // Switches the players direction when moving left and right
            if (horizontalMovement >= 0)
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;
                projDir = 0.2f;

            }

            else
            {
                gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;
                projDir = -0.2f;

            }

            Vector3 Move = new Vector3(horizontalMovement, verticalMovement, 0);
            transform.position += Move * Time.deltaTime * playerSpeed;


        }





    }

    /// <summary>
    /// Allows for a projectile to be spawned
    /// </summary>
    /// <param name="projectile">The spawned projectiles Prefab</param>
    void SpawnProjectile(GameObject projectile)
    {
        //Instantiate(projectile, gameObject.transform.position + worldPosition.normalized * 3, Quaternion.identity);
        Instantiate(projectile, transform.position, Quaternion.identity);

    }



    void OnCollisionEnter2D(Collision2D collision)
    {
        // Allows for the player to hurt the enemies 
        if(collision.gameObject.tag == "enemy" && isAttacking)
        {
            //isAttacking = false;


        }   


    }

    /// <summary>
    /// Allows for the health of the Player to be changed
    /// </summary>
    /// <param name="dmg"></param>
    public void ChangeHealth(float dmg)
    {
        health -= dmg;

        if (health <= 0)
        {
            health = 0;
            isAlive = false;
            enabled = false;

        }

    }


}
