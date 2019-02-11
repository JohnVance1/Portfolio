using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameName
{
    class Player : Character
    {
        //Removed some fields and altered one
        private int lives;
        public static int Attack;
        public bool isTouchingZombie;
        private int ammo;
        private bool bossFight;
        private bool zombo;

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Ammo
        {
            get { return ammo; }
            set { ammo = value; }
        }

        public bool BossFight
        {
            get { return bossFight; }
            set { bossFight = value; }
        }

        public bool Zombo
        {
            get { return zombo; }
            set { zombo = value; }
        }

        public Player(Texture2D texture)
            : base(texture)
        {
            Health = FileReader.PlayerHealth;
            Lives = 2;
            Attack = FileReader.PlayerAttack;
            ammo = FileReader.Ammo;
            isTouchingZombie = false;
            bossFight = false;
            zombo = false;
        }

        public void DecreaseAmmo()
        {
            if (ammo <= 0)
            {
                ammo = 0;
            }
            else
            {
                ammo--;

            }
        }

        public void Damaged(int attack)
        {
            Health -= Math.Abs(attack);
            //Console.WriteLine(Health);
            if (Health <= 0)
            {
                Lives--;
                SceneManager.GameState = GameState.TransitionDown;
                Respawn();
            }
        }

        /// <summary>
        /// Damages the player if a zombie touches its hitbox
        /// </summary>
        /// <param name="enemy">The enemy that the player is being damaged by</param>
        public void CheckMelee(Zombie enemy)
        {
            if (IsColliding(enemy))
            {
                Damaged(enemy.Attack);
            }

        }

        public bool Pickup(Projectile pickup)
        {
            if (CheckRanged(pickup))
            {
                return true;

            }
            return false;
        }

        /// <summary>
        /// Basic movement system
        /// We can/should change as the game progresses
        /// </summary>
        public void Movement(KeyboardState kb, SceneManager sceneManager, Camera camera)
        {
            if(kb.IsKeyDown(Keys.W))
            {
                Rectangle newHitBox = new Rectangle(hitBox.X, hitBox.Y - movementSpeed, hitBox.Width, hitBox.Height);
                bool intersected = false;
                for(int i =0; i < Overworld.WALLS.Count; i++)
                {
                    if (newHitBox.Intersects(Overworld.WALLS[i])) { intersected = true; }
                }
                if (!zombo)
                {
                    for (int i = 0; i < Overworld.BossWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.BossWalls[i])) { intersected = true; }
                    }
                }
                if (bossFight)
                {
                    for (int i = 0; i < Overworld.ChangeWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.ChangeWalls[i])) { intersected = true; }
                    }
                }
                if (newHitBox.Y < camera.CamY) intersected = true;
                if (!intersected)
                {
                    SetWorldPos(newHitBox.X, newHitBox.Y);
                }
            }

            if (kb.IsKeyDown(Keys.A))
            {
                Rectangle newHitBox = new Rectangle(hitBox.X - movementSpeed, hitBox.Y, hitBox.Width, hitBox.Height);
                bool intersected = false;
                for (int i = 0; i < Overworld.WALLS.Count; i++)
                {
                    if (newHitBox.Intersects(Overworld.WALLS[i])) { intersected = true; }
                }
                if (!zombo)
                {
                    for (int i = 0; i < Overworld.BossWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.BossWalls[i])) { intersected = true; }
                    }
                }
                if (bossFight)
                {
                    for (int i = 0; i < Overworld.ChangeWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.ChangeWalls[i])) { intersected = true; }
                    }
                }
                if (!intersected)
                {
                    SetWorldPos(newHitBox.X, newHitBox.Y);
                }
            }

            if (kb.IsKeyDown(Keys.S))
            {
                Rectangle newHitBox = new Rectangle(hitBox.X , hitBox.Y + movementSpeed, hitBox.Width, hitBox.Height);
                bool intersected = false;
                for (int i = 0; i < Overworld.WALLS.Count; i++)
                {
                    if (newHitBox.Intersects(Overworld.WALLS[i])) { intersected = true; }
                }
                if (!zombo)
                {
                    for (int i = 0; i < Overworld.BossWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.BossWalls[i])) { intersected = true; }
                    }
                }
                if (bossFight)
                {
                    for (int i = 0; i < Overworld.ChangeWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.ChangeWalls[i])) { intersected = true; }
                    }
                }
                if (newHitBox.Y > camera.CamY + camera.ScreenHeight - hitBox.Height) intersected = true;
                if (!intersected)
                {
                    SetWorldPos(newHitBox.X, newHitBox.Y);
                }
            }

            if (kb.IsKeyDown(Keys.D))
            {
                Rectangle newHitBox = new Rectangle(hitBox.X + movementSpeed, hitBox.Y, hitBox.Width, hitBox.Height);
                bool intersected = false;
                for (int i = 0; i < Overworld.WALLS.Count; i++)
                {
                    if (newHitBox.Intersects(Overworld.WALLS[i])) { intersected = true; }
                }
                if (!zombo)
                {
                    for (int i = 0; i < Overworld.BossWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.BossWalls[i])) { intersected = true; }
                    }
                }
                if (bossFight)
                {
                    for (int i = 0; i < Overworld.ChangeWalls.Count; i++)
                    {
                        if (newHitBox.Intersects(Overworld.ChangeWalls[i])) { intersected = true; }
                    }
                }
                if (!intersected)
                {
                    SetWorldPos(newHitBox.X, newHitBox.Y);
                }
            }
        }
        
       
        public void Respawn()
        {
            Health = FileReader.PlayerHealth;
            SceneManager.soundEffects["death"].Play();
            bossFight = false;
            SetWorldPos(360, 8070);
        }
    }
}
