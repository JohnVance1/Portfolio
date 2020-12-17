using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameName
{
    class Character : IDrawable
    {
        protected Rectangle hitBox;

        private int health; 

        private Texture2D sprite;

        protected int movementSpeed;

        /* // if we want some kind of overall score system?
         private int totalScore;
         public int TotalScore
         {get{return totalScore;}}
             
             */

        public Character(Texture2D texture)
        {
            hitBox = new Rectangle(140,140,80,80);
            sprite = texture;
            movementSpeed = 5;
        }

        public void SetWorldPos(int x, int y)
        {
            hitBox.X = x;
            hitBox.Y = y;
        }

        public Rectangle GetWorldPos()
        {
            return hitBox;
        }

        public void SetTexture(Texture2D texture)
        {
            sprite = texture;
        }

        public Texture2D GetTexture()
        {
            return sprite;
        }

        public void SetSpeed(int speed)
        {
            movementSpeed = speed;
        }

        public Rectangle HitBox
        {
            set { hitBox = value; }
            get { return hitBox; }
        }           

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int MovementSpeed
        {
            set { movementSpeed = value; }
            get { return movementSpeed; }
        }

        /// <summary>
        /// Allows for the Character to be loaded in the Game1 class
        /// </summary>
        /// <param name="content">This will be the original ContentManager from the Game1 class</param>
        /// <param name="textureName">The texture will also be brought into from the Game1 class</param>
        public void LoadContent(ContentManager content, string textureName)
        {
            sprite = content.Load<Texture2D>(textureName);

        }

        //implement collision
        public bool IsColliding(Character otherChar)
           
        {
            if(otherChar.HitBox.Intersects(HitBox))
            {
                return true;

            }

            else
            {
                return false;

            }

        }

        public bool CheckRanged(Projectile p)
        {
            if (p.IsColliding(HitBox))
            {
                return true;
            }
            return false;
        }

        //implement movement
        public void UpdatePos()
        {
            
        }

        /// <summary>
        /// Allows for this Character to be drawn
        /// </summary>
        /// <param name="spritebatch">This is the spritebatch from the Game1 class</param>
        public void DrawSelf(SpriteBatch spritebatch)
        {
            spritebatch.Draw(sprite, HitBox, Color.White);
            
        }


    }
}
