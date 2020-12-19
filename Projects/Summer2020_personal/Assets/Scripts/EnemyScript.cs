using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float enemySpeed = 1.0f;

    [SerializeField]
    private float enemyHealth;
    private bool isAlive;

    private Rigidbody2D enemyRB;

    public GameObject player;
    public Transform track;
    private Transform savedTransform;

    private Vector3 playerLoc;
    private Vector3 enemyLoc;

    private float distance;

    private bool isColliding;

    // Properties
    #region Properties
    public float EnemyHealth
    {
        get { return enemyHealth; }
        set { enemyHealth = value; }
    }
    
    public bool IsColliding
    {
        get { return isColliding; }

    }

    public bool IsAlive
    {
        get { return isAlive; }
        set { isAlive = value; }

    }

    void Awake()
    {
        isAlive = true;

    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Checks to see if the Enemy has a RigidBody2D
        player = GameObject.FindGameObjectWithTag("player");

       

        isColliding = false;
        enemyHealth = 1.0f;

        distance = 0;
        enemyRB = gameObject.GetComponent<Rigidbody2D>();
        if (enemyRB == null)
        {
            Debug.LogError("Enemy RidgidBody2D does not exist");

        }

    }

    // Update is called once per frame
    void Update()
    {
        //if(enemyHealth <= 0)
        //{
        //    Destroy(gameObject);

        //}
        
    }


    private void FixedUpdate()
    {
        // Allows for the Enemy to move toward the Player
        playerLoc = player.transform.position;
        enemyLoc = gameObject.transform.position;

        // Distance formula - sqrt((x2 - x1)^2 + (y2 - y1)^2)
        //distance = Mathf.Sqrt(Mathf.Pow((PlayerLoc.x - EnemyLoc.x), 2) + Mathf.Pow((PlayerLoc.y - EnemyLoc.y), 2));

        float horizontal = playerLoc.x - enemyLoc.x;
        float vertical = playerLoc.y - enemyLoc.y;

        // Switches the enemies direction 
        if(horizontal >= 0)
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = true;

        }

        else
        {
            gameObject.GetComponentInChildren<SpriteRenderer>().flipX = false;

        }

        Vector2 norm = new Vector2(horizontal, vertical);

        norm.Normalize();

        enemyRB.AddForce(Vector2.right * norm.x * enemySpeed);
        enemyRB.AddForce(Vector2.up * norm.y * enemySpeed);


    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "projectile")
        {
            //isColliding = true;
            this.EnemyHealth -= 1;
            if(this.EnemyHealth <= 0)
            {
                this.IsAlive = false;
            }

        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            //gameObject.GetComponent<BoxCollider2D>().enabled = false;
            player.GetComponent<PlayerCollision>().ChangeHealth(0.1f);

        }
    }



}
