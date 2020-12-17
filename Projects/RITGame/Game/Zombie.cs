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
    class Zombie : Character
    {

        private int attack;
        private bool isAlive;

        private Vector2 move;

        private int locatorDistance;
        private double percentSpeed;
        private int scale;

        public int Attack
        {
            get { return attack; }

        }

        public bool IsAlive
        {
            get { return isAlive; }
            set { isAlive = value; }

        }

        public Zombie(Texture2D texture, int LD, int scale)
            : base(texture)
        {
            Health = FileReader.ZombieHealth;


            attack = FileReader.ZombieAttack;
            isAlive = true;
            this.scale = scale;

            hitBox.X = 350;
            hitBox.Y = 350;
            hitBox.Width *= scale;
            hitBox.Height *= scale;

            move = new Vector2(0, 0);
            locatorDistance = LD;
            this.percentSpeed = 1 - (scale * .15);
        }

        /// <summary>
        /// Changes Zombie health based on the attack level of the projectile
        /// </summary>
        /// <param name="attack"></param>
        public void Damaged(int attack)
        {
            Health -= Math.Abs(attack);

            if (Health <= 0)
            {
                Health = 0;
                isAlive = false;
                if (scale == 1) SceneManager.soundEffects["oof"].Play();
                else SceneManager.soundEffects["slow oof"].Play();
            }

        }

        // Implement possible pathfinding for the zombie/way for 
        // the Zombie to move towards the player
        public void PlayerLocator(Player p)
        {
            move.X = 0;
            move.Y = 0;
            //if (hitBox.Intersects(p.HitBox)) { return; }

            double x = p.GetWorldPos().X - hitBox.X;
            double y = p.GetWorldPos().Y - hitBox.Y;
            double hypotenuse = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

            if(hypotenuse < 1) { return; }

            if(hypotenuse <= locatorDistance)
            {
                float movementX = (float)(2 * x / hypotenuse);
                float movementY = (float)(2 * y / hypotenuse);
                move.X = (int)(movementX * (movementSpeed * percentSpeed));
                move.Y = (int)(movementY * (movementSpeed * percentSpeed));
            }
        }

        public void Movement(Player p)
        {
            PlayerLocator(p);
            bool isIntersecting = false;
            Rectangle newHitBox = hitBox;
            newHitBox.X = hitBox.X + (int)move.X;
            foreach (Rectangle t in Overworld.WALLS)
            {
                if (newHitBox.Intersects(t))
                {
                    isIntersecting = true;
                    break;
                }
            }
            foreach (Rectangle t in Overworld.BossWalls)
            {
                if (newHitBox.Intersects(t))
                {
                    isIntersecting = true;
                    break;
                }
            }
            foreach (Rectangle t in Overworld.ChangeWalls)
            {
                if (newHitBox.Intersects(t))
                {
                    isIntersecting = true;
                    break;
                }
            }
            if (isIntersecting)
                newHitBox.X -= (int)move.X;
            isIntersecting = false;
            newHitBox.Y = hitBox.Y + (int)move.Y;
            foreach (Rectangle t in Overworld.WALLS)
            {
                if (newHitBox.Intersects(t))
                {
                    isIntersecting = true;
                    break;
                }
            }
            foreach (Rectangle t in Overworld.BossWalls)
            {
                if (newHitBox.Intersects(t))
                {
                    isIntersecting = true;
                    break;
                }
            }
            foreach (Rectangle t in Overworld.ChangeWalls)
            {
                if (newHitBox.Intersects(t))
                {
                    isIntersecting = true;
                    break;
                }
            }
            if (isIntersecting)
                newHitBox.Y -= (int)move.Y;
            SetWorldPos(newHitBox.X, newHitBox.Y);
        }

    }
}