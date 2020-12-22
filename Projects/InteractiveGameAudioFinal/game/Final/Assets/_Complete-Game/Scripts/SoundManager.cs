using UnityEngine;
using System.Collections;
using FMOD.Studio;
using FMODUnity;

namespace Completed
{
	public class SoundManager : MonoBehaviour 
	{
		//public AudioSource efxSource;					//Drag a reference to the audio source which will play the sound effects.
		//public AudioSource musicSource;					//Drag a reference to the audio source which will play the music.
		public static SoundManager instance = null;     //Allows other scripts to call functions from SoundManager.				
                                                        //public float lowPitchRange = .95f;				//The lowest a sound effect will be randomly pitched.
                                                        //public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.

        ////*************************// MAIN MENU //*************************//
        //[FMODUnity.EventRef]
        //public string MenuClick;
        //public EventInstance menuClick;

        //[FMODUnity.EventRef]
        //public string MainMusic;
        //public EventInstance mainMusic;

        ////*************************// ENEMY //*************************//
        //[FMODUnity.EventRef]
        //public string EnemyDamaged;
        //public EventInstance enemyDamaged;

        //[FMODUnity.EventRef]
        //public string EnemyDeath;
        //public EventInstance enemyDeath;

        //[FMODUnity.EventRef]
        //public string EnemyMove;
        //public EventInstance enemyMove;

        //[FMODUnity.EventRef]
        //public string Proximity;
        //public EventInstance proximity;


        ////*************************// PLAYER //*************************//
        //[FMODUnity.EventRef]
        //public string PlayerBreak;
        //public EventInstance playerBreak;

        //[FMODUnity.EventRef]
        //public string PlayerDamaged;
        //public EventInstance playerDamaged;

        //[FMODUnity.EventRef]
        //public string PickUp;
        //public EventInstance pickUp;

        //[FMODUnity.EventRef]
        //public string HealthLow;
        //public EventInstance healthLow;
        //public float healthParam;

        //[FMODUnity.EventRef]
        //public string HitWall;
        //public EventInstance hitWall;

        //[FMODUnity.EventRef]
        //public string PlayerDeath;
        //public EventInstance playerDeath;

        //[FMODUnity.EventRef]
        //public string PlayerHitEnemy;
        //public EventInstance playerHitEnemy;

        //[FMODUnity.EventRef]
        //public string FoodCrunch;
        //public EventInstance foodCrunch;

        //[FMODUnity.EventRef]
        //public string SodaDrink;
        //public EventInstance sodaDrink;

        //[FMODUnity.EventRef]
        //public string GameOver;
        //public EventInstance gameOver;


        ////*************************// MOVING OBJECT //*************************//
        //[FMODUnity.EventRef]
        //public string PlayerMove;
        //public EventInstance playerMove;


        ////*************************// GAME MANAGER //*************************//
        //[FMODUnity.EventRef]
        //public string LevelSwitch;
        //public EventInstance levelSwitch;

        //[FMODUnity.EventRef]
        //public string AmbiantMusic;
        //public EventInstance ambiantMusic;



        void Awake ()
		{
			//Check if there is already an instance of SoundManager
			if (instance == null)
				//if not, set it to this.
				instance = this;
			//If instance already exists:
			else if (instance != this)
				//Destroy this, this enforces our singleton pattern so there can only be one instance of SoundManager.
				Destroy (gameObject);
			
			//Set SoundManager to DontDestroyOnLoad so that it won't be destroyed when reloading our scene.
			DontDestroyOnLoad (gameObject);
		}

        private void Start()
        {
            //*****// MAIN MENU //*****//
            //menuClick = FMODUnity.RuntimeManager.CreateInstance("event:/Misc/MenuInterface");
            //mainMusic = FMODUnity.RuntimeManager.CreateInstance("event:/Music/MainMenu");


            //*****// PLAYER //*****//
            //playerDamaged = FMODUnity.RuntimeManager.CreateInstance("event:/Player/PlayerDamaged");
            //playerBreak = FMODUnity.RuntimeManager.CreateInstance("event:/Player/PlayerBreak");
            //pickUp = FMODUnity.RuntimeManager.CreateInstance("event:/Player/PickupFood");
            //healthLow = FMODUnity.RuntimeManager.CreateInstance("event:/Player/HealthLow");
            //hitWall = FMODUnity.RuntimeManager.CreateInstance("event:/Player/HitWall");
            //playerDeath = FMODUnity.RuntimeManager.CreateInstance("event:/Player/PlayerDeath");
            //playerHitEnemy = FMODUnity.RuntimeManager.CreateInstance("event:/Player/PlayerHitEnemy");
            //foodCrunch = FMODUnity.RuntimeManager.CreateInstance("event:/Player/FoodCrunch");
            //sodaDrink = FMODUnity.RuntimeManager.CreateInstance("event:/Player/SodaDrink");
            //gameOver = FMODUnity.RuntimeManager.CreateInstance("event:/Music/GameOver");

            
            //*****// ENEMY //*****//



            //*****// MOVING OBJECT //*****//



            //*****// GAME MANAGER //*****//



        }


    }
}
