using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;		//Allows us to use Lists. 


namespace Completed
{
	//Enemy inherits from MovingObject, our base class for objects that can move, Player also inherits from this.
	public class Enemy : MovingObject
	{
		public int playerDamage; 							//The amount of food points to subtract from the player when attacking.
		public AudioClip attackSound1;						//First of two audio clips to play when attacking the player.
		public AudioClip attackSound2;						//Second of two audio clips to play when attacking the player.
		
		
		private Animator animator;							//Variable of type Animator to store a reference to the enemy's Animator component.
		private Transform target;							//Transform to attempt to move toward each turn.
		private bool skipMove;                              //Boolean to determine whether or not enemy should skip a turn or move this turn.

        private int health;

        private bool canMove;
        private int numMoves;

        private List<Enemy> enemyList;

        private int nextX;
        private int nextY;
        private Vector2 nextMove;




        [FMODUnity.EventRef]
        public string EnemyDamaged;
        private EventInstance enemyDamaged;

        [FMODUnity.EventRef]
        public string EnemyDeath;
        private EventInstance enemyDeath;

        [FMODUnity.EventRef]
        public string EnemyMove;
        private EventInstance enemyMove;

        [FMODUnity.EventRef]
        public string Proximity;
        private EventInstance proximity;
        private float proxParam;
        private bool proxCheck1;
        private bool proxCheck2;



        //Start overrides the virtual Start function of the base class.
        protected override void Start ()
		{
            // Health of the enemy
            health = 2;
            canMove = false;
            numMoves = 0;
            skipMove = true;
            //Register this enemy with our instance of GameManager by adding it to a list of Enemy objects. 
            //This allows the GameManager to issue movement commands.
            GameManager.instance.AddEnemyToList (this);

            nextX = 0;
            nextY = 0;

            enemyDamaged = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/EnemyDamaged");

            enemyDeath = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/EnemyDeath");

            enemyMove = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/ZombieMove");

            proximity = FMODUnity.RuntimeManager.CreateInstance("event:/Enemy/Proximity");

            enemyList = GameManager.instance.enemies;

            proxCheck1 = true;
            proxCheck2 = true;


            //Get and store a reference to the attached Animator component.
            animator = GetComponent<Animator> ();
			
			//Find the Player GameObject using it's tag and store a reference to its transform component.
			target = GameObject.FindGameObjectWithTag ("Player").transform;



            //Call the start function of our base class MovingObject.
            base.Start ();
		}

        private void Update()
        {
            if (nextX != 0 || nextY != 0)
            {
                //AttemptMove<Wall>(nextX, nextY);

            }

        }


        //Override the AttemptMove function of MovingObject to include functionality needed for Enemy to skip turns.
        //See comments in MovingObject for more on how base AttemptMove function works.
        protected override void AttemptMove <T> (int xDir, int yDir)
		{

            //Check if skipMove is true, if so set it to false and skip this turn.
            //if (skipMove)
			//{
				//skipMove = false;
                //canMove = true;
                //numMoves = 0;
                //return;

            //}

            nextX = xDir;
            nextY = yDir;


            //canMove = GetMove();

            //Call the AttemptMove function from MovingObject.
            base.AttemptMove<T>(xDir, yDir);


            if (canMove)
            {
                enemyMove.start();
                CheckEnemies();
                //skipMove = true;

            }

            numMoves++;

            if (numMoves > 2)
            {
                //skipMove = true;
                canMove = true;
                numMoves = 0;
            }

            else
            {

            }

            //skipMove = true;

            //Now that Enemy has moved, set skipMove to true to skip next move.
        }


