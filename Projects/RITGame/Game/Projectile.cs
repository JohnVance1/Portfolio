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
    class Projectile : IDrawable
    {
        private Rectangle hitBox; //for projectile collisions
        private Texture2D sprite;
        private Vector2 move;
        private int attack;
        private const int SPEED_OF_BULLET = 10;
        private double hypot;
        private double sideX;
        private double sideY;
        private bool greaterPIY;
        private bool greaterPIX;

        public Rectangle GetWorldPos()
        {
            return hitBox;
        }

        public Texture2D GetTexture()
        {
            return sprite;
        }

        public int Attack
        { get { return attack; } }

        public Rectangle HitBox
        {
            get { return hitBox; }
        }

        public Vector2 Move
        {
            get { return move; }
        }
        
        public double Hypotenuse
        {
            get { return hypot; }

        }

        public double SideX
        {
            get { return sideX; }

        }

        public double SideY
        {
            get { return sideY; }

        }

        public bool GreaterPIY
        {
            get { return greaterPIY; }
        }

        public bool GreaterPIX
        {
            get { return greaterPIX; }
        }

        public Projectile()
        {

        }

        public Projectile(Rectangle playerPos, Projectile sample, Texture2D texture)
        {
            hitBox = sample.GetWorldPos();
            hitBox.X = playerPos.X + (playerPos.Width / 2);
            hitBox.Y = playerPos.Y + (playerPos.Height / 2);
            sprite = texture;
        }

        public Projectile(Rectangle playerPos, Point cursorPos, Projectile sample)
        {
            hitBox = sample.GetWorldPos();
            attack = sample.Attack;
            sprite = sample.GetTexture();
            hitBox.X = playerPos.X + (playerPos.Width / 2);
            hitBox.Y = playerPos.Y + (playerPos.Height  /2);
            sideX = cursorPos.X - hitBox.X;
            sideY = cursorPos.Y - hitBox.Y;
            if(sideY > 0)
            {
                greaterPIY = true;
            }
            if(sideX < 0)
            {
                greaterPIX = true;
            }
            double hypotenuse = Math.Sqrt(Math.Pow(cursorPos.X - hitBox.X, 2) + Math.Pow(cursorPos.Y - hitBox.Y, 2));
            hypot = hypotenuse;
            float movementX = (float)(2 * (cursorPos.X - hitBox.X) / hypotenuse);
            float movementY = (float)(2 * (cursorPos.Y - hitBox.Y) / hypotenuse);
            move.X = movementX * SPEED_OF_BULLET;
            move.Y = movementY * SPEED_OF_BULLET;
        }

        public Projectile(Texture2D texture)
        {
            hitBox.X = -100;
            hitBox.Y = -100;
            attack = 1;
            hitBox.Width = 25;
            hitBox.Height = 25;
            sprite = texture;
        }

        public bool IsColliding(Rectangle otherObject)
        {
            if (otherObject.Intersects(HitBox))
            {
                return true;
            }

            else
            {
                return false;
            }
        }
              
        /// <summary>
        /// Very basic movement system in order to test out projectile interactions
        /// highkey would not recommend keeping
        /// </summary>
        public void Movement()
        {
            hitBox.X += (int)move.X;
            hitBox.Y += (int)move.Y;
        }
        //collisions
        //not damageable
        //deals with an event maybe?
    }
}
