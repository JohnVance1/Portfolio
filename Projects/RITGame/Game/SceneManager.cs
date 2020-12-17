using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace GameName
{
    enum GameState { Overworld, Pause, MainMenu, Death, TransitionDown, TransitionUp, Win};
    class SceneManager
    {
        #region Fields and properties
        List<IDrawable> zombies;
        List<IDrawable> projectiles;
        List<IDrawable> pickups;

        private Overworld overworld;
        private static GameState gameState;
        private Camera camera;
        private SpriteBatch spriteBatch;
        private Player player;
        private MainMenu menu;
        private PauseMenu pMenu;
        private DeathMenu dMenu;
        private DeathMenu wMenu;
        private Projectile sampleProjectile;
        private Cursor cursor;
        private FinalBoss boss;
        private bool bossFight;
        public static Dictionary<string, SoundEffect> soundEffects;
        private int zombieNum;
        private Projectile samplePickup;

        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        private MouseState currentMouseState;
        private MouseState previousMouseState;

        private bool bossWall;
        private bool doorCheck;
        private bool wallCheck;
        private bool check;

        private const int BUFFER_DISTANCE = 500;

        public static GameState GameState { get { return gameState; } set { gameState = value; } }

        public int ZombieNum { get { return zombieNum; } }

        public int Count { get { return zombies.Count; } }

        public void AddPlayer(Player player)
        {
            this.player = player;
        }

        public void AddZombie(Zombie zombie)
        {
            zombies.Add(zombie);
        }

        public void AddMenu(MainMenu menu)
        {
            this.menu = menu;
        }
        public void AddPMenu(PauseMenu pauseMenu)
        {
            pMenu = pauseMenu;
        }
        public void AddDMenu(DeathMenu deathMenu)
        {
            dMenu = deathMenu;
        }
        public void AddWMenu(DeathMenu dM)
        {
            wMenu = dM;
        }

        public void AddOverworld(Overworld overworld)
        {
            this.overworld = overworld;
            /*
            foreach (Zombie z in overworld.GetZombies())
                AddZombie(z); */
        }

        public void AddCursor(Cursor cursor)
        {
            this.cursor = cursor;
        }

        public void AddProjectile(Projectile projectile)
        {
            sampleProjectile = projectile;
        }

        public void AddBoss(FinalBoss boss)
        {
            this.boss = boss;
        }

        public void AddSounds(Dictionary<string, SoundEffect> sounds)
        {
            soundEffects = sounds;
        }

        public void AddPickup(Projectile pickup)
        {
            samplePickup = pickup;
        }
        #endregion

        public SceneManager(int screenWidth, int screenHeight, SpriteBatch spriteBatch)
        {
            gameState = GameState.MainMenu;
            this.spriteBatch = spriteBatch;
            //NOTE: The int.MaxValue's here need to be replaced with the dimensions of the overworld
            camera = new Camera(screenWidth, screenHeight, int.MaxValue, int.MaxValue);
            zombies = new List<IDrawable>();
            projectiles = new List<IDrawable>();
            sampleProjectile = new Projectile();
            currentKeyboardState = Keyboard.GetState();
            previousKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            previousMouseState = Mouse.GetState();
            bossFight = false;
            zombieNum = zombies.Count();
            pickups = new List<IDrawable>();
            doorCheck = false;
            wallCheck = true;
            check = true;
        }

        /// <summary>
        /// Clears all zombies and repopulates the world
        /// </summary>
        public void ResetWorld()
        {
            zombies.Clear();

            foreach (Zombie element in overworld.NewZombieList())
            {
                AddZombie(element);
            }

            boss = boss.ResetBoss();
            bossFight = false;
            player.Zombo = false;
            player.BossFight = false;
            wallCheck = true;
            //boss.IsAlive = true;
        }

        public void Update()
        {
            currentKeyboardState = Keyboard.GetState();
            currentMouseState = Mouse.GetState();
            switch (gameState)
            {
                case GameState.MainMenu:
                    camera.IsIntroScrolling = false;
                    menu.Update(ref gameState);
                    break;

                case GameState.TransitionDown:
                    camera.IsIntroScrolling = true;
                    player.BossFight = false;
                    bossFight = false;
                    camera.MoveCamera(player.HitBox, BUFFER_DISTANCE, player.MovementSpeed * 2);
                    if (camera.IsOnCamera(player.GetWorldPos()))
                    {
                        camera.MoveCamera(player.HitBox, BUFFER_DISTANCE, player.MovementSpeed);
                        if (camera.CamY >= 7300)
                        {
                            gameState = GameState.Overworld;
                        }
                    }
                    else
                    {
                        camera.MoveCamera(player.HitBox, BUFFER_DISTANCE, player.MovementSpeed * 3);
                    }

                    // hacks
                    if(ButtonPressed(Keys.K))
                    {
                        gameState = GameState.TransitionUp;
                        player.SetWorldPos(800, 1000);
                    }
                    break;

                case GameState.TransitionUp:
                    camera.DisplaceCamera(0, 128, player.MovementSpeed / 2);
                    if (camera.CamY <= 128)
                    {
                        bossFight = true;
                        gameState = GameState.Overworld;
                    }

                    

                    break;

                case GameState.Overworld:
                    camera.IsIntroScrolling = false;
                    //player.Zombo = false;
                    //player.BossFight = false;
                    //wallCheck = true;
                    pMenu.SetPrevState(currentKeyboardState);
                    cursor.UpdateMouse();
                    player.Movement(currentKeyboardState, this, camera);

                    if (ButtonPressed(Keys.P)) { gameState = GameState.Pause; }
                    if ((LeftMouseClicked() || ButtonPressed(Keys.Space)) && player.Ammo > 0)
                    {
                        soundEffects["pew"].Play();
                        Point cursorPos = camera.ScreenToWorldPos(cursor.GetScreenPos());
                        player.DecreaseAmmo();
                        projectiles.Add(new Projectile(player.GetWorldPos(), cursorPos, sampleProjectile));
                    }

                    foreach (Projectile projectile in projectiles) projectile.Movement();

                    // If a projectile hits a wall, remove it
                    
                    foreach (Rectangle wall in Overworld.WALLS)
                    {
                        for (int i = 0; i < projectiles.Count; i++)
                        {
                            Projectile projectile = (Projectile)projectiles[i];
                            if (projectile.IsColliding(wall) || !camera.IsOnCamera(projectile.HitBox))
                            {
                                projectiles.RemoveAt(i);
                                i--;
                            }
                        }
                    }

                    if (zombies.Count > 0)
                    {
                        foreach (Rectangle element in Overworld.BossWalls)
                        {
                            for (int i = 0; i < projectiles.Count; i++)
                            {
                                Projectile projectile = (Projectile)projectiles[i];
                                if (projectile.IsColliding(element))
                                {
                                    projectiles.RemoveAt(i);
                                    i--;
                                }

                            }

                        }

                        if (check)
                        {
                            foreach (Tile element in overworld.BossList)
                            {
                                element.ChangeBossType(element.TileType, overworld.TileTextures, true);

                            }
                            check = false;
                        }

                    }

                    if(zombies.Count <= 0 && wallCheck)
                    {
                         foreach (Tile element in overworld.BossList)
                        {
                             element.ChangeBossType(element.TileType, overworld.TileTextures, false);

                        }

                        
                        //overworld.ClearBossLists();
                        player.Zombo = true;
                        wallCheck = false;

                    }

                    if(!wallCheck)
                    {
                        foreach (Tile element in overworld.BossList)
                        {
                            element.NoZomb = false;
                        }
                    }

                    if (bossFight)
                    {
                        foreach (Rectangle element in Overworld.ChangeWalls)
                        {
                            for (int i = 0; i < projectiles.Count; i++)
                            {
                                Projectile projectile = (Projectile)projectiles[i];
                                if (projectile.IsColliding(element))
                                {
                                    projectiles.RemoveAt(i);
                                    i--;
                                }

                            }

                        }

                        if (!doorCheck)
                        {
                            foreach (Tile element in overworld.BossDoor)
                            {
                                element.ChangeBossDoor(element.TileType, overworld.TileTextures, false);

                            }
                            doorCheck = true;

                        }
                        
                        if(!player.BossFight)
                        {
                            foreach (Tile element in overworld.BossDoor)
                            {
                                element.ChangeBossDoor(element.TileType, overworld.TileTextures, true);

                            }
                        }
                    }
                                        
                    // Check if zombies are damaged
                    for (int i = 0; i < zombies.Count; i++)
                    {
                        Zombie z = (Zombie)zombies[i];
                        z.Movement(player);
                        for (int j = 0; j < projectiles.Count; j++)
                        {
                            Projectile projectile = (Projectile)projectiles[j];
                            if (z.CheckRanged(projectile))
                            {
                                z.Damaged(Player.Attack);
                                projectiles.RemoveAt(j);

                                if (!z.IsAlive)
                                {
                                    zombies.RemoveAt(i);
                                    j--;
                                }
                            }
                        }
                        // Check if zombies damage the player
                        if (player.IsColliding(z))
                        {
                            player.Damaged(z.Attack);
                            player.isTouchingZombie = true;
                        }
                        else
                        {
                            player.isTouchingZombie = false;
                        }
                    }
                   

                    // Boss fight
                    // Check if the player touches the boss
                    if(bossFight)
                    {
                        player.BossFight = true;
                        if (player.IsColliding(boss))
                        {
                            player.Damaged(boss.Attack);
                        }

                        boss.IsDamaged = false;
                        for (int i = 0; i < projectiles.Count; i++)
                        {
                            Projectile projectile = (Projectile)projectiles[i];
                            if (boss.CheckRanged(projectile))
                            {
                                if (boss.IsVunerable)
                                {
                                    boss.Damaged(Player.Attack);
                                }

                                projectiles.RemoveAt(i);
                            }
                        }

                        // Boss fight zombies - sets their movement and checks of they get damaged
                        for (int i = 0; i < boss.Spawns.Count; i++)
                        {
                            Zombie z = (Zombie)boss.Spawns[i];
                            z.Movement(player);
                            for (int j = 0; j < projectiles.Count; j++)
                            {
                                Projectile projectile = (Projectile)projectiles[j];
                                if (z.CheckRanged(projectile))
                                {
                                    z.Damaged(Player.Attack);
                                    projectiles.RemoveAt(j);



                                    if (!z.IsAlive)
                                    {
                                        pickups.Add(new Projectile(z.GetWorldPos(), samplePickup, samplePickup.GetTexture()));
                                        boss.Spawns.RemoveAt(i);
                                        j--;
                                    }
                                }
                            }
                            // Check if zombie spawns damage the player
                            if (player.IsColliding(z))
                            {
                                player.Damaged(z.Attack);
                                player.isTouchingZombie = true;
                            }
                            else
                            {
                                player.isTouchingZombie = false;
                            }
                        }

                        for (int i = 0; i < pickups.Count; i++)
                        {
                            if (player.CheckRanged((Projectile)pickups[i]))
                            {
                                pickups.RemoveAt(i);
                                player.Ammo += 10;

                            }

                        }

                        // Other boss fight methods
                        boss.ChangeState();
                        boss.UpdateFight(player);
                    }

                    else
                    {
                        player.BossFight = false;
                    }




                    // Camera stuff
                    camera.MoveCamera(player.HitBox, BUFFER_DISTANCE, player.MovementSpeed);
                    if (player.HitBox.Y < 1065 && !bossFight)
                    {
                        //if (camera.IsOnCamera(new Rectangle(boss.GetWorldPos().X, boss.GetWorldPos().Y + 400, 1, 1)) && gameState != GameState.TransitionDown && !bossFight)
                        //{
                        gameState = GameState.TransitionUp;
                        //}
                    }
                    if (bossFight)
                    {
                        camera.CamX = 0;
                        camera.CamY = 128;
                    }

                    if (player.Lives <= 0)
                    {
                        player.Lives = 2;
                        gameState = GameState.Death;
                        //ResetWorld();
                        camera.CamY = 0;
                    }

                    break;
                case GameState.Pause:
                    pMenu.Update(ref gameState);
                    break;
                case GameState.Win:
                    check = true;
                    cursor.UpdateMouse();
                    ResetWorld();
                    wMenu.Update(ref gameState);
                    break;
                case GameState.Death:
                    check = true;
                    cursor.UpdateMouse();
                    ResetWorld();
                    dMenu.Update(ref gameState);
                    break;
            }
            cursor.UpdateMouse();
            previousKeyboardState = currentKeyboardState;
            previousMouseState = currentMouseState;

        }


        public void Draw()
        {
            switch(gameState)
            {
                case GameState.MainMenu:
                    menu.Draw(spriteBatch);
                    spriteBatch.Draw(cursor.GetTexture(), cursor.GetScreenPos(), Color.Lime);
                    break;

                case GameState.TransitionDown:
                case GameState.TransitionUp:
                case GameState.Overworld:
                    camera.DrawList(spriteBatch, overworld.DrawableObjects());
                    if(zombies.Count > 0)
                    {
                        camera.DrawList(spriteBatch, overworld.DrawWall());

                    }

                    if (bossFight)
                    {
                        camera.DrawList(spriteBatch, overworld.DrawDoor());

                    }

                    if (player.isTouchingZombie)
                    {
                        camera.DrawDamagedItem(spriteBatch, player);
                    }
                    else
                    {
                        camera.DrawItem(spriteBatch, player);
                    }

                    if (zombies.Count <= 0)
                    {
                        camera.DrawItem(spriteBatch, player);
                    }

                    camera.DrawProjectiles(spriteBatch, projectiles);

                    if (player.Ammo < 5000)
                    {
                        camera.DrawList(spriteBatch, pickups);
                    }
                    
                    if (zombies.Count > 0) camera.DrawList(spriteBatch, zombies);

                    

                    // draw the boss
                    if(boss.IsAlive)
                    {                        
                        if (boss.IsDamaged)
                        {
                            camera.DrawDamagedItem(spriteBatch, boss);
                        }
                        else
                        {
                            camera.DrawItem(spriteBatch, boss);
                        }
                        
                    }
                    else
                    {
                        gameState = GameState.Win;
                        player.Lives = 2;
                        ResetWorld();
                        camera.CamY = 0;
                        player.SetWorldPos(360, 8070);
                    }


                    // Draw the boss fight zombie spawns
                    for (int x = 0; x < boss.Spawns.Count; x++)
                    {
                        camera.DrawItem(spriteBatch, boss.Spawns[x]);
                    }
                    spriteBatch.Draw(cursor.GetTexture(), cursor.GetScreenPos(), Color.Lime);


                    break;

                case GameState.Pause:
                    pMenu.Draw(spriteBatch);
                    break;
                case GameState.Win:
                    wMenu.Draw(spriteBatch);
                    spriteBatch.Draw(cursor.GetTexture(), cursor.GetScreenPos(), Color.Lime);
                    break;
                case GameState.Death:
                    dMenu.Draw(spriteBatch);
                    spriteBatch.Draw(cursor.GetTexture(), cursor.GetScreenPos(), Color.Lime);
                    break;
            }
        }

        public bool LeftMouseClicked()
        {
            return (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton != ButtonState.Pressed);
        }

        public bool ButtonPressed(Keys key)
        {
            return (currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key));
        }
    }
}