        //MoveEnemy is called by the GameManger each turn to tell each Enemy to try to move towards the player.
        public void MoveEnemy ()
		{

            //Declare variables for X and Y axis move directions, these range from -1 to 1.
            //These values allow us to choose between the cardinal directions: up, down, left and right.
            int xDir = 0;
			int yDir = 0;



            


            //If the difference in positions is approximately zero (Epsilon) do the following:
            if (Mathf.Abs (target.position.x - transform.position.x) < float.Epsilon)
				
				//If the y coordinate of the target's (player) position is greater than the y coordinate of this enemy's position set y direction 1 (to move up). If not, set it to -1 (to move down).
				yDir = target.position.y > transform.position.y ? 1 : -1;
			
			//If the difference in positions is not approximately zero (Epsilon) do the following:
			else
				//Check if target x position is greater than enemy's x position, if so set x direction to 1 (move right), if not set to -1 (move left).
				xDir = target.position.x > transform.position.x ? 1 : -1;

            nextMove.x = xDir + transform.position.x;
            nextMove.y = yDir + transform.position.y;

            if(canMove)
            {
            }

            AttemptMove<Wall>(xDir, yDir);
            AttemptMove<Player>(xDir, yDir);
            AttemptMove<Wall>(xDir, yDir);


            //Call the AttemptMove function and pass in the generic parameter Player, because Enemy is moving and expecting to potentially encounter a Player

        }

        private void CheckEnemies()
        {
            if (target != null && transform != null)
            {
                proximity.getParameterByName("Prox", out proxParam);
                proxParam = 0;


                if (((Mathf.Abs(target.position.x - nextMove.x) <= 1) &&
                       (Mathf.Abs(target.position.y - nextMove.y) <= 1)))
                {
                    //proxCheck2 = false;
                    proxParam = 2.0f;
                    proximity.setParameterByName("Prox", proxParam);
                    proximity.start();
                }

                else if (((Mathf.Abs(target.position.x - nextMove.x) <= 2) &&
                        (Mathf.Abs(target.position.y - nextMove.y) <= 2)))
                {
                    //proxCheck1 = false;
                    proxParam = 1.0f;
                    proximity.setParameterByName("Prox", proxParam);
                    proximity.start();
                }

                else if (((Mathf.Abs(target.position.x - nextMove.x) > 1) &&
                    (Mathf.Abs(target.position.y - nextMove.y) > 1)))
                {
                    //proxCheck1 = true;
                    proxParam = 0;

                }

                else if (((Mathf.Abs(target.position.x - nextMove.x) > 0) &&
                   (Mathf.Abs(target.position.y - nextMove.y) > 0)))
                {
                    //proxCheck2 = true;
                    proxParam = 1.0f;
                }
                    

                //proximity.setParameterByName("Prox", proxParam);
                //proximity.start();

            }


        }

        public void DamageEnemy(int damage)
        {
            health -= damage;

            if(health <= 0)
            {
                enemyDeath.start();
                GameManager.instance.RemoveEnemyFromList(this);
                Destroy(gameObject);
            }
            else
            {
                enemyDamaged.start();
            }

        }


        //OnCantMove is called if Enemy attempts to move into a space occupied by a Player, it overrides the OnCantMove function of MovingObject 
        //and takes a generic parameter T which we use to pass in the component we expect to encounter, in this case Player
        protected override void OnCantMove <T> (T component)
		{
            //if (component is Wall)
            //{
            //    //Set hitWall to equal the component passed in as a parameter.
            //    Wall hitWall = component as Wall;

            //}

            //else if (component is Player)
            //{
            //Declare hitPlayer and set it to equal the encountered component.
            Player hitPlayer = component as Player;

            if(hitPlayer != null)
            {
                //Call the LoseFood function of hitPlayer passing it playerDamage, the amount of foodpoints to be subtracted.
                hitPlayer.LoseFood(playerDamage);

                //Set the attack trigger of animator to trigger Enemy attack animation.
                animator.SetTrigger("enemyAttack");

            }
            

            //}
            Debug.Log("Hello");

            //canMove = cantMove;
            canMove = false;


            //Call the RandomizeSfx function of SoundManager passing in the two audio clips to choose randomly between.
            //SoundManager.instance.RandomizeSfx (attackSound1, attackSound2);
        }


    }
}
