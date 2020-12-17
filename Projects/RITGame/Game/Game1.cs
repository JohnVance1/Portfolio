using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace GameName
{
    /// <summary>
    /// David Patch is shooting for a job at EA.
    /// Anthony was here
    /// ???????????
    /// Why's it all red? -Leah
    /// John Vance was here.
    /// </summary>
    public class Game1 : Game //our weird project name thing made me do this
    {
        #region Fields
        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;
        SceneManager sceneManager;
        Player player;
        Texture2D playerTexture;        
        Texture2D zombieTexture1;
        Texture2D zombieTexture2;
        Texture2D zombiePink;
        Texture2D zombieOrange;
        Texture2D zombieDark;
        Texture2D zombieChris;
        List<Texture2D> zombieTextures;
        Projectile basicProjectile;
        Texture2D projectileTexture;
        Dictionary<string, Texture2D> overworldTileTextures;
        Overworld overworld;
        private SpriteFont font;
        Cursor cursor;
        Texture2D cursorTexture;
        List<Texture2D> bossTextures;
        FinalBoss boss;
        Dictionary<string, SoundEffect> soundEffects = new Dictionary<string, SoundEffect>();
        Texture2D pickupTexture;
        Projectile basicPickup;


        #endregion

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {

            //enables full screen mode
            graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            //graphics.ToggleFullScreen();
            graphics.ApplyChanges();

            spriteBatch = new SpriteBatch(GraphicsDevice);

            //set up scenemanager
            sceneManager = new SceneManager(GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, spriteBatch);

            // Sets up static variables read from the file.
            FileReader reader = new FileReader();

            base.Initialize();
        }

        /// <summary>
        /// Loads all the tile textures for the overworld.
        /// </summary>
        public void OverworldTextureLoading()
        {
            overworldTileTextures = new Dictionary<string, Texture2D>();
            overworldTileTextures.Add("Brick Wall 2D", Content.Load<Texture2D>("Brick_Wall_2D"));
            overworldTileTextures.Add("Brick Wall 3D", Content.Load<Texture2D>("Brick_Wall_3D"));
            overworldTileTextures.Add("Empty Space", Content.Load<Texture2D>("Empty_Space"));
            overworldTileTextures.Add("Grass 2D", Content.Load<Texture2D>("Grass_2D"));
            overworldTileTextures.Add("Grass 3D", Content.Load<Texture2D>("Grass_3D"));
            overworldTileTextures.Add("Path 2D", Content.Load<Texture2D>("Path_2D"));
            overworldTileTextures.Add("Path 3D", Content.Load<Texture2D>("Path_3D"));
            overworldTileTextures.Add("Water", Content.Load<Texture2D>("Water"));
        }

        public void BossTextureLoading()
        {
            bossTextures.Add(Content.Load<Texture2D>("boss"));
            bossTextures.Add(Content.Load<Texture2D>("boss2"));
            bossTextures.Add(Content.Load<Texture2D>("boss3"));
            bossTextures.Add(Content.Load<Texture2D>("boss4"));
            bossTextures.Add(Content.Load<Texture2D>("boss5"));
            bossTextures.Add(Content.Load<Texture2D>("bossfinal"));
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Makes a new player
            playerTexture = Content.Load<Texture2D>("brickLink");
            player = new Player(playerTexture);
            player.SetWorldPos(360, 8070);

            zombieTextures = new List<Texture2D>();

            zombieTexture1 = Content.Load<Texture2D>("chrisZombieT");
            zombieTextures.Add(Content.Load<Texture2D>("pinkZombie"));
            zombieTextures.Add(Content.Load<Texture2D>("orangeZombie"));
            zombieTextures.Add(Content.Load<Texture2D>("darkZombie"));
            zombieTextures.Add(Content.Load<Texture2D>("zombieBigHead"));
            zombieTextures.Add(Content.Load<Texture2D>("blueZombie"));
            zombieTextures.Add(Content.Load<Texture2D>("chrisZombie"));
            zombieTextures.Add(Content.Load<Texture2D>("chrisZombieT"));

            // Create the Final Boss
            bossTextures = new List<Texture2D>();
            BossTextureLoading();
            boss = new FinalBoss(bossTextures, zombieTexture1, zombieTexture2);

            //make a projectile
            projectileTexture = Content.Load<Texture2D>("bullet");
            basicProjectile = new Projectile(projectileTexture);

            // Makes a custom mouse cursor
            cursorTexture = Content.Load<Texture2D>("mouseCursor");
            cursor = new Cursor(cursorTexture);

            // Make pickup
            pickupTexture = Content.Load<Texture2D>("ammo");
            basicPickup = new Projectile(pickupTexture);

            //make menus
            MainMenu menu = new MainMenu(Content.Load<Texture2D>("start"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, Content.Load<Texture2D>("logo maybe"));
            PauseMenu pMenu = new PauseMenu(Content.Load<Texture2D>("pause"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            DeathMenu dMenu = new DeathMenu(Content.Load<Texture2D>("lose"), Content.Load<Texture2D>("playAgain"), Content.Load<Texture2D>("exit"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            DeathMenu wMenu = new DeathMenu(Content.Load<Texture2D>("win"), Content.Load<Texture2D>("playAgain"), Content.Load<Texture2D>("exit"), GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);


            // Create a new SpriteBatch, which can be used to draw textures.
            // This is just an example to show that we can 
            // draw from the character class. The Link texture is 
            // just an example that we will change later on
            
            zombieTexture2 = Content.Load<Texture2D>("tank");
            OverworldTextureLoading();
            overworld = new Overworld(overworldTileTextures, zombieTexture1, zombieTexture2, player, zombieTextures);
            sceneManager.AddOverworld(overworld);

            //add to the scene manager
            sceneManager.AddPlayer(player);
            sceneManager.AddMenu(menu);
            sceneManager.AddPMenu(pMenu);
            sceneManager.AddDMenu(dMenu);
            sceneManager.AddWMenu(wMenu);
            sceneManager.AddCursor(cursor);
            sceneManager.AddProjectile(basicProjectile);
            sceneManager.AddBoss(boss);
            sceneManager.AddPickup(basicPickup);

            foreach(Zombie element in overworld.GetZombies())
            {
                sceneManager.AddZombie(element);

            }

            font = Content.Load<SpriteFont>("font");

            LoadSounds();
            Song song = Content.Load<Song>("song");
            MediaPlayer.Play(song);
            sceneManager.AddSounds(soundEffects);
        }

        private void LoadSounds()
        {
            soundEffects.Add("pew", Content.Load<SoundEffect>("pew"));
            soundEffects.Add("death", Content.Load<SoundEffect>("WilhelmScream"));
            soundEffects.Add("pop", Content.Load<SoundEffect>("pop"));
            soundEffects.Add("oof", Content.Load<SoundEffect>("oof"));
            soundEffects.Add("slow oof", Content.Load<SoundEffect>("slow_oof"));
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
            // TODO: Add your update logic here
            sceneManager.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp);

            // Can use samplerstate.Clamp for better textures

            //manager.DrawSomthing(player)
            // Allows to draw from the Character class
            sceneManager.Draw();

            if(SceneManager.GameState == GameState.Overworld)
            {
                spriteBatch.DrawString(font, "Health: " + ((int)((double)player.Health /(double)FileReader.PlayerHealth * 100)).ToString() + "%", new Vector2(GraphicsDevice.Viewport.Width / 2 - 55, 20), Color.White);
                spriteBatch.DrawString(font, "Lives: " + player.Lives.ToString(), new Vector2(20, 20), Color.White);
                spriteBatch.DrawString(font, "Enemies Left: " + sceneManager.Count, new Vector2(GraphicsDevice.Viewport.Width - 200, 20), Color.White);
                spriteBatch.DrawString(font, "Ammo Left : " + player.Ammo.ToString(), new Vector2(GraphicsDevice.Viewport.Width / 3 - 20, 20), Color.White);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
