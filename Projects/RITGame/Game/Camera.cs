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
    class Camera
    {
        //the world coordinates of the screen's (0,0)
        private int camX;
        private int camY;

        private int screenWidth;
        private int screenHeight;

        private int worldWidth;
        private int worldHeight;

        private bool isIntroScrolling;

        public bool IsIntroScrolling { set { isIntroScrolling = value; } get { return isIntroScrolling; } }

        /// <summary>
        /// Gets or sets the x position of the camera
        /// </summary>
        public int CamX { get { return camX; } set { camX = value; } }
        /// <summary>
        /// Gets or sets the y position of the camera
        /// </summary>
        public int CamY { get { return camY; } set { camY = value; } }

        public Camera(int screenWidth, int screenHeight, int worldWidth, int worldHeight)
        {
            //these will need to be changed to the actual starting position in the game
            //probably have to use viewport settings to figure it out
            camX = 0;
            camY = 0;
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.worldWidth = worldWidth;
            this.worldHeight = worldHeight;
        }

        public int WorldHeight { get { return worldHeight; } }

        public int ScreenHeight { get { return screenHeight; } }

        public bool IsOnCamera(Rectangle objRect)
        {
            Rectangle cameraRect = new Rectangle(camX, camY, screenWidth, screenHeight);
            return cameraRect.Intersects(objRect);
        }

        public float Rotation(Projectile projectile)
        {
            float rotate = (float)Math.Asin((projectile.SideX / projectile.Hypotenuse));
            float rotateBottom = (float)Math.Acos((projectile.SideY / projectile.Hypotenuse));
            if (projectile.GreaterPIY || projectile.GreaterPIX)
            {
                if (projectile.GreaterPIY && projectile.GreaterPIX)
                {
                    return (float)Math.PI - rotate; //* (float)(Math.PI);
                }

                else if(projectile.GreaterPIX)
                {
                    return rotateBottom + (float)Math.PI;
                }

                else
                {
                    return (float)Math.PI - rotateBottom;
                }

            }

            else
            {
                return rotate;

            }

        }

        /// <summary>
        /// Draws a list of objects
        /// </summary>
        public void DrawList(SpriteBatch spriteBatch, List<IDrawable> drawList)
        {
            foreach (IDrawable obj in drawList)
            {
                DrawItem(spriteBatch, obj);
            }
        }

        public void DrawProjectiles(SpriteBatch spriteBatch, List<IDrawable> drawList)
        {
            foreach (IDrawable obj in drawList)
            {
                DrawTiltedItem(spriteBatch, obj);
            }
        }

        /// <summary>
        /// Draws a single item
        /// </summary>
        public void DrawItem(SpriteBatch spriteBatch, IDrawable drawable)
        {
            //gets the world coordinates of the object
            Rectangle objRect = drawable.GetWorldPos();

            //if the object is within the camera bounds, draw it to the screen
            if (IsOnCamera(objRect))
            {                
                if (drawable is Projectile)
                {
                    objRect.X -= camX;
                    objRect.Y -= camY;
                    spriteBatch.Draw(drawable.GetTexture(), objRect, Color.Brown);

                }

                else
                {
                    objRect.X -= camX;
                    objRect.Y -= camY;
                    spriteBatch.Draw(drawable.GetTexture(), objRect, Color.White);
                }
            }
        }

        /// <summary>
        /// Draws a damaged item
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="drawable"></param>
        public void DrawDamagedItem(SpriteBatch spriteBatch, IDrawable drawable)
        {
            //gets the world coordinates of the object
            Rectangle objRect = drawable.GetWorldPos();
            int worldX = objRect.X;
            int worldY = objRect.Y;
            int width = objRect.Width;
            int height = objRect.Height;

            //if the object is within the camera bounds, draw it to the screen
            if (!(worldX + width < camX || worldY + height < camY || worldY > camY + screenHeight || worldX > camX + screenWidth))
            {                
                objRect.X -= camX;
                objRect.Y -= camY;
                spriteBatch.Draw(drawable.GetTexture(), objRect, Color.Red);
            }


        }

        /// <summary>
        /// Draws a tilted Projectile
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="drawable"></param>
        public void DrawTiltedItem(SpriteBatch spriteBatch, IDrawable drawable)
        {
            //gets the world coordinates of the object
            Rectangle objRect = drawable.GetWorldPos();
            objRect.Height *= 2;

            Projectile projectile = (Projectile)drawable;

            //if the object is within the camera bounds, draw it to the screen
            if (IsOnCamera(objRect))
            {
                objRect.X -= camX;
                objRect.Y -= camY;
                spriteBatch.Draw(drawable.GetTexture(), null, objRect, null, null, Rotation(projectile), null, Color.White, SpriteEffects.None, 0);
            }
        }


        /// <summary>
        /// Checks if the camera should be moved and then does so
        /// </summary>
        /// <param name="hitbox">The hitbox for the characater that would cause the camera to move</param>
        /// <param name="distanceBuffer">The max distance between the hitbox and the edge of the screen that should make the camera move</param>
        public void MoveCamera(Rectangle hitbox, int distanceBuffer, int speed)
        {
            //if the hitbox is within distanceBuffer of the edge of the screen, move the camera accordingly
            //if(hitbox.X < camX + distanceBuffer) { camX -= speed; }
            if(hitbox.Y < camY + distanceBuffer * 1.5) { camY -= speed; }
            //if(hitbox.X + hitbox.Width > camX + screenWidth - distanceBuffer) { camX += speed; }
            if(hitbox.Y + hitbox.Height > camY + screenHeight - distanceBuffer / 1.5 && SceneManager.GameState != GameState.Overworld) { camY += speed; }

            //makes sure the camera stays within the bounds of the game
            if(camX < 0) { camX = 0; }
            if(camY < 0) { camY = 0; }
            if(camX > worldWidth) { camX = worldWidth; }
            if(camY > worldHeight) { camY = worldHeight; }
        }

        public void DisplaceCamera(int x, int y, int speed)
        {
            //if (camX > x) camX -= speed;
            //else if (camX < x) camX += speed;
            if (camY > y)
            {
                camY -= speed;
                if (CamY < y) CamY = y;
            }
            else if (camY < y)
            {
                camY += speed;
                if (CamY > y) CamY = y;
            }
        }

        /// <summary>
        /// Converts a screen position to a world position
        /// </summary>
        /// <param name="screenPos">The position to convert</param>
        /// <returns>The origin point of the world rectangle that corresponds to the screen position</returns>
        public Point ScreenToWorldPos(Rectangle screenPos)
        {
            Rectangle rect = new Rectangle(screenPos.X + camX, screenPos.Y + camY, screenPos.Width, screenPos.Height);
            return new Point(rect.X, rect.Y);
        }
    }
}
